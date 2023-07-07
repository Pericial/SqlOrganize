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
    public class Persist
    {
        public Db db { get; }

        public List<object> parameters { get; set; }  = new List<object> { };

        public int count = 0;

        public string sql { get; set; } = "";
        

        public Persist(Db _db)
        {
            db = _db;
        }

        public Persist Parameters(params object[] parameters)
        {
            this.parameters.AddRange(parameters.ToList());
            return this;
        }

        protected Persist _Update(string entityName, Dictionary<string, object> row)
        {
            string sn = db.Entity(entityName).schemaName;
            sql += @"
UPDATE " + sn + @"SET
";
            List<string> fieldNames = db.FieldNamesNoAdmin(entityName);
            foreach (string fieldName in fieldNames)
                if (row.ContainsKey(fieldName)) sql += fieldName + " = @" + fieldName + count + ", ";
            sql.RemoveLastIndex(',');
            parameters.Add(row);
            return this;
        }

        public Persist UpdateId(string entityName, Dictionary<string, object> row)
        {
            _Update(entityName, row);
            sql += @"
WHERE $_Id = @_Id" + count + @";
";
            count++;
                
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

    }
}
 