using SqlOrganize;
using Utils;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace SqlOrganizeSs
{
    public class EntityPersistSs : EntityPersist
    {

        public EntityPersistSs(Db db, string? entityName) : base(db, entityName)
        {
        }

        protected override EntityPersist _Update(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;
            Entity e = db.Entity(_entityName);
            sql += @"
UPDATE " + e.alias + @" SET
";
            List<string> fieldNames = db.FieldNamesAdmin(_entityName);

            foreach (string fieldName in fieldNames)
                if (row.ContainsKey(fieldName))
                {
                    sql += fieldName + " = @" + count + ", ";
                    count++;
                    parameters.Add(row[fieldName]);
                }
            sql = sql.RemoveLastChar(',');
            sql += " FROM " + e.schemaNameAlias + @"
";
            return this;
        }

    }

}
