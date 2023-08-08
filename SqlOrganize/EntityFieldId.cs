using System.Reflection;
using Utils;

namespace SqlOrganize
{
    public class EntityFieldId
    {
        public Db db { get; }
        public string entityName { get; }

        public string? fieldId { get; set; }

        public EntityFieldId(Db _db, string _entityName, string? _fieldId = null)
        {
            db = _db;
            entityName = _entityName;
            fieldId = _fieldId;
        }

        public string Pf()
        {
            return (!fieldId.IsNullOrEmpty()) ? fieldId! + "-" : "";
        }

        public string Pt()
        {
            return (!fieldId.IsNullOrEmpty()) ? fieldId! : db.Entity(entityName).alias;
        }

       

    }
}