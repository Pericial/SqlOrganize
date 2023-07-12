using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Utils;

namespace SchemaJson
{
    public abstract class BuildSchema
    {
        public Config Config { get; }

        public List<Table> Tables { get; } = new();

        /// <summary>
        /// Definir datos del esquema y arbol de relaciones
        /// </summary>
        /// <param name="config">Configuracion</param>
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
                table.FieldsData = GetFields(table.Name);

                List<string> fieldAlias = new List<string>(Config.reserved_alias);
                foreach (Field field in table.FieldsData)
                {
                    field.Alias = GetAlias(field.COLUMN_NAME, fieldAlias, 3);
                    fieldAlias.Add(field.Alias);
                    table.Fields.Add(field.COLUMN_NAME);


                    if (field.IS_FOREIGN_KEY == 1)
                        table.Fk.Add(field.COLUMN_NAME);
                    if (field.IS_PRIMARY_KEY == 1)
                        table.Pk.Add(field.COLUMN_NAME);
                    if (field.IS_UNIQUE_KEY == 1)
                        table.Unique.Add(field.COLUMN_NAME);
                    if (field.IS_NULLABLE == 0)
                        table.NotNull.Add(field.COLUMN_NAME);

                    switch (field.DATA_TYPE)
                    {
                        case "varchar":
                        case "char":
                        case "nchar":
                        case "nvarchar":
                        case "text":
                            field.DataType = "string";
                            break;
                        case "real":
                            field.DataType = "float";
                            break;
                        case "bit":
                            field.DataType = "bool";
                            break;
                        case "datetime":
                            field.DataType = "DateTime";
                            break;
                        case "smallint":
                            field.DataType = "int";
                            break;
                        default:
                            field.DataType = field.DATA_TYPE!;
                            break;
                    }
                }
                
                Tables.Add(table);
            }

