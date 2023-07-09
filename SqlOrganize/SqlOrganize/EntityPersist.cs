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
    public abstract class EntityPersist
    {
        public Db db { get; }

        public string? entityName { get; }

        public List<object> parameters { get; set; }  = new List<object> { };

        public int count = 0;

        public string sql { get; set; } = "";

        public EntityPersist(Db _db, string? _entityName = null)
        {
            db = _db;
            entityName = _entityName;
        }

        public EntityPersist Parameters(params object[] parameters)
        {
            this.parameters.AddRange(parameters.ToList());
            return this;
        }

        protected EntityPersist _Update(Dictionary<string, object> row, string? _entityName = null)
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

        public EntityPersist Update(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;

            _Update(row, _entityName);
            string _Id = db.Mapping(_entityName!).Map("_Id");
            sql += @"
WHERE " + _Id + " = @" + count + @";
";
            count++;
            parameters.Add(row["_Id"]);
            return this;
        }


        public EntityPersist Insert(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;

            List<string> fieldNames = db.FieldNamesAdmin(_entityName!);
            Dictionary<string, object> row_ = new();
            foreach (string key in row.Keys)
                if (fieldNames.Contains(key))
                    row_.Add(key, row[key]);

            var keys = row.Keys.Select(x => "@" + x + count).ToList();
            string sn = db.Entity(_entityName!).schemaName;
            sql = "INSERT INTO " + sn + @" (" + String.Join(", ", row_.Keys) + @") 
VALUES (";


            foreach (object value in row_.Values)
            {
                sql += "@" + count + ", ";
                parameters.Add(value);
                count++;
            }

            sql = @");
";

            return this;
        }

        public string Sql()
        {
            return sql;
        }

        public EntityPersist Persist(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;

            EntityValues v = (EntityValues)db.Values(_entityName!).From(row, "Set");
            var rows = db.Query(_entityName!).Unique(row).ListDict();
            if (rows.Count > 1)
                throw new Exception("La consulta por campos unicos retorno mas de un resultado");

            if (rows.Count == 1) //actualizar
            {
                v.Set("_Id", row["_Id"]).Call("Reset").Call("Check");
                if (v.logging.Error())
                    throw new Exception("Los campos a actualizar poseen errores: " + v.logging.ToString());
                return Update(v.values, _entityName);
            }

            v.Call("SetDefault").Call("Reset").Call("Check");
            if (v.logging.Error())
                throw new Exception("Los campos a insertar poseen errores: " + v.logging.ToString());
            return Insert(v.values, _entityName);
        }

        public abstract void Exec();

    }
}
 