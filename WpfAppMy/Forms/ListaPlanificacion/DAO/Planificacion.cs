using Google.Protobuf.WellKnownTypes;
using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;

namespace WpfAppMy.Forms.ListaPlanificacion.DAO
{
    internal class Planificacion
    {

        public IEnumerable<Dictionary<string, object>> All()
        {
            return ContainerApp.Db().Query("planificacion").ColOfDictCache();
        }

        public void UpdateValueRel(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.Db().Persist("planificacion").UpdateValueRel(key, value, source).Exec().RemoveCache();
        }



    }
}
