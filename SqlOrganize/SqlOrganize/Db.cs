using Newtonsoft.Json;
using System.Text;
using Utils;

namespace SqlOrganize
{
    /*
    Restricciones de la base de datos:

    - La fk solo puede referenciar a una y solo una tabla
    */
    public abstract class Db
    {
        public Config config { get; }

        //public Dictionary<string, Dictionary<string, EntityTree>> tree { get; set; } = new();

        //public Dictionary<string, Dictionary<string, EntityRel>> relations { get; set; } = new();

        public Dictionary<string, Entity> entities { get; set; }

        public Dictionary<string, Dictionary<string, Field>> fields { get; set; }

        public Db(Config _config)
        {
            config = _config;
            fields = new Dictionary<string, Dictionary<string, Field>>();

            //string path = config.modelPath + "tree.json";
            //using (StreamReader r = new StreamReader(path, Encoding.UTF8))
            //{
                //if(r.Peek() != -1) tree = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, EntityTree>>>(r.ReadToEnd())!;
            //}

            //using (StreamReader r = new StreamReader(config.modelPath + "relations.json"))
            //{
                //if (r.Peek() != -1) relations = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, EntityRel>>>(r.ReadToEnd())!;
            //}

            using (StreamReader r = new StreamReader(config.modelPath + "entities.json"))
            {
                entities = JsonConvert.DeserializeObject<Dictionary<string, Entity>>(r.ReadToEnd())!;
                foreach (KeyValuePair<string, Entity> e in entities) { 
                    e.Value.db = this;

                    if (!e.Value.pk.IsNullOrEmpty())
                    {
                        e.Value
                    }
                }
            }

            if (File.Exists(config.modelPath + "entities" + config.modelSuffix + ".json"))
            {
                using (StreamReader r = new StreamReader(config.modelPath + "entities" + config.modelSuffix + ".json"))
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
        }

        public Dictionary<string, Field> FieldsEntity(string entityName)
        {
            if (!fields.ContainsKey(entityName))
            {
                using (StreamReader r = new StreamReader(config.modelPath + "fields/"+ entityName + ".json"))
                {
                    fields[entityName] = JsonConvert.DeserializeObject<Dictionary<string, Field>>(r.ReadToEnd())!;

                    foreach (KeyValuePair<string, Field> e in fields[entityName])
                        e.Value.db = this;

                    if (File.Exists(config.modelPath + "fields/" + entityName + config.modelSuffix + ".json"))
                    {
                        using (StreamReader r2 = new StreamReader(config.modelPath + "fields/" + entityName + config.modelSuffix + ".json"))
                        {
                            Dictionary<string, Field> fieldsAux = JsonConvert.DeserializeObject<Dictionary<string, Field>>(r2.ReadToEnd())!;
                            foreach (KeyValuePair<string, Field> e in fieldsAux) 
                            {
                                if (fields[entityName].ContainsKey(e.Key))
                                {
                                    CollectionUtils.CopyValues(fields[entityName][e.Key], e.Value);
                                }
                            }
                        }

                    }

                }

                
            }

            return fields[entityName];
        }

        /* 
        configuracion de field

        Si no existe el field consultado se devuelve una configuracion vacia
        No es obligatorio que exista el field en la configuracion, se cargaran los parametros por defecto.
        */
        public Field field(string entityName, string fieldName)
        {
            Dictionary<string, Field> fe = FieldsEntity(entityName);
            return (fe.ContainsKey(fieldName)) ? fe[entityName] : new Field();
        }

        public List<string> EntityNames() => tree.Keys.ToList();
        
        public List<string> FieldNames(string entityName) => FieldsEntity(entityName).Keys.ToList();
        
        public Dictionary<string, string> ExplodeField(string entityName, string fieldName)
        {
            List<string> f = fieldName.Split("-").ToList();

            if (f.Count() == 2)
            {
                return new Dictionary<string, string>
                {
                    { "fieldId", f[0] },
                    { "entityName", relations[entityName][f[0]].refEntityName },
                    { "fieldName", f[1] },
                };

            }

            return new Dictionary<string, string>
            {
                { "fieldId", "" },
                { "entityName", entityName },
                { "fieldName", fieldName },
            };
        }
        
        public Entity Entity(string entity_name)
        {
            return entities[entity_name];
        }

        public EntityTools tools(string entity_name)
        {
            return new (this, entity_name);
        }

        public abstract EntityQuery Query(string entity_name);

        public abstract Mapping Mapping(string entity_name, string field_id);

        public abstract Values Values(string entity_name, string field_id);

        //public abstract FieldById(string entityName, string fieldId) 

    }

}
