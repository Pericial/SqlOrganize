using Newtonsoft.Json;
using System.Text;

namespace SqlOrganize
{
    /*
    Restricciones de la base de datos:

    - La fk solo puede referenciar a una y solo una tabla
    */
    public abstract class Db
    {
        public Config Config { get; }

        public Dictionary<string, Dictionary<string, EntityTree>> tree { get; set; } = new();

        public Dictionary<string, Dictionary<string, EntityRel>> relations { get; set; } = new();

        public Dictionary<string, Entity> entities { get; set; }

        public Dictionary<string, Dictionary<string, Field>> fields { get; set; }

        public Db(Config config)
        {
            Config = config;
            fields = new Dictionary<string, Dictionary<string, Field>>();

            string path = Config.ModelPath + "tree.json";
            using (StreamReader r = new StreamReader(path, Encoding.UTF8))
            {
                if(r.Peek() != -1) tree = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, EntityTree>>>(r.ReadToEnd())!;
            }

            using (StreamReader r = new StreamReader(Config.ModelPath + "relations.json"))
            {
                if (r.Peek() != -1) relations = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, EntityRel>>>(r.ReadToEnd())!;
            }

            using (StreamReader r = new StreamReader(Config.ModelPath + "entities.json"))
            {
                entities = JsonConvert.DeserializeObject<Dictionary<string, Entity>>(r.ReadToEnd())!;
            }

            if (File.Exists(Config.ModelPath + "entities" + Config.modelSuffix + ".json"))
            {
                using (StreamReader r = new StreamReader(Config.ModelPath + "entities" + Config.modelSuffix + ".json"))
                {
                    Dictionary<string, Entity> ee = JsonConvert.DeserializeObject<Dictionary<string, Entity>>(r.ReadToEnd());
                    foreach (KeyValuePair<string, Entity> e in ee)
                    {
                        if (entities.ContainsKey(e.Key))
                        {
                            Utils.CopyValues(entities[e.Key], e.Value);                            
                        }
                    }
                }
            }

            foreach(KeyValuePair<string, Entity> e in entities)
            {
                e.Value.db = this;
                if(!e.Value.nf_add.IsNullOrEmpty())
                {
                    var nf = new List<string>(e.Value.nf.Count + e.Value.nf_add.Count);
                    nf.AddRange(e.Value.nf);
                    nf.AddRange(e.Value.nf_add);
                    e.Value.nf = nf;
                    e.Value.nf_add.Clear();
                }
                if (!e.Value.mo_add.IsNullOrEmpty())
                {
                    var mo = new List<string>(e.Value.mo.Count + e.Value.mo_add.Count);
                    mo.AddRange(e.Value.mo);
                    mo.AddRange(e.Value.mo_add);
                    e.Value.mo = mo;
                    e.Value.mo_add.Clear();
                }
                if (!e.Value.oo_add.IsNullOrEmpty())
                {
                    var oo = new List<string>(e.Value.oo.Count + e.Value.oo_add.Count);
                    oo.AddRange(e.Value.oo);
                    oo.AddRange(e.Value.oo_add);
                    e.Value.oo = oo;
                    e.Value.oo_add.Clear();
                }
                if (!e.Value.unique_add.IsNullOrEmpty())
                {
                    var unique = new List<string>(e.Value.unique.Count + e.Value.unique_add.Count);
                    unique.AddRange(e.Value.unique);
                    unique.AddRange(e.Value.unique_add);
                    e.Value.unique = unique;
                    e.Value.unique_add.Clear();
                }
                if (!e.Value.not_null_add.IsNullOrEmpty())
                {
                    var not_null = new List<string>(e.Value.not_null.Count + e.Value.not_null_add.Count);
                    not_null.AddRange(e.Value.not_null);
                    not_null.AddRange(e.Value.not_null_add);
                    e.Value.not_null = not_null;
                    e.Value.not_null_add.Clear();
                }
                if (!e.Value.nf_sub.IsNullOrEmpty())
                {
                    e.Value.nf = e.Value.nf.Except(e.Value.nf_sub).ToList();
                    e.Value.nf_sub.Clear();
                }
                if (!e.Value.mo_sub.IsNullOrEmpty())
                {
                    e.Value.mo = e.Value.mo.Except(e.Value.mo_sub).ToList();
                    e.Value.mo_sub.Clear();
                }
                if (!e.Value.oo_sub.IsNullOrEmpty())
                {
                    e.Value.oo = e.Value.oo.Except(e.Value.oo_sub).ToList();
                    e.Value.oo_sub.Clear();
                }
                if (!e.Value.unique_sub.IsNullOrEmpty())
                {
                    e.Value.unique = e.Value.unique.Except(e.Value.unique_sub).ToList();
                    e.Value.unique_sub.Clear();
                }
                if (!e.Value.not_null_sub.IsNullOrEmpty())
                {
                    e.Value.not_null = e.Value.not_null.Except(e.Value.not_null_sub).ToList();
                    e.Value.not_null_sub.Clear();
                }
            }
        }

        public Dictionary<string, Field> fields_entity(string entity_name)
        {
            if (!fields.ContainsKey(entity_name))
            {
                using (StreamReader r = new StreamReader(config!["path_model"] + "fields/_"+entity_name+".json"))
                {
                    fields[entity_name] = JsonConvert.DeserializeObject<Dictionary<string, Field>>(r.ReadToEnd())!;

                    if (File.Exists(Config.ModelPath + "fields/" + entity_name + ".json"))
                    {
                        using (StreamReader r2 = new StreamReader(Config.ModelPath + "fields/" + entity_name + ".json"))
                        {
                            Dictionary<string, Field> ee = JsonConvert.DeserializeObject<Dictionary<string, Field>>(r2.ReadToEnd());
                            foreach (KeyValuePair<string, Field> e in ee) 
                            {
                                if (fields[entity_name].ContainsKey(e.Key))
                                {
                                    Utils.CopyValues(fields[entity_name][e.Key], e.Value);
                                }
                            }
                        }

                    }

                }

                foreach (KeyValuePair<string, Field> e in fields[entity_name])
                {
                    e.Value.db = this;
                }
            }

            return fields[entity_name];
        }

        /* 
        configuracion de field

        Si no existe el field consultado se devuelve una configuracion vacia
        No es obligatorio que exista el field en la configuracion, se cargaran los parametros por defecto.
        */
        public Field field(string entity_name, string field_name)
        {
            Dictionary<string, Field> fe = fields_entity(entity_name);
            return (fe.ContainsKey(field_name)) ? fe[entity_name] : new Field();
        }

        public List<string> entity_names() => tree.Keys.ToList();
        
        public List<string> field_names(string entity_name) => fields_entity(entity_name).Keys.ToList();
        
        public Dictionary<string, string> explode_field(string entity_name, string field_name)
        {
            List<string> f = field_name.Split("-").ToList();

            if (f.Count() == 2)
            {
                return new Dictionary<string, string>
                {
                    { "field_id", f[0] },
                    { "entity_name", relations[entity_name][f[0]].entity_name },
                    { "field_name", f[1] },
                };

            }

            return new Dictionary<string, string>
            {
                { "field_id", "" },
                { "entity_name", entity_name },
                { "field_name", field_name },
            };
        }
        
        public Entity entity(string entity_name)
        {
            return entities[entity_name];
        }

        public EntityTools tools(string entity_name)
        {
            return new (this, entity_name);
        }

        public abstract EntityQuery Query(string entity_name);

        public abstract Mapping mapping(string entity_name, string field_id);

        public abstract Values values(string entity_name, string field_id);

        //field_by_id(self, entity_name:str, field_id:str) 

    }

}
