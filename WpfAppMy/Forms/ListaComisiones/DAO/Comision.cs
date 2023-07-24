using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Forms.ListaComisiones.DAO
{
    internal class Comision
    {

        public void UpdateValueRel(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.Db().Persist("comision").UpdateValueRel(key, value, source).Exec();
            ContainerApp.DbCache().Remove(p.detail);
        }


        public List<Dictionary<string, object>> Search(ComisionSearch search)
        {
            var q = ContainerApp.Db().Query("comision")
                .Fields()
                .Select("CONCAT($sede-numero, $division, '/', $planificacion-anio, $planificacion-semestre) AS numero")
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
            if (!search.sede.IsNullOrEmpty())
            {
                q.Where("AND sede = @" + count + " ");
                q.Parameters(search.sede!);
            }

            return ContainerApp.DbCache().ListDict(q);
        }

    }
}
