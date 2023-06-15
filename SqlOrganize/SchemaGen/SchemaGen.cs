using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using Utils;

namespace SchemaGen
{
    public class SchemaGen
    {
        Config Config { get; }
        public List<string> ReservedTableNames { get; } = new();
        public List<Table> TableInfo { get; } = new();


        public SchemaGen(Config config)
        {
            Config = config;

            foreach ( string tableName in GetTableNames())
            {
                var t = new Table();
                t.Name = tableName;
                Aliases.Existent.Clear();
                t.Alias = Aliases.GetAlias(tableName, 4);

            }

        }

        protected List<String> GetTableNames()
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

        protected void AddFields(Table table)
        {

        }

        protected object GetFieldsInfo()
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