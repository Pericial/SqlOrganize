using MySql.Data.MySqlClient;
using SqlOrganize;
using Utils;

namespace SqlOrganizeMy
{
    /// <summary>
    /// Ejecucion de consultas a la base de datos
    /// </summary>
    public class QueryMy : Query
    {
        public QueryMy(Db db) : base(db)
        {
        }

        protected void SqlExecute(MySqlConnection connection, MySqlCommand command)
        {
            connection.Open();
            command.Connection = connection;

            var j = parameters.Count;
            foreach(var (key, value) in parametersDict)
                while (sql.Contains("@"+key))
                {
                    sql = sql.ReplaceFirst("@" + key, "@"+ j.ToString());
                    parameters.Add(value);
                    j++;
                }

            for (var i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].IsList())
                {
                    //cuidado con el tipo de entrada, no se puede hacer cast de List<string> a List<object> por ejemplo
                    var _parameters = (parameters[i] as List<object>).Select((x, j) => Tuple.Create($"@{i}_{j}", x));
                    sql = sql.ReplaceFirst("@" + i.ToString(), string.Join(",", _parameters.Select(x => x.Item1)));
                    foreach (var parameter in _parameters)
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                }
                else
                {
                    var p = (parameters[i] == null) ? DBNull.Value : parameters[i];
                    command.Parameters.AddWithValue(i.ToString(), p);
                }
            }

            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public override List<Dictionary<string, object>> ListDict()
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.Serialize();
        }

        public override List<T> ListObj<T>()
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.ConvertToListOfObject<T>();
        }

        public override Dictionary<string, object> Dict()
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            return reader.SerializeRow();
        }

        public override T Obj<T>()
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            return reader.ConvertToObject<T>();
        }

        public override List<T> Column<T>(string columnName)
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.ColumnValues<T>(columnName);
        }

        public override List<T> Column<T>(int columnNumber = 0)
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.ColumnValues<T>(columnNumber);
        }
        public override T Value<T>(string columnName)
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            return reader.Read() ? (T)reader[columnName] : default(T);
        }

        public override T Value<T>(int columnNumber = 0)
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            return (reader.Read()) ? (T)reader.GetValue(columnNumber) : default(T);
        }

        public override void Exec()
        {
            using MySqlConnection connection = new MySqlConnection((string)db.config.connectionString);
            using MySqlCommand command = new MySqlCommand();
            SqlExecute(connection, command);
        }

        public override void Transaction()
        {
            sql = @"BEGIN;
" + sql + @"
COMMIT;";
            Exec();
        }
    }

}
