using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Forms.ListaComisiones.DAO
{
    class Comision
    {

        public List<Dictionary<string, object>> Search(ComisionSearch search)
        {
            var q = ContainerApp.Db().Query("comision")
                .Fields()
                .Select("CONCAT($sede-numero, $division, '/', $planificacion-anio, $planificacion-semestre) AS numero")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $autorizada = @2
                ")
                .Parameters(search.calendario__anio, search.calendario__semestre, search.autorizada);
            return ContainerApp.QueryCache().ListDict(q);
        }

    }
}
