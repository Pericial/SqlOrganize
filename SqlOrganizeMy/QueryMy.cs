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
                    command.Parameters.AddWithValue(i.ToString(), parameters[i]);
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

        public override List<T> Column<T>(int columnValue = 0)
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
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
 