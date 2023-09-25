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
            return ContainerApp.Db().Query("modalidad").ColOfDictCache();
        }

        public IDictionary<string, object>? RowByEntityFieldValue(string entityName, string fieldName, object value)
        {
            return ContainerApp.db.Query(entityName).Where("$"+fieldName+" = @0").Parameters(value).DictCache();
        }

        public IDictionary<string, object>? RowByEntityUnique(string entityName, IDictionary<string, object> source)
        {
            return ContainerApp.db.Query(entityName).Unique(source).DictCache();
        }


        public void UpdateValueRelModalidad(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.Db().Persist("modalidad").UpdateValueRel(key, value, source).Exec().RemoveCache();
        }



    }
}
