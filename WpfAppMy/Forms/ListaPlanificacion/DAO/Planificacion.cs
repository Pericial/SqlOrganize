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
            var q = ContainerApp.Db().Query("planificacion");
            return ContainerApp.DbCache().ListDict(q);
        }

        public void UpdateValueRel(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.Db().Persist("planificacion").UpdateValueRel(key, value, source).Exec();
            ContainerApp.DbCache().Remove(p.detail);
        }



    }
}
