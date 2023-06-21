using System.Text;
using Utils;

namespace SchemaJson
{
    public abstract class BuildSchema
    {
        public Config Config { get; }
        public List<Table> Tables { get; } = new();

        public BuildSchema(Config config)
        {
            Config = config;

            foreach ( string tableName in GetTableNames())
            {
                Table table = new();
                table.Name = tableName;

                List<string> tableAlias = new List<string>(Config.reserved_alias);
                table.Alias = GetAlias(tableName, tableAlias, 4);
                tableAlias.Add(table.Alias);

                table.Fields = GetFields(table.Name);

                List<string> fieldAlias = new List<string>(Config.reserved_alias);
                foreach (Field field in table.Fields)
                {
                    field.Alias = GetAlias(field.COLUMN_NAME, fieldAlias, 3);
                    fieldAlias.Add(field.Alias);

                    if (field.IS_FOREIGN_KEY == 1)
                        table.Fk.Add(field.COLUMN_NAME);
                    else
                        table.Nf.Add(field.COLUMN_NAME);
                    if (field.IS_PRIMARY_KEY == 1)
                        table.PkAux.Add(field.COLUMN_NAME);
                    if (field.IS_UNIQUE_KEY == 1)
                        table.Unique.Add(field.COLUMN_NAME);
                }

                if(table.PkAux.Count == 1)
                    table.Pk = table.PkAux[0];
                else
                    table.UniqueMultiple = table.PkAux;

                Tables.Add(table);
                
            }

        }


        protected string GetAlias(string name, List<string> reserved, int length = 3, string separator = "_")
        {
            var n = name.Trim('_');
            string[] words = n.Split(separator);

            string nameAux = "";

            if (n.Length < length) 
                length = n.Length;

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

        protected abstract List<Field> GetFields(string tableName);

        /*
        Write text files:
        - https://www.csharptutorial.net/csharp-file/csharp-write-text-files/
        - https://www.c-sharpcorner.com/UploadFile/mahesh/create-a-text-file-in-C-Sharp/
        - https://stackoverflow.com/questions/32452300/could-not-find-a-part-of-the-path-c-sharp
        - https://www.educative.io/answers/how-to-check-if-a-directory-exists-in-c-sharp
        */
        public void Entities()
        {
            string file = @"{
";
            foreach(Table t in Tables)

            {
                file += "    \"" + t.Name + @""": {
        ""name"": """ + t.Name + @"""
        ""alias"": """ + t.Alias + @"""
        ""pk"": """ + t.Pk + @"""
        ""nf"": """ + String.Join("\", \"", t.Nf) + @"""
        ""fk"": """ + String.Join("\", \"", t.Fk) + @"""
        ""unique"": """ + String.Join("\", \"", t.Unique) + @"""
        ""uniqueMultiple"": """ + String.Join("\", \"", t.UniqueMultiple) + @"""
    }

";

            }

            file += "}";

            if(!Directory.Exists(Config.path))
                Directory.CreateDirectory(Config.path);


            if (File.Exists(Config.path + "_entities.json"))
                File.Delete(Config.path + "_entities.json");

            File.WriteAllText(Config.path + "_entities.json", file);

        }



    }
}