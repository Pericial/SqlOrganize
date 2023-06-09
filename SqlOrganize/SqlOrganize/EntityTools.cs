namespace SqlOrganize
{
    public class EntityTools
    {
        public Db db { get; }
        public string entity_name { get; }

        public EntityTools(Db _db, string _entity_name)
        {
            db = _db;
            entity_name = _entity_name;
        }

        /*
        field_names from entity_name and its relations
        */
        public List<string> field_names()
        {
            List<string> field_names_r = new();

            if(db.relations.ContainsKey(entity_name))
                foreach((string field_id, EntityRel er) in db.relations[entity_name])
                    foreach(string field_name in db.field_names(er.entity_name))
                        field_names_r.Add(field_id + "-" + field_name);

            return db.field_names(entity_name).Concat(field_names_r).ToList();
        }

        


    }
}