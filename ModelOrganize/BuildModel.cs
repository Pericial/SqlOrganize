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
                if (Config.reservedEntities.Contains(tableName!))
                    continue;

                Table table = new();
                table.Name = tableName;
                table.Alias = GetAlias(tableName, tableAlias, 4);
                tableAlias.Add(table.Alias);
                table.Columns = GetColumns(table.Name);

                List<string> fieldAlias = new List<string>(Config.reservedAlias);
                foreach (Column col in table.Columns)
                {
                    if (col.IS_FOREIGN_KEY == 1 && !Config.reservedEntities.Contains(col.REFERENCED_TABLE_NAME!)) {
                        string aliasSource = (Config.aliasSource == "field_name") ? col.COLUMN_NAME : col.REFERENCED_TABLE_NAME;
                        col.Alias = GetAlias(aliasSource, fieldAlias, 3);
                        fieldAlias.Add(col.Alias);
                    }
                    table.ColumnNames.Add(col.COLUMN_NAME);

                    if (col.IS_FOREIGN_KEY == 1 && !Config.reservedEntities.Contains(col.REFERENCED_TABLE_NAME!))
                        table.Fk.Add(col.COLUMN_NAME);
                    if (col.IS_PRIMARY_KEY == 1)
                        table.Pk.Add(col.COLUMN_NAME);
                    if (col.IS_UNIQUE_KEY == 1)
                        table.Unique.Add(col.COLUMN_NAME);
                    if (col.IS_NULLABLE == 0)
                        table.NotNull.Add(col.COLUMN_NAME);

                    switch (col.DATA_TYPE)
                    {
                        case "varchar":
                        case "char":
                        case "nchar":
                        case "nvarchar":
                        case "text":
                            col.DataType = "string";
                            break;
                        case "real":
                            col.DataType = "float";
                            break;
                        case "bit":
                        case "tinyint":
                            col.DataType = "bool";
                            break;
                        case "datetime":
                            col.DataType = "DateTime";
                            break;
                        case "smallint":
                            col.DataType = "int";
                            break;
                        default:
                            col.DataType = col.DATA_TYPE!;
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
           if (File.Exists(config.configPath + "entities.json"))
            {
                using (StreamReader r = new StreamReader(config.configPath + "entities.json"))
                {
                    Dictionary<string, EntityAux> entitiesAux = JsonConvert.DeserializeObject<Dictionary<string, EntityAux>>(r.ReadToEnd())!;
                    foreach (KeyValuePair<string, EntityAux> e in entitiesAux)
                    {
                        if (!entities.ContainsKey(e.Key))
                            continue;

                        CollectionUtils.CopyValues(entities[e.Key], e.Value);

                        var f = new List<string>();
                        f.AddRange(entities[e.Key].fields);
                        if(!e.Value.fieldsAdd.IsNullOrEmpty())
                            f.AddRange(e.Value.fieldsAdd);
                        if (!e.Value.fieldsSub.IsNullOrEmpty())
                            f = f.Except(e.Value.fieldsSub).ToList();
                        entities[e.Key].fields = f;

                        entities[e.Key].fk = entities[e.Key].fk.Intersect(f).ToList();
                        entities[e.Key].unique = entities[e.Key].unique.Intersect(f).ToList();
                        entities[e.Key].notNull = entities[e.Key].notNull.Intersect(f).ToList();
                        entities[e.Key].uniqueMultiple = entities[e.Key].uniqueMultiple.Intersect(f).ToList();

                        f = new List<string>();
                        f.AddRange(entities[e.Key].fk);
                        if (!e.Value.fkAdd.IsNullOrEmpty())
                            f.AddRange(e.Value.fkAdd);
                        if (!e.Value.fkSub.IsNullOrEmpty())
                            f = f.Except(e.Value.fkSub).ToList();
                        entities[e.Key].fk = f;

                        f = new List<string>();
                        f.AddRange(entities[e.Key].unique);
                        if (!e.Value.uniqueAdd.IsNullOrEmpty())
                            f.AddRange(e.Value.uniqueAdd);
                        if (!e.Value.uniqueSub.IsNullOrEmpty())
                            f = f.Except(e.Value.uniqueSub).ToList();
                        entities[e.Key].unique = f;

                        f = new List<string>();
                        f.AddRange(entities[e.Key].notNull);

                        if (!e.Value.notNullAdd.IsNullOrEmpty())
                            f.AddRange(e.Value.notNullAdd);

                        if (!e.Value.notNullSub.IsNullOrEmpty())
                            f = f.Except(e.Value.notNullSub).ToList();

                        entities[e.Key].notNull = f;

                        f = new List<string>();
                        f.AddRange(entities[e.Key].uniqueMultiple);

                        if (!e.Value.uniqueMultipleAdd.IsNullOrEmpty())
                            f.AddRange(e.Value.uniqueMultipleAdd);

                        if (!e.Value.uniqueMultipleSub.IsNullOrEmpty())
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
                    if (!c.COLUMN_DEFAULT.IsNullOrEmpty() && !c.COLUMN_DEFAULT.IsDbNull() && (c.COLUMN_DEFAULT as string) != "NULL")
                        f.defaultValue = c.COLUMN_DEFAULT;

                    if (!c.REFERENCED_TABLE_NAME.IsNullOrEmpty())
                        f.refEntityName = c.REFERENCED_TABLE_NAME;

                    if (!c.REFERENCED_COLUMN_NAME.IsNullOrEmpty()) //se compara por REFERENCED_TABLE_NAME para REFERENCED_COLUMN_NAME
                        f.refFieldName = c.REFERENCED_COLUMN_NAME;

                    f.notNull = (c.IS_NULLABLE == 1) ? false : true;

                    f.checks = new()
                    {
                        { "type", f.dataType },
                    };

                    if(f.notNull)
                        f.checks["required"] = true;

                    if(f.dataType == "string")
                        f.resets = new()
                        {
                            { "trim", ' '},
                            { "removeMultipleSpaces", true },
                        };

                    if (f.dataType == "bool" && f.defaultValue is not null)
                        f.defaultValue = ((string)f.defaultValue).ToBool();

                    if (!fields.ContainsKey(t.Name!))
                        fields[t.Name!] = new();

                    fields[t.Name!][f.name] = f;
                }
            }
            #endregion

            #region Redefinicion de fields en base a configuracion
            foreach (string entityName in entities.Keys)
                if (fields.ContainsKey(entityName))
                    if (File.Exists(config.configPath + "fields/" + entityName + ".json"))
                        using (StreamReader r = new StreamReader(config.configPath + "fields/" + entityName + ".json"))
                        {
                            Dictionary<string, FieldAux> fieldsAux = JsonConvert.DeserializeObject<Dictionary<string, FieldAux>>(r.ReadToEnd())!;
                            foreach (KeyValuePair<string, FieldAux> e in fieldsAux)
                            {
                                if (fields[entityName].ContainsKey(e.Key))
                                {
                                    CollectionUtils.CopyValues(fields[entityName][e.Key], e.Value);

                                    Dictionary<string, object> f = fields[entityName][e.Key].checks;
                                    if (!e.Value.checks.IsNullOrEmpty())
                                        f = e.Value.checks;
                                    if (!e.Value.checksAdd.IsNullOrEmpty())
                                        f.Merge(e.Value.checks);
                                    if (!e.Value.checksSub.IsNullOrEmpty())
                                        foreach (string k in e.Value.checksSub)
                                            f.Remove(k);
                                    fields[entityName][e.Key].checks = f;

                                    f = fields[entityName][e.Key].resets;
                                    if (!e.Value.resets.IsNullOrEmpty())
                                        f = e.Value.resets;
                                    if (!e.Value.resetsAdd.IsNullOrEmpty())
                                        f.Merge(e.Value.checks);
                                    if (!e.Value.resetsSub.IsNullOrEmpty())
                                        foreach (string k in e.Value.resetsSub)
                                            f.Remove(k);
                                    fields[entityName][e.Key].resets = f;
                                }
                            }
                        }
            #endregion

            #region Definicion de tree y relations de entities            
            /*
             * La definicion de tree y relations no se debe modificar 
             * en la configuracion, ya que se basa en otras llaves 
             * (entity.fk y field.ref*)
             */
            foreach (var (name, e) in entities)
            {
                var bet = new BuildEntityTree(config, entities, fields, e.name!);
                e.tree = bet.Build();
                RelationsRecursive(e.relations, e.tree);

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
            if (!Directory.Exists(Config.modelPath))
                Directory.CreateDirectory(Config.modelPath);

            if (File.Exists(Config.modelPath + "entities.json"))
                File.Delete(Config.modelPath + "entities.json");

            var file = JsonConvert.SerializeObject(entities, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Config.modelPath + "entities.json", file);
        }

        public void CreateFileFields()
        {
            if (!Directory.Exists(Config.modelPath + "Fields/"))
                Directory.CreateDirectory(Config.modelPath + "Fields/");

            foreach(var (entityName, field) in fields)
            {
                if (File.Exists(Config.modelPath + entityName + ".json"))
                    File.Delete(Config.modelPath + entityName + ".json");

                var file = JsonConvert.SerializeObject(fields[entityName], Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Config.modelPath + "Fields/" + entityName + ".json", file);
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

