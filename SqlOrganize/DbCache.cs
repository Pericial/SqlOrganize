using Microsoft.Extensions.Caching.Memory;
using Utils;

namespace SqlOrganize
{

   

    /*
    Uso de cache para resutado de consulta
    */
    public class DbCache
    {
        public Db Db { get; }

        public MemoryCache Cache { get; set; }

        public DbCache(Db db, MemoryCache cache)
        {
            Db = db;
            Cache = cache;
        }


        /*
        Obtener campos de una entidad (sin relaciones)
        Si no encuentra valores en el Cache, realiza una consulta a la base de datos
        y lo almacena en Cache.
        */
        public List<Dictionary<string, object>> ListDict(string entityName, params object[] ids)
        {
            ids = ids.Distinct().ToArray();

            List<Dictionary<string, object>> response = new(ids.Length); //respuesta que sera devuelta

            List<object> searchIds = new(); //ids que no se encuentran en cache y deben ser buscados

            for (var i = 0; i < ids.Length; i++)
            {
                object? data;
                if (Cache.TryGetValue(entityName + ids[i], out data))
                {
                    response.Insert(i, (Dictionary<string, object>)data!);
                } else
                {
                    response.Insert(i, null);
                    searchIds.Add(ids[i]);
                }
            }

            if (searchIds.Count == 0)
                return response;

            string q = Db.Query(entityName).Where("$_Id IN (@0)").Parameters(searchIds).Sql();
            List<Dictionary<string, object>> rows = Db.Query(entityName).Where("$_Id IN (@0)").Parameters(searchIds).ListDict();

            foreach (Dictionary<string, object> row in rows)
            {
                int index = Array.IndexOf(ids, row["_Id"]);
                response[index] = EntityCache(entityName, row);
            }

            return response;
        }


        protected List<Dictionary<string, object>> _ListDict(EntityQuery query)
        {
            List<string> queries;
            if (!Cache.TryGetValue("queries", out queries))
                queries = new();

            List<Dictionary<string, object>> result;
            string queryKey = query!.ToString();
            if (!Cache.TryGetValue(queryKey, out result))
            {
                result = query.ListDict();
                Cache.Set(queryKey, result);
                queries!.Add(queryKey);
                Cache.Set<List<string>>("queries", queries);
            }
            return result!;
        }

        /*
        TODO se puede optimizar y evitar tantos loops?
        */        
        public List<T> ListObj<T>(EntityQuery query) where T : class, new()
        {
            List<Dictionary<string, object>> response = ListDict(query);
            return response.ConvertToListOfObject<T>();
        }

        public List<Dictionary<string, object>> ListDict(EntityQuery query)
        {
            List<Dictionary<string, object>> response = new();

            if (!query.select.IsNullOrEmpty() || !query.group.IsNullOrEmpty())
                return _ListDict(query);

            if (query.fields.IsNullOrEmpty())
                query.Fields();
            
            EntityQuery queryAux = (EntityQuery)query.Clone();
            queryAux.fields = "_Id";

            List<string> ids = queryAux.Column<string>();

            List<string> fields = query.fields!.Replace("$", "").Split(',').ToList().Select(s => s.Trim()).ToList();

            FieldsOrganize fo = new(Db, query.entityName, fields);

            List<Dictionary<string, object>> data = ListDict(query.entityName, ids.ToArray());
            
            for (var i = 0; i < data.Count; i++)
            {
                response.Add(new());
                for (var j = 0; j < fo.Fields.Count; j++) response[i][fo.Fields[j]] = null;
                for (var j = 0; j < fo.FieldsMain.Count; j++) response[i][fo.FieldsMain[j]] = data[i][fo.FieldsMain[j]];
            }
            return ListDictRecursive(fo, response, 0);

        }

        public List<Dictionary<string, object>> ListDictRecursive(FieldsOrganize fo, List<Dictionary<string, object>> response, int index)
        {
            if (index >= fo.FieldsIdOrder.Count) return response;
            {
                if (response.Count == 0) return response;

                string fieldId = fo.FieldsIdOrder[index];
                string refEntityName = Db.Entity(fo.EntityName).relations[fieldId].refEntityName;
                string? parentId = Db.Entity(fo.EntityName).relations[fieldId].parentId;
                string fieldName = Db.Entity(fo.EntityName).relations[fieldId].fieldName;
                string refFieldName = Db.Entity(fo.EntityName).relations[fieldId].refFieldName;

                string fkName = (!parentId.IsNullOrEmpty()) ? parentId + Db.config.idNameSeparatorString + fieldName : fieldName;

                List<object> ids = response.Column<object>(fkName).Distinct().ToList();
                ids.RemoveAll(item => item == null);
                List<Dictionary<string, object>> data;
                if (ids.Count == 1 && ids[0] == System.DBNull.Value)
                    data = new();
                else 
                    data = ListDict(refEntityName, ids.ToArray());

                for(var i = 0; i < response.Count; i++)
                {
                    for (var j = 0; j < data.Count; j++)
                    {
                        if (response[i][fkName].Equals(data[j][refFieldName]))
                        {
                            for (var k = 0; k < fo.FieldsRel[fieldId].Count; k++)
                            {
                                var n = fo.FieldsRel[fieldId][k];
                                response[i][fieldId + Db.config.idNameSeparatorString + n] = data[j][n];
                            }
                        }
                    }
                }

                return (++index < fo.FieldsIdOrder.Count) ? ListDictRecursive(fo, response, index) : response;
            }
        }


        protected Dictionary<string, object> EntityCache(string entityName, Dictionary<string, object> row)
        {
            if(!Db.Entity(entityName).relations.IsNullOrEmpty()) 
                EntityCacheRecursive(Db.Entity(entityName).relations!, row);

            Cache.Set(entityName+row["_Id"].ToString(), row);
            return row;
        }

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
                if(rowAux.Count > 0)
                    Cache.Set(entityName + rowAux["_Id"].ToString(), rowAux);

            }
        }

    }

    /*
   Clase interna para organizar campos
   */
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
