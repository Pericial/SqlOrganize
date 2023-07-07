using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SqlOrganize
{
    public class EntityPersist
    {
        public Db db { get; }

        public List<object> parameters = new List<object> { };

        public string sql { get; set; } = "";
        

        public EntityPersist(Db _db)
        {
            db = _db;
        }

        public EntityPersist Parameters(params object[] parameters)
        {
            this.parameters.AddRange(parameters.ToList());
            return this;
        }

        protected string _Update(string entityName, Dictionary<string, object> row)
        {
            string sn = db.Entity(entityName).schemaName;
            return @"
UPDATE " + sn + @"SET
";
            List<string> fns = db.FieldNamesNoAdmin(entityName);
            foreach(string fn in fns)  {
                if(isset)
            }
        }

        public EntityPersist UpdateId(string entityName, Dictionary<string, object> row)
        {
            sql += _Update(row) + @"
WHERE $_Id = @_Id 
                
                {$this->_update($row)}
            WHERE {$this->container->entity($this->entity_name)->getPk()->getName()} = {$row['id']};
            ";
            return this;
        }

        
    }
}
