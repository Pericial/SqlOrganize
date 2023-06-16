using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;
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

                List<string> tableAlias = new List<string>(Config.reserved_alias);
                t.Alias = GetAlias(tableName, tableAlias, 4);
                tableAlias.Add(t.Alias);

                List<Dictionary<string, string>> fieldsInfo = GetFieldsInfo(t.Name);
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

        protected List<Dictionary<string, string>> GetFieldsInfo(string tableName)
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

        protected string GetAlias(string name, List<string> reserved, int length = 3, string separator = "_")
        {
            string[] words = name.Split(separator);

            string nameAux = "";
            if (words.Length > 1)
                foreach (string word in words)
                    nameAux += word[0];

            string aliasAux = name.Substring(0, length);

            char c = 'a';

            while (reserved.Contains(aliasAux))
            {
                if (!Char.IsLetter(c) && !Char.IsNumber(c))
                {
                    c = 'a';
                    length++;
                    name.Substring(0, length);
                }
                else if (aliasAux.Length < length)
                    aliasAux += c;
                else
                {
                    StringBuilder sb = new StringBuilder(aliasAux);
                    sb[length - 1] = c;
                    aliasAux = sb.ToString();
                }

                c = c.GetNextChar();
            }

            return aliasAux;
        }



    }
}