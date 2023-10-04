using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Utils;

namespace SqlOrganize
{

    /*
    Consulta y formato de datos

    Efectua la consulta a la base de datos y la transforma en el formato so-
    licitado.
    
    Los fields se traducen con los metodos de mapeo, deben indicarse con el 
    prefijo $
        . indica aplicacion de funcion de agregacion
        - indica que pertenece a una relacion
        Ej "($ingreso = %p1) AND ($persona-nombres.max = %p1)"
    */
    public abstract class EntityQuery
    {
        public Db Db { get; }


        public string entityName { get; set; }

        public string? where { get; set; } = "";
        


        public string? having { get; set; }

        public string? fields { get; set; } = "";

        public string? select { get; set; } = "";

        public string? order { get; set; } = "";

        public int size { get; set; } = 100;

        public int page { get; set; } = 1;

        public string group { get; set; } = "";

        public List<object> parameters = new List<object> { };

        public Dictionary<string, object> parametersDict = new ();




        public EntityQuery(Db db, string entityName)
        {
            Db = db;
            this.entityName = entityName;
        }

        public EntityQuery Where(string w)
        {
            where += w;
            return this;
        }

        public EntityQuery Search<T>(T param) where T : class
        {
            var d = param.Dict();
            return Search(d);
        }

        /// <summary>
        /// Crear condicion de busqueda del diccionario
        /// </summary>
        /// <param name="param">Diccionario fieldName : Valor</param>
        /// <returns>this</returns>
        /// <remarks>Filtra los campos que pertenecen a la entidad</remarks>
        public EntityQuery Search(IDictionary<string, object> param)
        {
            var count = parameters.Count;
            foreach(var (key, value) in param)
            {
                if (!Db.FieldNamesRel(entityName).Contains(key))
                    continue;
                if(!value.IsNullOrEmpty())
                {
                    if (!where.IsNullOrEmpty())
                        Where(" AND ");
                    Where("$"+key+" = @" + count.ToString());
                    Parameters(value);
                    count++;
                }
            }
            return this;
        }


        public EntityQuery Unique(EntityValues values)
        {
            return Unique(values.values);
        }

        public EntityQuery Unique(IDictionary<string, object> row)
        {
            if (row.IsNullOrEmpty())
                throw new Exception("El parametro de condicion unica esta vacio");

            List<string> whereUniqueList = new();
            foreach (string fieldName in Db.Entity(entityName).unique)
            {
                foreach (var (key, value) in row)
                {
                    if (key == fieldName)
                    {
                        var v = (value == null) ? DBNull.Value : value;
                        whereUniqueList.Add("$" + key + " = @" + parameters.Count);
                        parameters.Add(v);
                        break;
                    }
                }
            }

            string w = "";
            if (whereUniqueList.Count > 0)
                w = "(" + String.Join(") OR (", whereUniqueList) + ")";

            string ww;
            foreach(var um in Db.Entity(entityName).uniqueMultiple)
            {
                ww = UniqueMultiple(um, row);
                if (!ww.IsNullOrEmpty())
                    w += (w.IsNullOrEmpty()) ? ww : " OR " + ww;
            }

            ww = UniqueMultiple(Db.Entity(entityName).pk, row);
            if (!ww.IsNullOrEmpty())
                w += (w.IsNullOrEmpty()) ? ww : " OR " + ww;

            if (w.IsNullOrEmpty())
                throw new Exception("No se pudo definir condicion de campo unico con el parametro indicado");

            where += (where.IsNullOrEmpty()) ? w : " AND (" + w + ")";

            return this;
        }

       

