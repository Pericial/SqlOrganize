using MySql.Data.MySqlClient;
using SqlOrganize;
using System.Data.Common;
using Utils;

namespace SqlOrganizeMy
{
    public class QueryMy : Query
    {

        public QueryMy(Db db, string entityName) : base(db, entityName)
        {
        }

        public override List<Dictionary<string, T>> Tree<T>()
        {
            throw new NotImplementedException();
        }

        protected override string SqlLimit()
        {
            if (size.IsNullOrEmpty()) return "";
            page = page.IsNullOrEmpty() ? 1 : page;
            return "LIMIT " + size + " OFFSET " + ((page - 1) * size) + @"
";
        }

        protected override string SqlOrder()
        {
            if (order.IsNullOrEmpty())
            {
                var o = db.Entity(entityName).orderDefault;
                order = o.IsNullOrEmpty() ? "" : string.Join(", ", o.Select(x => "$" + x));
            }

            return (order.IsNullOrEmpty()) ? "" : "ORDER BY " + Traduce(order!) + @"
";
        }

  
        public override List<Dictionary<string, object>> ListDict()
        {
            using MySqlConnection connection = new MySqlConnection((string)db.config.connectionString);
            connection.Open();
            string sql = Sql();
            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            for (var i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].IsList())
                {
                    var _parameters = (parameters[i] as List<object>).Select((x, j) => Tuple.Create($"@{i}_{j}", x));
                    sql = sql.ReplaceFirst("@" + i.ToString(), string.Join(",", _parameters.Select(x => x.Item1)));
                    foreach (var parameter in _parameters)
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                }
                else
                {
                    command.Parameters.AddWithValue(i.ToString(), parameters[i]);
                }
            }

            command.CommandText = sql;
            command.ExecuteNonQuery();
            using MySqlDataReader reader = command.ExecuteReader();

            return reader.Serialize();
        }

        public override List<T> ListObject<T>()
        {
            using MySqlConnection connection = new MySqlConnection((string)db.config.connectionString);
            connection.Open();
            string sql = Sql();
            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            for (var i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].IsList())
                {
                    var _parameters = (parameters[i] as List<object>).Select((x, j) => Tuple.Create($"@{i}_{j}", x));
                    sql = sql.ReplaceFirst("@" + i.ToString(), string.Join(",", _parameters.Select(x => x.Item1)));
                    foreach (var parameter in _parameters)
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                }
                else
                {
                    command.Parameters.AddWithValue(i.ToString(), parameters[i]);
                }
            }

            command.CommandText = sql;
            command.ExecuteNonQuery();
            using MySqlDataReader reader = command.ExecuteReader();

            return reader.ConvertToListOfObject<T>();
        }

        public override Dictionary<string, object> Dict()
        {
            throw new NotImplementedException();
        }

        public override T Object<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> Column<T>(string columnName)
        {
            using MySqlConnection connection = new MySqlConnection((string)db.config.connectionString);
            connection.Open();
            string sql = Sql();
            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            for (var i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].IsList())
                {
                    var _parameters = (parameters[i] as List<object>).Select((x, j) => Tuple.Create($"@{i}_{j}", x));
                    sql = sql.ReplaceFirst("@" + i.ToString(), string.Join(",", _parameters.Select(x => x.Item1)));
                    foreach (var parameter in _parameters)
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                }
                else
                {
                    command.Parameters.AddWithValue(i.ToString(), parameters[i]);
                }
            }

            command.CommandText = sql;
            command.ExecuteNonQuery();
            using MySqlDataReader reader = command.ExecuteReader();

            return reader.ColumnValues<T>(columnName);
        }

        public override List<T> Column<T>(int columnValue = 0)
        {
            using MySqlConnection connection = new MySqlConnection((string)db.config.connectionString);
            connection.Open();
            string sql = Sql();
            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            for (var i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].IsList())
                {
                    var _parameters = (parameters[i] as List<object>).Select((x, j) => Tuple.Create($"@{i}_{j}", x));
                    sql = sql.ReplaceFirst("@" + i.ToString(), string.Join(",", _parameters.Select(x => x.Item1)));
                    foreach (var parameter in _parameters)
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                }
                else
                {
                    command.Parameters.AddWithValue(i.ToString(), parameters[i]);
                }
            }

            command.CommandText = sql;
            command.ExecuteNonQuery();
            using MySqlDataReader reader = command.ExecuteReader();

            return reader.ColumnValues<T>(columnValue);
        }
        public override T Value<T>(string columnName)
        {
            throw new NotImplementedException();
        }

        public override T Value<T>(int columnValue = 0)
        {
            throw new NotImplementedException();
        }

        public override Query Clone()
        {
            var eq = new QueryMy(db, entityName);
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