using Newtonsoft.Json;
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
        public Db db { get; }

        public string entityName { get; }

        public string? where { get; set; } = "";

        public string? having { get; set; }

        public string? fields { get; set; } = "";

        public string? select { get; set; } = "";

        public string? order { get; set; } = "";

        public int size { get; set; } = 100;

        public int page { get; set; } = 1;

        public string group { get; set; } = "";

        public List<object> parameters = new List<object> { };


        public EntityQuery(Db _db, string _entityName)
        {
            db = _db;
            entityName = _entityName;
        }

        public EntityQuery Where(string w)
        {
            where += w;
            return this;
        }

        public EntityQuery Unique(Dictionary<string, object> row)
        {
            List<string> pk = db.Entity(entityName).pk;

            List<string> whereUnique = new();
            foreach (string fieldName in db.Entity(entityName).unique)
            {
                foreach (var (key, value) in row)
                {
                    if (key == fieldName)
                    {
                        whereUnique.Add(key + " = " + value);
                        break;
                    }
                }
            }

            if (whereUnique.Count > 0)
            {
                string w = "(" + String.Join(") OR (", whereUnique) + ")";
                where += (where.IsNullOrEmpty()) ? w : " OR " + w;
            }

            UniqueMultiple(row, db.Entity(entityName).uniqueMultiple);
            UniqueMultiple(row, db.Entity(entityName).pk);

            return this;
        }

        protected EntityQuery UniqueMultiple(Dictionary<string, object> row, List<string> fields)
        {
            bool existsUniqueMultiple = true;
            List<string> whereMultiple = new();
            foreach(string field in fields)
            {
                if (!existsUniqueMultiple) 
                    break;

                existsUniqueMultiple = false;

                foreach(var (key, value) in row)
                {
                    if(key == field)
                    {
                        existsUniqueMultiple = true;
                        whereMultiple.Add(key + " = " + value);
                        break;
                    }
                }
            }
            if(existsUniqueMultiple && whereMultiple.Count > 0)
            {
                string w = "(" + String.Join(") AND (", whereMultiple) + ")";
                where += (where.IsNullOrEmpty()) ? w : " OR " + w;
            }
            return this;
        }

        public EntityQuery Fields()
        {
            fields += string.Join(", ", db.Tools(entityName).FieldNames());
            return this;
        }

        public EntityQuery Fields(string f)
        {
            fields += f;
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

        protected string TraduceFields(string _sql)
        {
            if (_sql.IsNullOrEmpty())
                return "";

            List<string> fields = _sql!.Replace("$", "").Split(',').ToList().Select(s => s.Trim()).ToList();
            string sql = "";
            
            foreach (var fieldName in fields)
            {
                if (fieldName.Contains(db.config.idNameSeparatorString))
                {
                    List<string> ff = fieldName.Split(db.config.idNameSeparatorString).ToList();
                    sql += db.Mapping(db.Entity(entityName).relations[ff[0]].refEntityName, ff[0]).Map(ff[1]) + " AS '" + fieldName + "', ";
                } else
                    sql += db.Mapping(entityName).Map(fieldName) + " AS '" + fieldName + "', ";
            }
            sql = sql.RemoveLastIndex(',');
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
                ff += db.Mapping(db.Entity(entityName).relations[fff[0]].refEntityName, fff[0]).Map(fff[1]);
            }
            else
                ff += db.Mapping(entityName).Map(fieldName);

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
            if (!db.Entity(entityName).tree.IsNullOrEmpty())
                sql += SqlJoinFk(db.Entity(entityName).tree!, "");
            return sql;
        }

        protected string SqlJoinFk(Dictionary<string, EntityTree> tree, string table_id)
        {
            if (table_id.IsNullOrEmpty())
                table_id = db.Entity(entityName).alias;

            string sql = "";
            string schema_name;
            foreach (var (field_id, entity_tree) in tree) {
                schema_name = db.Entity(entity_tree.refEntityName).schemaName;
                sql += "LEFT OUTER JOIN " + schema_name + " AS " + field_id + " ON (" + table_id + "." + entity_tree.fieldName + " = " + field_id + "." + entity_tree.refFieldName + @")
";

                if (!entity_tree.children.IsNullOrEmpty()) sql += SqlJoinFk(entity_tree.children, field_id);
            }
            return sql;
        }

        public string Sql()
        {
            var sql = "SELECT ";
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

        protected string SqlFields()
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
            return @"FROM " + db.Entity(entityName).schemaName + " AS " + db.Entity(entityName).alias + @"
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



        /*
        Obtener arbol

        Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
        */
        public abstract List<Dictionary<string, T>> Tree<T>();

        public List<Dictionary<string, object>> ListDict()
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.ListDict();
        }

        public List<T> ListObject<T>()
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.ListObj<T>();
        }

        public Dictionary<string, object> Dict()
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Dict();
        }

        public T Object<T>()
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Obj<T>();

        }

        public List<T> Column<T>(string columnName)
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Column<T>(columnName);
        }

        public List<T> Column<T>(int columnValue = 0)
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Column<T>(columnValue);
        }
        public T Value<T>(string columnName)
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Value<T>(columnName);
        }

        public T Value<T>(int columnValue = 0)
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Value<T>(columnValue);
        }

        public abstract EntityQuery Clone();

        protected EntityQuery _Clone(EntityQuery eq)
        {
            eq.size = size;
            eq.where = where;
            eq.page = page;
            eq.parameters = parameters;
            eq.group = group;
            eq.having = having;
            eq.fields = fields;
            eq.select = select;
            eq.order = order;
            return eq;
        }
    }
}
