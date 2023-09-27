using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{

    /// <summary>
    /// Metodos de acceso a datos de uso general
    /// </summary>
    public class DAO
    {
        protected Db Db;

        public DAO(Db db) {
            this.Db = db;
        }

        public IEnumerable<Dictionary<string, object>> Search<T>(string entityName, T param) where T : class
        {
            return Db.Query(entityName).Search(param).Size(0).ColOfDictCache();
        }

        public EntityPersist UpdateValueRel(string entityName, string key, object value, Dictionary<string, object> source)
        {
            return Db.Persist(entityName).UpdateValueRel(key, value, source).Exec().RemoveCache();
        }

        public IDictionary<string, object> Get(string entityName, object id)
        {
            return Db.Query(entityName).CacheByIds(id).ElementAt(0);
        }

    }
}
