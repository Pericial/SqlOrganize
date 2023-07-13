using MySql.Data.MySqlClient;
using SqlOrganize;
using Utils;

namespace SqlOrganizeMy
{
    public class EntityPersistMy : EntityPersist
    {

        public EntityPersistMy(Db db, string entityName) : base(db, entityName)
        {
        }

        public override void Exec()
        {
            var q = db.Query();
            q.sql = sql;
            q.parameters = parameters;
            q.Transaction();
        }

        protected override EntityPersist _Update(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;
            string sna = db.Entity(_entityName).schemaNameAlias;
            sql += @"
UPDATE " + sna + @" SET
";
            List<string> fieldNames = db.FieldNamesAdmin(_entityName);
            foreach (string fieldName in fieldNames)
                if (row.ContainsKey(fieldName))
                {
                    sql += fieldName + " = @" + count + ", ";
                    count++;
                    parameters.Add(row[fieldName]);
                }
            sql = sql.RemoveLastIndex(',');
            return this;
        }

    }

}