using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaJsonMy
{
    internal class SchemaJsonMy
    {

        protected List<String> GetTableNames()
        {
            using MySqlConnection connection = new MySqlConnection(Config.connection_string);
            connection.Open();
            using MySqlCommand command = new();
            command.CommandText = @"
                SELECT TABLE_NAME
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG=@db_name";
            command.Connection = connection;
            command.Parameters.AddWithValue("db_name", Config.db_name);
            command.ExecuteNonQuery();
            using MySqlDataReader reader = command.ExecuteReader();
            return DbDataReaderUtils.ColumnValues<string>(reader, "TABLE_NAME");
        }
    }
}
