using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Utils;

namespace ModelOrganize
{
    public abstract class BuildModel
    {
        public Config Config { get; }

        public List<Table> Tables { get; } = new();

        public Dictionary<string, Entity> entities { get; } = new();

        public Dictionary<string, Dictionary<string, Field>> fields { get; set; } = new();




        /// <summary>
        /// Definir datos del esquema y arbol de relaciones
        /// </summary>
        /// <param name="config">Configuracion</param>
        public BuildModel(Config config)
        {
            Config = config;

            #region Definicion de tables y columns
            List<string> tableAlias = new List<string>(Config.reservedAlias);

            foreach (string tableName in GetTableNames())
            {
                Table table = new();
                table.Name = tableName;
                table.Alias = GetAlias(tableName, tableAlias, 4);
                tableAlias.Add(table.Alias);
                table.Columns = GetColumns(table.Name);

                List<string> fieldAlias = new List<string>(Config.reservedAlias);
                foreach (Column field in table.Columns)
                {
                    field.Alias = GetAlias(field.COLUMN_NAME, fieldAlias, 3);
                    fieldAlias.Add(field.Alias);
                    table.ColumnNames.Add(field.COLUMN_NAME);


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
                        case "tinyint":
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
            #endregion

            #region Definicion de entities
            foreach (Table t in Tables)
            {
                if (Config.reservedEntities.Contains(t.Name!))
                    continue;

                List<string> fieldIds = new(); //lista de fieldIds de la tabla para no repetir

                var e = new Entity();
                e.name = t.Name!;
                e.alias = t.Alias!;
                e.fields = t.ColumnNames;
                e.pk = t.Pk;
                e.fk = t.Fk;
                e.unique = t.Unique;
                e.notNull = t.NotNull;
                entities[e.name!] = e;
            }
            #endregion

            #region Definir id de entities
            foreach (var (name, e) in entities)
                e.id = DefineId(e);
            #endregion

            #region Redefinicion de entities en base a configuracion
            if (File.Exists(config.modelPath + "entities.json"))
            {
                using (StreamReader r = new StreamReader(config.modelPath + "entities.json"))
                {
                    Dictionary<string, EntityAux> entitiesAux = JsonConvert.DeserializeObject<Dictionary<string, EntityAux>>(r.ReadToEnd())!;
                    foreach (KeyValuePair<string, EntityAux> e in entitiesAux)
                    {
                        if (!entities.ContainsKey(e.Key))
                            continue;

                        CollectionUtils.CopyValues(entities[e.Key], e.Value);

                        var f = new List<string>();
                        f.AddRange(entities[e.Key].fields);
                        f.AddRange(e.Value.fieldsAdd);
                        f = f.Except(e.Value.fieldsSub).ToList();
                        entities[e.Key].fields = f;

                        f = new List<string>();
                        f.AddRange(entities[e.Key].fk);
                        f.AddRange(e.Value.fkAdd);
                        f = f.Except(e.Value.fkSub).ToList();
                        entities[e.Key].fk = f;

                        f = new List<string>();
                        f.AddRange(entities[e.Key].unique);
                        f.AddRange(e.Value.uniqueAdd);
                        f = f.Except(e.Value.uniqueSub).ToList();
                        entities[e.Key].unique = f;

                        f = new List<string>();
                        f.AddRange(entities[e.Key].notNull);
                        f.AddRange(e.Value.notNullAdd);
                        f = f.Except(e.Value.notNullSub).ToList();
                        entities[e.Key].notNull = f;

                        f = new List<string>();
                        f.AddRange(entities[e.Key].uniqueMultiple);
                        f.AddRange(e.Value.uniqueMultipleAdd);
                        f = f.Except(e.Value.uniqueMultipleSub).ToList();
                        entities[e.Key].uniqueMultiple = f;
                    }
                }
            }
            #endregion

            #region Definicion de fields
            foreach (Table t in Tables)
            {
                if (Config.reservedEntities.Contains(t.Name!))
                    continue;

                foreach (Column c in t.Columns)
                {
                    if (!entities[t.Name!]!.fields.Contains(c.COLUMN_NAME))
                        continue;

                    var f = new Field();
                    f.entityName = t.Name!;
                    f.name = c.COLUMN_NAME;
                    f.alias = c.Alias;
                    f.dataType = c.DataType;
                    if (!c.COLUMN_DEFAULT.IsNullOrEmpty())
                        f.defaultValue = c.COLUMN_DEFAULT;

                    if (!c.REFERENCED_TABLE_NAME.IsNullOrEmpty())
                        f.refEntityName = c.REFERENCED_TABLE_NAME;

                    if (!c.REFERENCED_COLUMN_NAME.IsNullOrEmpty()) //se compara por REFERENCED_TABLE_NAME para REFERENCED_COLUMN_NAME
                        f.refFieldName = c.REFERENCED_COLUMN_NAME;

                    f.notNull = (c.IS_NULLABLE == 0) ? false : true;

                    f.checks = new()
                    {
                        { "type", f.dataType },
                        { "required", f.notNull },
                    };

                    if(f.dataType == "string")
                    {
                        f.resets = new()
                        {
                            { "trim", " " },
                            { "removeMultipleSpaces", true },
                        };
                    }
                    if (!fields.ContainsKey(t.Name!))
                        fields[t.Name!] = new();

                    fields[t.Name!][f.name] = f;

                    
                }

            }
            #endregion

            #region Definicion de tree y relations de entities
            foreach (var (name, e) in entities)
            {
                var bet = new BuildEntityTree(entities, fields, e.name!);
                e.tree = bet.Build();
                RelationsRecursive(e.relations, e.tree);

            }
            #endregion

            #region Redefinicion de fields en base a configuracion
            foreach (string entityName in entities.Keys)
                if (!fields.ContainsKey(entityName))
                    if (File.Exists(config.modelPath + "fields/" + entityName + ".json"))
                        using (StreamReader r = new StreamReader(config.modelPath + "fields/" + entityName + ".json"))
                        {
                            Dictionary<string, Field> fieldsAux = JsonConvert.DeserializeObject<Dictionary<string, Field>>(r.ReadToEnd())!;
                            foreach (KeyValuePair<string, Field> e in fieldsAux)
                            {
                                if (fields[entityName].ContainsKey(e.Key))
                                {
                                    CollectionUtils.CopyValues(fields[entityName][e.Key], e.Value);
                                }
                            }
                        }
            #endregion



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

        protected abstract List<Column> GetColumns(string tableName);


        protected void RelationsRecursive(Dictionary<string, EntityRelation> rel, Dictionary<string, EntityTree> tree, string? parentId = null)
        {
            foreach (var (fieldId, t) in tree)
            {
                EntityRelation r = new()
                {
                    fieldName = t.fieldName,
                    refEntityName = t.refEntityName,
                    refFieldName = t.refFieldName,
                    parentId = parentId
                };
                rel[fieldId] = r;
                RelationsRecursive(rel, t.children, fieldId);
            }
        }


        public void CreateFileEntitites()
        {
            if (!Directory.Exists(Config.path))
                Directory.CreateDirectory(Config.path);

            if (File.Exists(Config.path + "entities.json"))
                File.Delete(Config.path + "entities.json");

            var file = JsonConvert.SerializeObject(entities, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Config.path + "entities.json", file);
        }

        public void CreateFileFields()
        {
            if (!Directory.Exists(Config.path + "fields/"))
                Directory.CreateDirectory(Config.path + "fields/");

            foreach(var (entityName, field) in fields)
            {
                if (File.Exists(Config.path + entityName + ".json"))
                    File.Delete(Config.path + entityName + ".json");

                var file = JsonConvert.SerializeObject(fields[entityName], Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Config.path + "fields/" + entityName + ".json", file);
            }

        }


        protected List<string> DefineId(Entity entity)
        {
            if (entity.pk.Count == 1)
                return entity.pk;

            foreach (string f in entity.unique)
                if (entity.notNull.Contains(f))
                    return new List<string> { f };


            if (entity.uniqueMultiple.Count > 1)
            {
                bool uniqueMultipleFlag = true;
                foreach (string f in entity.uniqueMultiple)
                {

                    if (!entity.uniqueMultiple.Contains(f))
                    {
                        uniqueMultipleFlag = false;
                        break;
                    }
                }

                if (uniqueMultipleFlag)
                {
                    return entity.uniqueMultiple;
                }
            }

            if (entity.notNull.Count > 1)
            {
                return entity.notNull;
            }

            return entity.fields;
        }

    }
}

