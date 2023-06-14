using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Common;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SqlOrganize
{

    /*
    Consulta y formato de datos

    Efectua la consulta a la base de datos y la transforma en el formato solicitado
    */
    public abstract class EntityQuery
    {
        public Db db { get; }
        public string entity_name { get; }


        /*
        Debe respetar el formato del motor de base de datos
        Los fields se traducen con los metodos de mapeo, deben indicarse con el prefijo $
        . indica aplicacion de funcion de agregacion
        - indica que pertenece a una relacion
        Ej "($ingreso = %p1) AND ($persona-nombres = %p1)"
        */
        public string? where { get; set; }

        /*
        Debe respetar el formato del motor de base de datos
        Los fields se traducen con los metodos de mapeo, deben indicarse con el prefijo $
        . indica aplicacion de funcion de agregacion
        - indica que pertenece a una relacion
        Ej "($ingreso = %p1) AND ($persona-nombres = %p1)"
        */
        public string? having { get; set; }

        /*
        Los fields deben estar definidos en el mapping field, se realizará la 
        traducción correspondiente
        Para traducir se les debe indicar el prefijo "$"
        . indica aplicacion de funcion de agregacion
        - indica que pertenece a una relacion
        Ej "$nombres, $curso-horas_catedra.sum" => Se traduce a "alum.nombres as 'nombres', SUM(cur.horas_catedra) AS 'curso-horas_catedra.sum'"
        */
        public string? fields { get; set; } = "";
        /*
        Similar a fields, pero no se aplica renombramiento

        "$nombres, $curso-horas_catedra.sum" => Se traduce a "alum.nombres, SUM(cur.horas_catedra)"
        */

        public string? fields_as { get; set; } = "";


        public string? order { get; set; } = "";
        public int? size { get; set; } = 100;
        public int? page { get; set; } = 1;

        /*
        Similar a fields pero campo de agrupamiento
        */
        public string group { get; set; } = "";

        public List<object> parameters = new List<object> { };

        public string fetch = "All";

        public EntityQuery(Db _db, string _entity_name)
        {
            db = _db;
            entity_name = _entity_name;
        }


        public EntityQuery Where(string w)
        {
            where = w;
            return this;
        }


        public EntityQuery Fields()
        {
            fields += string.Join(", ", db.tools(entity_name).field_names().Select(x => "$" + x));
            return this;
        }

        public EntityQuery Fields(string f)
        {
            fields += f;
            return this;
        }


        public EntityQuery FieldsAs()
        {
            fields_as += string.Join(", ", db.tools(entity_name).field_names().Select(x => "$" + x));
            return this;
        }

        public EntityQuery FieldsAs(string f)
        {
            fields_as += f;
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


        protected string traduce(string _sql, bool flag_as = false)
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
                    sql += traduce_(_sql, flag_as, field_start, i - field_start - 1);
                    field_start = -1;
                }

                sql += _sql[i];

            }

            if (field_start != -1)
            {
                sql += traduce_(_sql, flag_as, field_start, _sql.Length - field_start - 1);
            }


            return sql;
        }

        protected string traduce_(string _sql, bool flag_as, int field_start, int field_end)
        {
            var field_name = _sql.Substring(field_start + 1, field_end);
            var f = db.explode_field(entity_name, field_name);

            var ff = db.mapping(f["entity_name"], f["field_id"]).map(f["field_name"]);
            if (flag_as)
            {
                var a = (f["field_id"].IsNullOrEmpty()) ? f["field_name"] : field_name;
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

        protected string sql_join()
        {
            string sql = "";
            if (db.tree.ContainsKey(entity_name))
                sql += sql_join_fk(db.tree[entity_name], "");
            return sql;
        }

        protected string sql_join_fk(Dictionary<string, EntityTree> tree, string table_id)
        {
            if (table_id.IsNullOrEmpty())
                table_id = db.entity(entity_name).alias;

            string sql = "";
            string schema_name;
            foreach (var (field_id, entity_tree) in tree) {
                schema_name = db.entity(entity_tree.entity_name).schema_name;
                sql += "LEFT OUTER JOIN " + schema_name + " AS " + field_id + " ON (" + table_id + "." + entity_tree.field_name + " = " + field_id + "." + entity_tree.field_ref_name + @")
";

                if (!entity_tree.children.IsNullOrEmpty()) sql += sql_join_fk(entity_tree.children, field_id);
            }
            return sql;
        }

        public string Sql()
        {
            var sql = "SELECT ";
            sql += sql_fields();
            sql += sql_from();
            sql += sql_join();
            sql += sql_where();
            sql += sql_group();
            sql += sql_having();
            sql += sql_order();
            sql += sql_limit();

            return sql;
        }

        protected string sql_where()
        {
            return (where.IsNullOrEmpty()) ? "" : "WHERE " + traduce(where!) + @"
";
        }

        protected string sql_group()
        {
            return (group.IsNullOrEmpty()) ? "" : "GROUP BY " + traduce(group!) + @"
";
        }

        protected string sql_having()
        {
            return (having.IsNullOrEmpty()) ? "" : "HAVING " + traduce(having!) + @"
";
        }

        protected abstract string sql_order();


        protected string sql_fields()
        {
            if(this.fields.IsNullOrEmpty() && this.fields_as.IsNullOrEmpty() && this.group.IsNullOrEmpty())
                this.FieldsAs();

            string f = concat(traduce(this.fields), @"
");
            var p = traduce(this.fields_as, true);
            f += concat(p, @",
", "", !f.IsNullOrEmpty());

            f += concat(traduce(this.group), @",
", "", !f.IsNullOrEmpty());

            return f + @"
";
        }

        protected string sql_from()
        {
            return @"FROM " + db.entity(entity_name).schema_name + " AS " + db.entity(entity_name).alias + @"
";
        }

        protected abstract string sql_limit();
       

        protected string concat(string? value, string connect_true, string connect_false = "", bool connect_cond = true)
        {
            if (value.IsNullOrEmpty()) return "";

            string connect = "";
            if (connect_cond)
                connect = connect_true;
            else
                connect = connect_false;

            return connect + " " + value;
        }

        public object Exec() { 
            switch (fetch) {
                case "All":
                    return All();

                case "Tree":
                    return Tree();

                default:
                    throw new Exception("No se encuentra definido el Fetch");
            }

        }

        public override string ToString()
        {
            return Regex.Replace(entity_name + where + having + fields + fields_as + order + size + page + JsonConvert.SerializeObject(parameters), @"\s+", "");
        }


        /*
        Obtener todas las filas

        Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
        */
        public abstract List<Dictionary<string, object>> All();

        /*
        Obtener arbol

        Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
        */
        public abstract List<Dictionary<string, object>> Tree();


        /*
        Obtener todas las filas

        Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
        */


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