        protected string UniqueMultiple(List<string> fields, IDictionary<string, object> param)
        {
            if (fields.IsNullOrEmpty())
                return "";

            bool existsUniqueMultiple = true;
            List<string> whereMultipleList = new();
            foreach(string field in fields)
            {
                if (!existsUniqueMultiple) 
                    break;

                existsUniqueMultiple = false;

                foreach(var (key, value) in param)
                    if (key == field)
                    {
                        var v = (value == null) ? DBNull.Value : value;
                        existsUniqueMultiple = true;
                        whereMultipleList.Add("$" + key + " = @" + parameters.Count);
                        parameters.Add(v);
                        break;
                    }
                
            }
            if(existsUniqueMultiple && whereMultipleList.Count > 0)
                return "(" + String.Join(") AND (", whereMultipleList) + ")";                

            return "";
        }

        public EntityQuery Fields()
        {
            fields += string.Join(", ", Db.FieldNamesRel(entityName));
            return this;
        }

        public EntityQuery Fields(string f)
        {
            fields += f;
            return this;
        }

        public EntityQuery Fields(params string[] f)
        {
            fields += String.Join(", ", f);
            return this;
        }

        public EntityQuery Select(string f)
        {
            select += f;
            return this;
        }

        public EntityQuery Parameters(params object[] parameters)
        {
            this.parameters.AddRange(parameters.ToList());
            return this;
        }

        public EntityQuery Parameters(Dictionary<string, object> parameters)
        {
            this.parametersDict.Merge(parameters);
            return this;
        }

