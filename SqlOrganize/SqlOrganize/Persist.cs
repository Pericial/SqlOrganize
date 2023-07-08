using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace SqlOrganize
{
    public abstract class Persist
    {
        public Db db { get; }

        public string? entityName { get; }

        public List<object> parameters { get; set; }  = new List<object> { };

        public int count = 0;

        public string sql { get; set; } = "";

        public Persist(Db _db, string? _entityName = null)
        {
            db = _db;
            entityName = _entityName;
        }

        public Persist Parameters(params object[] parameters)
        {
            this.parameters.AddRange(parameters.ToList());
            return this;
        }

        protected Persist _Update(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;
            string sn = db.Entity(_entityName).schemaName;
            sql += @"
UPDATE " + sn + @" SET
";
            List<string> fieldNames = db.FieldNamesNoAdmin(_entityName);
            foreach (string fieldName in fieldNames)
                if (row.ContainsKey(fieldName))
                {
                    sql += fieldName + " = @" + count + ", ";
                    count++;
                    parameters.Add(row[fieldName]);
                }
            sql.RemoveLastIndex(',');
            return this;
        }

        public Persist Update(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;

            _Update(row, _entityName);
            sql += @"
WHERE $_Id = @" + count + @";
";
            count++;
            parameters.Add(row["_Id"]);
            return this;
        }


        public Persist Insert(string entityName, Dictionary<string, object> row)
        {
            List<string> fieldNames = db.FieldNamesNoAdmin(entityName);
            Dictionary<string, object> row_ = new();
            foreach (string key in row.Keys)
                if (fieldNames.Contains(key))
                    row_.Add(key, row[key]);

            var keys = row.Keys.Select(x => "@" + x + count).ToList();
            string sn = db.Entity(entityName).schemaName;
            sql = "INSERT INTO " + sn + @"(" + String.Join(", ", row_.Keys) + @") 
VALUES (" + String.Join(", ", keys) + @")
";
            parameters.Add(row);
            count++;
            return this;

        }

        public string Sql()
        {
            return sql;
        }

        public abstract void Exec();

    }
}
 