using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Windows.ListaCursos
{
    internal class DAO : WpfAppMy.DAO2
    {
        public IEnumerable<Dictionary<string, object>> CursoAll(Search search)
        {
            return ContainerApp.db.Query("curso")
                .Fields()
                .Select("CONCAT($sede-numero, $comision-division, '/', $planificacion-anio, $planificacion-semestre) AS numero")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                ")
                .Parameters(search.calendario__anio, search.calendario__semestre).ColOfDictCache();
        }

    }
}
