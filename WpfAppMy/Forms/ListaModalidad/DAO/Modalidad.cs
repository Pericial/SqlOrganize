using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Forms.ListaModalidad.DAO
{
    internal class Modalidad
    {

        public List<Dictionary<string, object>> All()
        {
            var q = ContainerApp.Db().Query("modalidad");
            return ContainerApp.DbCache().ListDict(q);
        }

        public void UpdateValueRel(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.Db().Persist("modalidad").UpdateValueRel(key, value, source).Exec();
            ContainerApp.DbCache().Remove(p.detail);
        }



    }
}
