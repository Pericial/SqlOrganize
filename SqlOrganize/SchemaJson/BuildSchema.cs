using System.Text;
using Utils;

namespace SchemaJson
{
    public abstract class BuildSchema
    {
        public Config Config { get; }
        public List<string> ReservedTableNames { get; } = new();
        public List<Dictionary<string, string>> TableInfo { get; } = new();

        public BuildSchema(Config config)
        {
            Config = config;

            foreach ( string tableName in GetTableNames())
            {
                var t = new Table();
                t.Name = tableName;

                List<string> tableAlias = new List<string>(Config.reserved_alias);
                t.Alias = GetAlias(tableName, tableAlias, 4);
                tableAlias.Add(t.Alias);

                List<Field> fieldsInfo = GetFieldsInfo(t.Name);
            }
        }

     
        protected string GetAlias(string name, List<string> reserved, int length = 3, string separator = "_")
        {
            string[] words = name.Trim('_').Split(separator);

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

        protected abstract List<String> GetTableNames();

        protected abstract List<Field> GetFieldsInfo(string tableName);



    }
}