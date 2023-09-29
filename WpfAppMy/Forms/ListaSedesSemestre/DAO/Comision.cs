using Google.Protobuf.WellKnownTypes;
using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;

namespace WpfAppMy.Forms.ListaSedesSemestre.DAO
{
    class Comision
    {

        public IEnumerable<Dictionary<string, object>> Search(ComisionSearch search)
        {
            var q = ContainerApp.db.Query("comision")
                .Fields("sede-*, domicilio-*")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                ")
                .Parameters(search.calendario__anio, search.calendario__semestre);
            var count = 2;
            if (!search.autorizada.IsNullOrEmpty())
            {
                q.Where("AND $autorizada = @" + count + " ");
                q.Parameters(search.autorizada!);
                count++;
            }
    

            return q.ColOfDictCache();
        }

        public void UpdateValueRel(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.db.Persist("comision").UpdateValueRel(key, value, source).Exec().RemoveCache();
        }

    }
}
