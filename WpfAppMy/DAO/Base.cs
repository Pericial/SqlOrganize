using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.DAO
{
    public class Base
    {
        public IEnumerable<Dictionary<string, object>> Buscar<T>(string entityName, T param) where T : class
        {
            var q = ContainerApp.db.Query(entityName).Search<T>(param);
            return ContainerApp.dbCache.ListDict(q);
        }

        public void UpdateValueRel(string entityName, string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.Db().Persist(entityName).UpdateValueRel(key, value, source).Exec();
            ContainerApp.DbCache().Remove(p.detail);
        }
    }
}
