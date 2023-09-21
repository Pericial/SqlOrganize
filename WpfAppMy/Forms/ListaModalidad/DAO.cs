using Google.Protobuf.WellKnownTypes;
using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Forms.ListaModalidad
{
    internal class DAO
    {

        public IEnumerable<Dictionary<string, object>> AllModalidad()
        {
            var q = ContainerApp.Db().Query("modalidad");
            return ContainerApp.DbCache().ListDict(q);
        }

        public IDictionary<string, object>? RowByEntityFieldValue(string entityName, string fieldName, object value)
        {
            var q =  ContainerApp.db.Query(entityName).Where("$"+fieldName+" = @0").Parameters(value);
            return ContainerApp.DbCache().Dict(q);
        }

        public IDictionary<string, object>? RowByEntityUnique(string entityName, Dictionary<string, object> source)
        {
            var q = ContainerApp.db.Query(entityName).Unique(source);
            if (!q.IsUnique())
                return null;
            return ContainerApp.DbCache().Dict(q);
        }


        public void UpdateValueRelModalidad(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.Db().Persist("modalidad").UpdateValueRel(key, value, source).Exec();
            ContainerApp.DbCache().Remove(p.detail);
        }



    }
}
