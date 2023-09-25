using MySql.Data.MySqlClient;
using SqlOrganize;
using Utils;

namespace SqlOrganizeMy
{
    public class EntityPersistMy : EntityPersist
    {

        public EntityPersistMy(Db db, string? entityName) : base(db, entityName)
        {
        }

        protected override EntityPersist _Update(IDictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;
            string sna = Db.Entity(_entityName).schemaNameAlias;
            sql += @"
UPDATE " + sna + @" SET
";
            List<string> fieldNames = Db.FieldNamesAdmin(_entityName);

            foreach (string fieldName in fieldNames)
                if (row.ContainsKey(fieldName))
                {
                    sql += fieldName + " = @" + count + ", ";
                    count++;
                    parameters.Add(row[fieldName]);
                }
            sql = sql.RemoveLastChar(',');
            return this;
        }

    }

}
