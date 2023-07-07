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

        public string? where { get; set; }

        public string? having { get; set; }

        public string? fields { get; set; } = "";

        public string? select { get; set; } = "";

        public string? order { get; set; } = "";

        public int? size { get; set; } = 100;

        public int? page { get; set; } = 1;

        public string group { get; set; } = "";

        public List<object> parameters = new List<object> { };


        public EntityQuery(Db _db, string _entityName)
        {
            db = _db;
            entityName = _entityName;
        }

        public EntityQuery Where(string w)
        {
            where = w;
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
            List<string> fields = _sql!.Replace("$", "").Split(',').ToList().Select(s => s.Trim()).ToList();
            string sql = "";
            
            foreach (var fieldName in fields)
            {
                if (fieldName.Contains('-'))
                {
                    List<string> ff = fieldName.Split("-").ToList();
                    sql += db.Mapping(db.Entity(entityName).relations[ff[0]].refEntityName, ff[0]).Map(ff[1]) + " AS '" + fieldName + "', ";
                } else
                    sql += db.Mapping(entityName).Map(fieldName) + " AS '" + fieldName + "', ";
            }
            sql = sql.RemoveLastIndex(',');
            return sql;
        }

        protected string Traduce(string _sql)
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
                    sql += Traduce_(_sql, field_start, i - field_start - 1);
                    field_start = -1;
                }

                sql += _sql[i];

            }

            if (field_start != -1)
                sql += Traduce_(_sql, field_start, _sql.Length - field_start - 1);


            return sql;
        }

        protected string Traduce_(string _sql, int fieldStart, int fieldEnd)
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

            return ff;
        }

        public EntityQuery Size(int _size)
        {
            size = _size;
            return this;
        }

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

            f += Concat(Traduce(this.group), @",
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
        Obtener todas las filas

        Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
        */
        public abstract List<Dictionary<string, object>> ListDict();

        public abstract List<T> ListObject<T>() where T : class, new();

        public abstract Dictionary<string, object> Dict();
        public abstract T Object<T>() where T : class, new();

        public abstract List<T> Column<T>(string columnName);

        public abstract List<T> Column<T>(int columnValue = 0);

        public abstract T Value<T>(string columnName);

        public abstract T Value<T>(int columnValue = 0);
        /*
        Obtener arbol

        Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
        */
        public abstract List<Dictionary<string, T>> Tree<T>();

        public abstract EntityQuery Clone();




        /*
        public abstract Dictionary<string, object> fetch_assoc();

        public abstract List<List<object>> fetch_row();

        public abstract List<object> fetch_column();

        public abstract List<Dictionary<string, object>> tree();

        public abstract List<Dictionary<string, string>> json_tree();

        public abstract List<Dictionary<string, object>> rel();

        public abstract List<Dictionary<string, string>> json_rel();

        public abstract Dictionary<string, object>? one_or_null();

        public abstract Dictionary<string, object>? one();

        public abstract Dictionary<string, object>? first_or_null();

        public abstract Dictionary<string, object>? first();

        public abstract Dictionary<string, string>? json_one();

        public abstract List<object>? column(int number = 0);
        */







    }

}