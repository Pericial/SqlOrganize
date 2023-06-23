namespace SqlOrganize
{
    public class EntityTools
    {
        public Db db { get; }
        public string entityName { get; }

        public EntityTools(Db _db, string _entityName)
        {
            db = _db;
            entityName = _entityName;
        }

        /*
        field_names from entity_name and its relations
        */
        public List<string> FieldNames()
        {
            List<string> fieldNamesR = new();

            if(db.relations.ContainsKey(entityName))
                foreach((string fieldId, EntityRel er) in db.relations[entityName])
                    foreach(string fieldName in db.FieldNames(er.refEntityName))
                        fieldNamesR.Add(fieldId + "-" + fieldName);

            return db.FieldNames(entityName).Concat(fieldNamesR).ToList();
        }

        


    }
}