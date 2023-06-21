using System.Text;
using System.Text.RegularExpressions;
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

            List<string> tableAlias = new List<string>(Config.reserved_alias);

            foreach ( string tableName in GetTableNames())
            {
                Table table = new();
                table.Name = tableName;
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

            foreach (Table t in Tables)
            {
                var bt = new BuildTree(Tables, t.Name!);
                t.Tree = bt.Build();
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
        Generar _entities.json
        */
        public void Entities()
        {
            string file = @"{
";
            foreach(Table t in Tables)

            {
                file += @"    """ + t.Name + @""": {
        ""name"": """ + t.Name + @""",
        ""alias"": """ + t.Alias + @""",
        ""nf"": [""" + String.Join("\", \"", t.Nf) + @"""],
";
                if(t.Pk is not null) 
                    file += @"        ""pk"": """ + t.Pk + @""",
";
                if (t.Fk.Count >  0)
                    file += @"        ""fk"": [""" + String.Join("\", \"", t.Fk) + @"""],
";
                if (t.Unique.Count > 0)
                    file += @"        ""unique"": [""" + String.Join("\", \"", t.Unique) + @"""],
";
                if (t.UniqueMultiple.Count > 0)
                    file += @"        ""uniqueMultiple"": [""" + String.Join("\", \"", t.UniqueMultiple) + @"""],
";

                file = file.RemoveLastIndex(',');
                file += @"    },

";

            }

            file = file.RemoveLastIndex(',');
            file += "}";

            if(!Directory.Exists(Config.path))
                Directory.CreateDirectory(Config.path);


            if (File.Exists(Config.path + "_entities.json"))
                File.Delete(Config.path + "_entities.json");

            File.WriteAllText(Config.path + "_entities.json", file);

        }
        
        public void FileTree()
        {
            string content = "";
            foreach (Table t in Tables)
            {
                var tree = new TreeTable(Tables, t.Name!);
                content += tree.CreateFile();
            }
            if (!Directory.Exists(Config.path))
                Directory.CreateDirectory(Config.path);

            if (File.Exists(Config.path + "tree.json"))
                File.Delete(Config.path + "tree.json");

            File.WriteAllText(Config.path + "tree.json", content);
        }
    }
}