            foreach (Table t in Tables)
            {
                var bt = new BuildTree(Tables, t.Name!);
                t.Tree = bt.Build();
            }
        }

        /// <summary>
        /// Definir alias para tablas y campos
        /// </summary>
        /// <param name="name">Nombre para el cual se definira alias</param>
        /// <param name="reserved">Palabras reservadas que no seran definidas como alias</param>
        /// <param name="length">Longitud del alias</param>
        /// <param name="separator">String utilizado para separar palabras de 'name'</param>
        /// <returns></returns>
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

            int c = 0;

            while (reserved.Contains(aliasAux))
            {
                c++;
                aliasAux = aliasAux.Substring(0, length - 1);
                aliasAux += c.ToString();
            }

            return aliasAux;
        }

        protected abstract List<String> GetTableNames();

        protected abstract List<Field> GetFields(string tableName);

        /*
        Generar _entities.json
        */
        public void FileEntities()
        {
            string file = @"{
";
            foreach(Table t in Tables)

            {
                file += @"  """ + t.Name + @""": {
    ""name"": """ + t.Name + @""",
    ""alias"": """ + t.Alias + @""",
    ""fields"": [""" + String.Join("\", \"", t.Fields) + @"""],
";
                if(t.Pk is not null) 
                    file += @"    ""pk"": [""" + String.Join("\", \"", t.Pk) + @"""],
";
                if (t.Fk.Count >  0)
                    file += @"    ""fk"": [""" + String.Join("\", \"", t.Fk) + @"""],
";
                if (t.Unique.Count > 0)
                    file += @"    ""unique"": [""" + String.Join("\", \"", t.Unique) + @"""],
";
   
                if (t.NotNull.Count > 0)
                    file += @"    ""notNull"": [""" + String.Join("\", \"", t.NotNull) + @"""],
";

                var contentAux = ContentTree(t);
                if (!contentAux.IsNullOrEmpty())
                    file += contentAux;

                contentAux = ContentRelations(t);
                if (!contentAux.IsNullOrEmpty())
                    file += contentAux;

                file = file.RemoveLastIndex(',');
                file += @"  },

";

            }

            file = file.RemoveLastIndex(',');
            file += "}";

            if(!Directory.Exists(Config.path))
                Directory.CreateDirectory(Config.path);

            if (File.Exists(Config.path + "entities.json"))
                File.Delete(Config.path + "entities.json");

            File.WriteAllText(Config.path + "entities.json", file);

        }

        public void FileFields()
        {
            if (!Directory.Exists(Config.path + "fields/"))
                Directory.CreateDirectory(Config.path + "fields/");
            foreach (Table t in Tables)
            {

                string file = @"{
";
                foreach (Field f in t.FieldsData) {

                    file += @"    """ + f.COLUMN_NAME + @""": {
        ""alias"": """ + f.Alias + @""",
        ""dataType"": """ + f.DataType + @""",
";
                if (!f.COLUMN_DEFAULT.IsNullOrEmpty())
                    file += @"        ""defaultValue"": """ + f.COLUMN_DEFAULT + @""",
";
            
                 if (!f.REFERENCED_COLUMN_NAME.IsNullOrEmpty())
                    file += @"        ""refEntityName"": """ + f.REFERENCED_COLUMN_NAME + @""",
";

                    file = file.RemoveLastIndex(',');
                file += @"    },

";
                }

                file = file.RemoveLastIndex(',');
                file += "}";




                if (File.Exists(Config.path + "fields/"+ t.Name + ".json"))
                    File.Delete(Config.path + "fields/" + t.Name + ".json");

                File.WriteAllText(Config.path + "fields/" + t.Name + ".json", file);
            }

        }


        public string ContentTree(Table t)
        {
            if (t.Tree.IsNullOrEmpty()) return "";
            string content = @"    ""tree"": {
" + FileTreeRecursive(t.Tree, "      ");
            content += @"    },
";
            //content = content.RemoveLastIndex(',');
            return content;
        }

        public string ContentRelations(Table t)
        {
            if (t.Tree.IsNullOrEmpty()) return "";
            string content = @"    ""relations"": {
" + FileRelationsRecursive(t.Tree, "      ");
            content = content.RemoveLastIndex(',');

            content += @"    },
";
            return content;
        }


        public void FileTree()
        {
            string content = @"{
";
            foreach (Table t in Tables)
            {
                if (t.Tree.IsNullOrEmpty()) continue;
                content += @"    """ + t.Name + @""": {
" + FileTreeRecursive(t.Tree, "        ");
                content += @"    },
";

            }

            content = content.RemoveLastIndex(',');
            content += "}";

            if (!Directory.Exists(Config.path))
                Directory.CreateDirectory(Config.path);

            if (File.Exists(Config.path + "tree.json"))
                File.Delete(Config.path + "tree.json");

            File.WriteAllText(Config.path + "tree.json", content);
        }

        protected string FileTreeRecursive(List<Tree> tree, string blankSpaces = "")
        {
            string content = "";
            foreach ( Tree t in tree)
            {
                content += blankSpaces + "\"" + t.FieldId + "\": { \"fieldName\": \"" + t.FieldName + "\", \"refEntityName\": \"" + t.RefEntityName + "\", \"refFieldName\": \"" + t.RefFieldName + @""", ""children"": {
";
                if (!t.Children.IsNullOrEmpty())
                     content += FileTreeRecursive(t.Children, blankSpaces + "    ");

                content += blankSpaces + @"}},
";
            }
            content = content.RemoveLastIndex(',');
            return content;
        }

        public void FileRelations()
        {
            string content = @"{
";
            foreach (Table t in Tables)
            {
                {
                    if (t.Tree.IsNullOrEmpty()) continue;
                    content += @"    """ + t.Name + @""": {
" + FileRelationsRecursive(t.Tree, "        ");
                    content = content.RemoveLastIndex(',');
                    content += @"    },
";
                    
                }
            }

            content = content.RemoveLastIndex(',');
            content += "}";

            if (!Directory.Exists(Config.path))
                Directory.CreateDirectory(Config.path);

            if (File.Exists(Config.path + "relations.json"))
                File.Delete(Config.path + "relations.json");

            File.WriteAllText(Config.path + "relations.json", content);
        }


        protected string FileRelationsRecursive(List<Tree> tree, string blankSpaces = "", string? parentId = null)
        {
            string content = "";
            string p = parentId.IsNullOrEmpty() ? "null" : "\""+parentId+"\"";
            foreach (Tree t in tree)
            {

                content += blankSpaces + "\"" + t.FieldId + "\": { \"fieldName\": \"" + t.FieldName + "\", \"refEntityName\": \"" + t.RefEntityName + "\", \"refFieldName\": \"" + t.RefFieldName + "\", \"parentId\": " + p + @"},
";
                if (!t.Children.IsNullOrEmpty())
                    content += FileRelationsRecursive(t.Children, blankSpaces + "    ", t.FieldId);
            }

            return content;
        }
    }
}

