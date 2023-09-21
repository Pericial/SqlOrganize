using Microsoft.Data.SqlClient;
using SqlOrganize;
using System.Data.Common;
using Utils;

namespace SqlOrganizeSs
{
    /// <summary>
    /// Ejecucion de consultas a la base de datos
    /// </summary>
    public class QuerySs : Query
    {
        public QuerySs(Db db) : base(db)
        {
        }

        protected override void AddWithValue(DbCommand command, string columnName, object value)
        {
            (command as SqlCommand)!.Parameters.AddWithValue(columnName, value);
        }

        public override List<Dictionary<string, object>> ColOfDict()
        {
            using SqlConnection connection = new(db.config.connectionString);
            using SqlCommand command = new();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();
            return reader.Serialize();
        }

        public override List<T> ColOfObj<T>()
        {
            using SqlConnection connection = new(db.config.connectionString);
            using SqlCommand command = new();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();
            return reader.ColOfObj<T>();
        }

        public override Dictionary<string, object> Dict()
        {
            using SqlConnection connection = new(db.config.connectionString);
            using SqlCommand command = new();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            return reader.SerializeRow();
        }

        public override T Obj<T>()
        {
            using SqlConnection connection = new(db.config.connectionString);
            using SqlCommand command = new();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            return reader.Obj<T>();
        }

        public override IEnumerable<T> Column<T>(string columnName)
        {
            using SqlConnection connection = new(db.config.connectionString);
            using SqlCommand command = new();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();
            return reader.ColumnValues<T>(columnName);
        }

        public override IEnumerable<T> Column<T>(int columnNumber = 0)
        {
            using SqlConnection connection = new(db.config.connectionString);
            using SqlCommand command = new();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();
            return reader.ColumnValues<T>(columnNumber);
        }
        public override T Value<T>(string columnName)
        {
            using SqlConnection connection = new(db.config.connectionString);
            using SqlCommand command = new();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            return reader.Read() ? (T)reader[columnName] : default(T);
        }

        public override T Value<T>(int columnNumber = 0)
        {
            using SqlConnection connection = new(db.config.connectionString);
            using SqlCommand command = new();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            return (reader.Read()) ? (T)reader.GetValue(columnNumber) : default(T);
        }

        public override void Exec()
        {
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            using SqlCommand command = new SqlCommand();
            SqlExecute(connection, command);
        }

        public override void Transaction()
        {
            sql = @"BEGIN TRAN; 
" + sql + @"
COMMIT TRAN;";
            Exec();
        }
    }

}
