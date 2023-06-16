using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;
using Utils;
using SchemaJson;

namespace SchemaJsonSs
{
    public class BuildSchemaSs : BuildSchema
    {
        public BuildSchemaSs(Config config) : base(config)
        {
        }

        protected override List<String> GetTableNames()
        {
            using SqlConnection connection = new SqlConnection(Config.connection_string);
            connection.Open();
            using SqlCommand command = new SqlCommand();
            command.CommandText = @"
                SELECT TABLE_NAME
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG=@db_name";
            command.Connection = connection;
            command.Parameters.AddWithValue("db_name", Config.db_name);
            command.ExecuteNonQuery();
            using SqlDataReader reader = command.ExecuteReader();
            return DbDataReaderUtils.ColumnValues<string>(reader, "TABLE_NAME");
        }

        protected override List<Dictionary<string, string>> GetFieldsInfo(string tableName)
        {
            using SqlConnection connection = new SqlConnection(Config.connection_string);
            connection.Open();
            using SqlCommand command = new SqlCommand();
            command.CommandText = @"
                SELECT TABLE_NAME
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG=@db_name";
            command.Connection = connection;
            command.Parameters.AddWithValue("db_name", Config.db_name);
            command.ExecuteNonQuery();
            using SqlDataReader reader = command.ExecuteReader();
            return DbDataReaderUtils.ColumnValues<string>(reader, "TABLE_NAME");

        }

    }
}