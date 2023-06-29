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

        public string? fieldsAs { get; set; } = "";

        public string? order { get; set; } = "";

        public int? size { get; set; } = 100;

        public int? page { get; set; } = 1;

        public string group { get; set; } = "";

        public List<object> parameters = new List<object> { };

        public string fetch = "ListDict";

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
            fields += string.Join(", ", db.tools(entityName).FieldNames().Select(x => "$" + x));
            return this;
        }

        public EntityQuery Fields(string f)
        {
            fields += f;
            return this;
        }

        public EntityQuery FieldsAs()
        {
            fieldsAs += "$id, "+string.Join(", ", db.tools(entityName).FieldNames().Select(x => "$" + x));
            return this;
        }

        public EntityQuery FieldsAs(string f)
        {
            fieldsAs += f;
            return this;
        }

        public EntityQuery Parameters(params object[] parameters)
        {
            this.parameters.AddRange(parameters.ToList());
            return this;
        }

        public EntityQuery Fetch(string fetch)
        {
            this.fetch = fetch;
            return this;
        }

        protected string Traduce(string _sql, bool flag_as = false)
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
                    sql += Traduce_(_sql, flag_as, field_start, i - field_start - 1);
                    field_start = -1;
                }

                sql += _sql[i];

            }

            if (field_start != -1)
                sql += Traduce_(_sql, flag_as, field_start, _sql.Length - field_start - 1);


            return sql;
        }

        protected string Traduce_(string _sql, bool flag_as, int field_start, int field_end)
        {
            var field_name = _sql.Substring(field_start + 1, field_end);
            var f = db.ExplodeField(entityName, field_name);

            var ff = db.Mapping(f["entityName"], f["fieldId"]).map(f["fieldName"]);
            if (flag_as)
            {
                var a = (f["fieldId"].IsNullOrEmpty()) ? f["fieldName"] : field_name;
                ff += " AS '" + a + "'";
            }
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
            if (db.Entity(entityName).tree.IsNullOrEmpty())
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
            if(this.fields.IsNullOrEmpty() && this.fieldsAs.IsNullOrEmpty() && this.group.IsNullOrEmpty())
                this.FieldsAs();

            string f = Concat(Traduce(this.fields), @"
");
            var p = Traduce(this.fieldsAs, true);
            f += Concat(p, @",
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

        public object Exec<T>() { 
            switch (fetch.ToLower()) {
                case "listdict":
                case "list_dict":
                case "list":
                    return ListDict();

                case "dict":
                    return Dict();

                case "column":
                    return Column<T>(0);

                case "value":
                    return Value<T>();

                default:
                    throw new Exception("No se encuentra definido el Fetch");
            }

        }

        public override string ToString()
        {
            return Regex.Replace(entityName + where + having + fields + fieldsAs + order + size + page + JsonConvert.SerializeObject(parameters), @"\s+", "");
        }


        /*
        Obtener todas las filas

        Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
        */
        public abstract List<Dictionary<string, object>> ListDict();

        public abstract List<T> ListObject<T>() where T : class, new();

        public abstract Dictionary<string, object> Dict();
        public abstract T Object<T>() where T : class, new();

        public abstract T Column<T>(string columnName);

        public abstract T Column<T>(int columnValue = 0);

        public abstract T Value<T>(string columnName);

        public abstract T Value<T>(int columnValue = 0);
        /*
        Obtener arbol

        Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
        */
        public abstract List<Dictionary<string, T>> Tree<T>();

  

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