using MySql.Data.MySqlClient;
using SqlOrganize;
using System.Data.Common;
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

        public override List<Dictionary<string, object>> ColOfDict()
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.Serialize();
        }

        public override List<T> ColOfObj<T>()
        {
            using MySqlConnection connection = new(db.config.connectionString);
            using MySqlCommand command = new();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.ColOfObj<T>();
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
            return reader.Obj<T>();
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

        protected override void AddWithValue(DbCommand command, string columnName, object value)
        {
            (command as MySqlCommand)!.Parameters.AddWithValue(columnName, value);
        }
    }

}