        protected string TraduceFields(string _sql)
        {
            if (_sql.IsNullOrEmpty())
                return "";

            List<string> fields = _sql!.Replace("$", "").Split(',').ToList().Select(s => s.Trim()).ToList();

            #region procesar *
            List<string> fieldNamesToDelete = new();
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].Contains("*"))
                {
                    fieldNamesToDelete.Add(fields[i]);
                    var en = entityName;
                    var fid = "";
                    if (fields[i].Contains(Db.config.idNameSeparatorString))
                    {
                        List<string> ff = fields[i].Split(Db.config.idNameSeparatorString).ToList();
                        en = Db.Entity(entityName).relations[ff[0]].refEntityName;
                        fid = ff[0] + Db.config.idNameSeparatorString;
                    }

                    List<string> fns = (List<string>)Db.FieldNames(en).AddPrefixToEnum(fid);
                    fields.AddRange(fns);
                }
            }

            foreach (var fntd in fieldNamesToDelete)
                fields.Remove(fntd);
            #endregion

            #region definir sql
            string sql = "";

            foreach (var fieldName in fields)
            {
                if (fieldName.Contains(Db.config.idNameSeparatorString))
                {
                    List<string> ff = fieldName.Split(Db.config.idNameSeparatorString).ToList();
                    sql += Db.Mapping(Db.Entity(entityName).relations[ff[0]].refEntityName, ff[0]).Map(ff[1]) + " AS '" + fieldName + "', ";
                } else
                    sql += Db.Mapping(entityName).Map(fieldName) + " AS '" + fieldName + "', ";
            }
            sql = sql.RemoveLastChar(',');
            #endregion

            return sql;
        }

        protected string Traduce(string _sql, bool fieldAs = false )
        {
            string sql = "";
            int field_start = -1;

            for (int i = 0; i < _sql.Length; i++)
            {
                if (_sql[i] == '$')
                {
                    field_start = i;
                    continue;
                }

                if (field_start != -1)
                {
                    if ((_sql[i] != ' ') && (_sql[i] != ')') && (_sql[i] != ',')) continue;
                    sql += Traduce_(_sql, field_start, i - field_start - 1, fieldAs);
                    field_start = -1;
                }

                sql += _sql[i];

            }

            if (field_start != -1)
                sql += Traduce_(_sql, field_start, _sql.Length - field_start - 1, fieldAs);


            return sql;
        }

        protected string Traduce_(string _sql, int fieldStart, int fieldEnd, bool fieldAs)
        {
            var fieldName = _sql.Substring(fieldStart + 1, fieldEnd);

            string ff = "";
            if (fieldName.Contains('-'))
            {
                List<string> fff = fieldName.Split("-").ToList();
                ff += Db.Mapping(Db.Entity(entityName).relations[fff[0]].refEntityName, fff[0]).Map(fff[1]);
            }
            else
                ff += Db.Mapping(entityName).Map(fieldName);

            if (fieldAs) ff += " AS '" + fieldName + "'";
            return ff;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_size"></param>
        /// <returns></returns>
        public EntityQuery Size(int _size)
        {
            size = _size;
            return this;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_page"></param>
        /// <returns></returns>
        public EntityQuery Page(int _page)
        {
            page = _page;
            return this;
        }

        public EntityQuery Order(string _order)
        {
            order = _order;
            return this;
        }

        public EntityQuery Having(string h)
        {
            having += h;
            return this;
        }

        public EntityQuery Group(string g)
        {
            group += g;
            return this;
        }

        protected string SqlJoin()
        {
            string sql = "";
            if (!Db.Entity(entityName).tree.IsNullOrEmpty())
                sql += SqlJoinFk(Db.Entity(entityName).tree!, "");
            return sql;
        }

        protected string SqlJoinFk(Dictionary<string, EntityTree> tree, string table_id)
        {
            if (table_id.IsNullOrEmpty())
                table_id = Db.Entity(entityName).alias;

            string sql = "";
            string schema_name;
            foreach (var (field_id, entity_tree) in tree) {
                schema_name = Db.Entity(entity_tree.refEntityName).schemaName;
                sql += "LEFT OUTER JOIN " + schema_name + " AS " + field_id + " ON (" + table_id + "." + entity_tree.fieldName + " = " + field_id + "." + entity_tree.refFieldName + @")
";

                if (!entity_tree.children.IsNullOrEmpty()) sql += SqlJoinFk(entity_tree.children, field_id);
            }
            return sql;
        }

        public string Sql()
        {
            var sql = "SELECT DISTINCT ";
            sql += SqlFields();
            sql += SqlFrom();
            sql += SqlJoin();
            sql += SqlWhere();
            sql += SqlGroup();
            sql += SqlHaving();
            sql += SqlOrder();
            sql += SqlLimit();

            return sql;
        }

        protected string SqlWhere()
        {
            return (where.IsNullOrEmpty()) ? "" : "WHERE " + Traduce(where!) + @"
";
        }

        protected string SqlGroup()
        {
            return (group.IsNullOrEmpty()) ? "" : "GROUP BY " + Traduce(group!) + @"
";
        }

        protected string SqlHaving()
        {
            return (having.IsNullOrEmpty()) ? "" : "HAVING " + Traduce(having!) + @"
";
        }

        protected abstract string SqlOrder();

        protected virtual string SqlFields()
        {
            if(this.fields.IsNullOrEmpty() && this.select.IsNullOrEmpty() && this.group.IsNullOrEmpty())
                this.Fields();

            string f = TraduceFields(this.fields);

            f += Concat(Traduce(this.select), @",
", "", !f.IsNullOrEmpty());

            f += Concat(Traduce(this.group, true), @",
", "", !f.IsNullOrEmpty());

            return f + @"
";
        }

        protected string SqlFrom()
        {
            return @"FROM " + Db.Entity(entityName).schemaName + " AS " + Db.Entity(entityName).alias + @"
";
        }

        protected abstract string SqlLimit();
       
        protected string Concat(string? value, string connect_true, string connect_false = "", bool connect_cond = true)
        {
            if (value.IsNullOrEmpty()) return "";

            string connect = "";
            if (connect_cond)
                connect = connect_true;
            else
                connect = connect_false;

            return connect + " " + value;
        }

        public override string ToString()
        {
            return Regex.Replace(entityName + where + having + fields + select + order + size + page + JsonConvert.SerializeObject(parameters), @"\s+", "");
        }


        /// <summary>
        /// Obtener lista
        /// </summary>
        /// <remarks>Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)</remarks>
        /// <returns></returns>
        public IEnumerable<Dictionary<string, object>> ColOfDict()
        {
            var q = Db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            q.parametersDict.Merge(parametersDict);
            return q.ColOfDict();
        }

        public IEnumerable<T> ColOfObj<T>() where T : class, new()
        {
            var q = Db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            q.parametersDict.Merge(parametersDict);
            return q.ColOfObj<T>();
        }

        public IDictionary<string, object> Dict()
        {
            var q = Db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            q.parametersDict.Merge(parametersDict);
            return q.Dict();
        }

        public T Obj<T>() where T : class, new()
        {
            var q = Db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            q.parametersDict.Merge(parametersDict);
            return q.Obj<T>();

        }

        public IEnumerable<T> Column<T>(string columnName)
        {
            var q = Db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            q.parametersDict.Merge(parametersDict);
            return q.Column<T>(columnName);
        }

        public IEnumerable<T> Column<T>(int columnValue = 0)
        {
            var q = Db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            q.parametersDict.Merge(parametersDict);
            return q.Column<T>(columnValue);
        }
        public T Value<T>(string columnName)
        {
            var q = Db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            q.parametersDict.Merge(parametersDict);
            return q.Value<T>(columnName);
        }

        public T Value<T>(int columnValue = 0)
        {
            var q = Db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            q.parametersDict.Merge(parametersDict);
            return q.Value<T>(columnValue);
        }

        public abstract EntityQuery Clone();

        protected EntityQuery _Clone(EntityQuery eq)
        {
            eq.entityName = entityName;
            eq.size = size;
            eq.where = where;
            eq.page = page;
            eq.parameters = parameters;
            eq.parametersDict = parametersDict;
            eq.group = group;
            eq.having = having;
            eq.fields = fields;
            eq.select = select;
            eq.order = order;
            return eq;
        }











        #region Metodos que utilizan cache

        /// <summary>
        /// Metodo de busqueda rapida en cache
        /// </summary>
        /// <remarks>Solo analiza el atributo fields (devuelve relaciones)</remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IEnumerable<Dictionary<string, object>> CacheByIds(params object[] ids)
        {
            if (this.fields.IsNullOrEmpty())
                this.Fields();

            List<string> _fields = fields!.Replace("$", "").Split(',').ToList().Select(s => s.Trim()).ToList();

            return PreColOfDictCacheRecursive(_fields, ids);
        }

        /// <summary>
        /// Metodo de busqueda rapida en cache
        /// </summary>
        /// <remarks>Solo analiza el atributo fields (devuelve relaciones)</remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IDictionary<string, object>? CacheById(object id)
        {
            var list = CacheByIds(id);
            if (list.IsNullOrEmpty())
                return null;

            return list.ElementAt(0);
        }

        public IDictionary<string, object>? _CacheById(object id)
        {
            var list = _CacheByIds(id);
            if (list.IsNullOrEmpty())
                return null;

            return list.ElementAt(0);
        }


        /// <summary>
        /// Obtener campos de una entidad (sin relaciones)<br/>
        /// Si no encuentra valores en el Cache, realiza una consulta a la base de datos y lo almacena en Db.Cache.
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="ids"></param>
        /// <remarks>IMPORTANTE! No devuelve relaciones!!!</remarks>
        /// <returns></returns>
        public List<Dictionary<string, object>> _CacheByIds(params object[] ids)
        {
            ids = ids.Distinct().ToArray();

            List<Dictionary<string, object>> response = new(ids.Length); //respuesta que sera devuelta

            List<object> searchIds = new(); //ids que no se encuentran en cache y deben ser buscados

            for (var i = 0; i < ids.Length; i++)
            {
                object? data;
                if (Db.Cache.TryGetValue(entityName + ids[i], out data))
                {
                    response.Insert(i, (Dictionary<string, object>)data!);
                }
                else
                {
                    response.Insert(i, null);
                    searchIds.Add(ids[i]);
                }
            }

            if (searchIds.Count == 0)
                return response;

            IEnumerable<Dictionary<string, object>> rows = Db.Query(entityName).Size(0).Where("$" + Db.config.id + " IN (@0)").Parameters(searchIds).ColOfDict();

            foreach (Dictionary<string, object> row in rows)
            {
                int index = Array.IndexOf(ids, row[Db.config.id]);
                response[index] = EntityCache(entityName, row);
            }

            return response;
        }

        /// <summary>
        /// Ejecuta consulta de datos (con relaciones).<br/>
        /// Verifica la cache para obtener el resultado de la consulta, si no existe en cache accede a la base de datos.
        /// </summary>
        public IEnumerable<Dictionary<string, object>> ColOfDictCacheQuery()
        {
            List<string> queries;
            if (!Db.Cache.TryGetValue("queries", out queries))
                queries = new();

            IEnumerable<Dictionary<string, object>> result;
            string queryKey = this!.ToString();
            if (!Db.Cache.TryGetValue(queryKey, out result))
            {
                result = this.ColOfDict();
                Db.Cache.Set(queryKey, result);
                queries!.Add(queryKey);
                Db.Cache.Set("queries", queries);
            }
            return result!;
        }

        /// <summary>
        /// Ejecuta consulta de datos (con relaciones).<br/>
        /// Verifica la cache para obtener el resultado de la consulta, si no existe en cache accede a la base de datos.
        /// </summary>
        protected IDictionary<string, object> DictCacheQuery()
        {
            List<string> queries;
            if (!Db.Cache.TryGetValue("queries", out queries))
                queries = new();

            IDictionary<string, object> result;
            string queryKey = this!.ToString();
            if (!Db.Cache.TryGetValue(queryKey, out result))
            {
                result = this.Dict();
                Db.Cache.Set(queryKey, result);
                queries!.Add(queryKey);
                Db.Cache.Set("queries", queries);
            }
            return result!;
        }

        /// <summary>
        /// Consulta de datos (uso de cache para consulta y resultados)<br/>
        /// </summary>
        /// <param name="query">Consulta</param>
        public IEnumerable<Dictionary<string, object>> ColOfDictCache()
        {
            if (!this.select.IsNullOrEmpty() || !this.group.IsNullOrEmpty())
                return ColOfDictCacheQuery();

            if (this.fields.IsNullOrEmpty())
                this.Fields();

            List<string> _fields = this.fields!.Replace("$", "").Split(',').ToList().Select(s => s.Trim()).ToList();

            //si no se encuentra el Db.config.id, no se realiza cache.
            //Si por ejemplo se consultan solo campos de relacoines, no se aplicaria correctamente el distinct
            if (!_fields.Contains(Db.config.id))
                return ColOfDictCacheQuery();

            EntityQuery queryAux = this.Clone();
            queryAux.fields = Db.config.id;

            IEnumerable<object> ids = queryAux.ColOfDictCacheQuery().ColOfVal<object>(Db.config.id);

            return PreColOfDictCacheRecursive(_fields, ids.ToArray());
        }

        /// <summary>
        /// Efectua una consulta a la base de datos, la almacena en cache.<br/>
        /// Dependiendo del tipo de consulta almacena la fila de resultado en cache.
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <remarks>Cuando se esta seguro de que se desea consultar una sola fila. Utilizar este metodo para evitar que se tenga que procesar un tamaño grande de resultado</remarks>
        public IDictionary<string, object>? DictCache()
        {
            if (!this.select.IsNullOrEmpty() || !this.group.IsNullOrEmpty())
                return DictCacheQuery();

            if (this.fields.IsNullOrEmpty())
                this.Fields();

            EntityQuery queryAux = this.Clone();
            queryAux.fields = Db.config.id;

            string id = queryAux.Value<string>();

            if (id.IsNullOrEmpty())
                return null;

            List<string> fields = this.fields!.Replace("$", "").Split(',').ToList().Select(s => s.Trim()).ToList();

            IEnumerable<Dictionary<string, object>> response = PreColOfDictCacheRecursive(fields, id);

            return response.ElementAt(0);
        }


        /// <summary>
        /// Organiza los elementos a consultar y efectua la consulta a la base de datos.
        /// </summary>
        protected IEnumerable<Dictionary<string, object>> PreColOfDictCacheRecursive(List<string> fields, params object[] ids)
        {
            FieldsOrganize fo = new(Db, entityName, fields);

            List<Dictionary<string, object>> data = _CacheByIds(ids);

            List<Dictionary<string, object>> response = new();

            for (var i = 0; i < data.Count; i++)
            {
                response.Add(new());
                for (var j = 0; j < fo.Fields.Count; j++)
                    response[i][fo.Fields[j]] = null;
                for (var j = 0; j < fo.FieldsMain.Count; j++)
                    response[i][fo.FieldsMain[j]] = data[i][fo.FieldsMain[j]];
            }

            return ColOfDictCacheRecursive(fo, response, 0);
        }

        /// <summary>
        /// Analiza la respuesta de una consulta y re organiza los elementos para armar el resultado
        /// </summary>
        protected IEnumerable<Dictionary<string, object>> ColOfDictCacheRecursive(FieldsOrganize fo, IEnumerable<Dictionary<string, object>> response, int index)
        {
            if (index >= fo.FieldsIdOrder.Count) return response;
            {
                if (response.Count() == 0) return response;

                string fieldId = fo.FieldsIdOrder[index];
                string refEntityName = Db.Entity(fo.EntityName).relations[fieldId].refEntityName;
                string? parentId = Db.Entity(fo.EntityName).relations[fieldId].parentId;
                string fieldName = Db.Entity(fo.EntityName).relations[fieldId].fieldName;
                string refFieldName = Db.Entity(fo.EntityName).relations[fieldId].refFieldName;
                string fkName = (!parentId.IsNullOrEmpty()) ? parentId + Db.config.idNameSeparatorString + fieldName : fieldName;

                List<object> ids = response.ColOfVal<object>(fkName).Distinct().ToList();
                ids.RemoveAll(item => item == null || item == System.DBNull.Value);
                IEnumerable<Dictionary<string, object>> data;
                if (ids.Count() == 1 && ids.ElementAt(0) == System.DBNull.Value)
                    return Enumerable.Empty<Dictionary<string, object>>();
                else
                {
                    //Si las fk estan asociadas a una unica pk, debe indicarse para mayor eficiencia
                    if (Db.config.fkId)
                    {
                        data = Db.Query(refEntityName)._CacheByIds(ids.ToArray());
                    }
                    else
                    {
                        //data = Db.Query(refEntityName).Where("$"+Db.config.id+" IN (@0)").Parameters(ids).ColOfDictCacheQuery();
                        data = Db.Query(refEntityName).CacheByIds(ids.ToArray());
                    }
                }

                for (var i = 0; i < response.Count(); i++)
                {
                    if (response.ElementAt(i)[fkName].IsNullOrEmpty())
                        continue;

                    for (var j = 0; j < data.Count(); j++)
                    {
                        if (response.ElementAt(i)[fkName].Equals(data.ElementAt(j)[refFieldName]))
                        {
                            for (var k = 0; k < fo.FieldsRel[fieldId].Count; k++)
                            {
                                var n = fo.FieldsRel[fieldId][k];
                                response.ElementAt(i)[fieldId + Db.config.idNameSeparatorString + n] = data.ElementAt(j)[n];
                            }
                        }
                    }
                }

                return (++index < fo.FieldsIdOrder.Count) ? ColOfDictCacheRecursive(fo, response, index) : response;
            }
        }

        /// <summary>
        /// Analiza una fila de resultado y la almacena en cache.
        /// </summary>
        /// <param name="entityName">Nombre de la entidad principal de la fila</param>
        /// <param name="row">Fila de datos (tupla)</param>
        /// <returns>Resultado filtrado solo para la entidad principal</returns>
        protected Dictionary<string, object> EntityCache(string entityName, Dictionary<string, object> row)
        {
            if (!Db.Entity(entityName).relations.IsNullOrEmpty())
                EntityCacheRecursive(Db.Entity(entityName).relations!, row);

            Db.Cache.Set(entityName + row[Db.config.id].ToString(), row);
            return row;
        }

        /// <summary>
        /// Analiza una fila de resultado y la almacena en cache considerando cada entidad de las relaciones. 
        /// </summary>
        /// <param name="relations">Relaciones de una entidad</param>
        /// <param name="row">Fila de datos (tupla)</param>
        protected void EntityCacheRecursive(Dictionary<string, EntityRelation> relations, Dictionary<string, object> row)
        {
            foreach (var (fieldId, rel) in relations)
            {
                var entityName = rel.refEntityName;
                Dictionary<string, object> rowAux = new();
                string f = fieldId + Db.config.idNameSeparatorString;
                foreach (var (column, value) in row)
                {
                    if (column.Contains(f))
                    {
                        string ff = column.Substring(f.Length);
                        rowAux[ff] = value;
                        row.Remove(column);
                    }
                }
                if (rowAux.Count > 0)
                    Db.Cache.Set(entityName + rowAux[Db.config.id].ToString(), rowAux);
            }
        }
        #endregion



    }


    public class FieldsOrganize
    {
        Db Db;

        public string EntityName;

        /*
        Campos a consultar de relaciones, 
        Se pueden agregar fk adicionales necesarias para comparar
        */
        public List<string> Fields;

        /*
        Campos de relaciones
        Para facilitar el filtro de campos de relaciones se agregan agrupadas por fieldId
        */
        public Dictionary<string, List<string>> FieldsRel = new();

        /*
        FieldsId que deben ser consultados en el orden correspondiente
        */
        public List<string> FieldsIdOrder = new();

        /*
        Campos a consultar de la entidad principal "entityName", 
        */
        public List<string> FieldsMain = new();

        public FieldsOrganize(Db db, string entityName, List<string> fields)
        {
            Db = db;
            EntityName = entityName;
            Fields = fields;
            this.OrganizeRelations(0);
            this.OrganizeOrder(Db.Entity(entityName).tree);
        }
        /*
        Organizar fields 
        Se agregan los campos necesarios para consultar y comparar el arbol de fields
        */
        protected void OrganizeRelations(int index)
        {
            if (Fields[index].Contains(Db.config.idNameSeparatorString))
            {
                var f = Fields[index].Split(Db.config.idNameSeparatorString);
                EntityRelation r = Db.Entity(EntityName).relations[f[0]];
                string fkName = (!r.parentId.IsNullOrEmpty()) ? r.parentId + Db.config.idNameSeparatorString + r.fieldName : r.fieldName;

                if (!FieldsRel.ContainsKey(f[0]))
                    FieldsRel.Add(f[0], new List<string>());
                if (!FieldsRel[f[0]].Contains(f[1]))
                    FieldsRel[f[0]].Add(f[1]);
                if (!Fields.Contains(fkName))
                    Fields.Add(fkName);
            }
            else
                FieldsMain.Add(Fields[index]);

            if (++index < Fields.Count)
                OrganizeRelations(index);
        }

        protected void OrganizeOrder(Dictionary<string, EntityTree> tree)
        {
            foreach (var (fieldId, et) in tree)
            {
                bool recorrerChildren = false;
                for (var j = 0; j < Fields.Count; j++)
                {
                    if (Fields[j].Contains(Db.config.idNameSeparatorString))
                    {
                        var f = Fields[j].Split(Db.config.idNameSeparatorString);
                        if (f[0] == fieldId && !FieldsIdOrder.Contains(fieldId))
                        {
                            FieldsIdOrder.Add(fieldId);
                            recorrerChildren = true;
                        }

                    }
                }
                if (recorrerChildren && !et.children.IsNullOrEmpty())
                    OrganizeOrder(et.children);
            }
        }
    }
}
