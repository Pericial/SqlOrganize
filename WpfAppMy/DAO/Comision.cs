using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.DAO
{
    public class Comision
    {

        public List<Dictionary<string, object>> ComisionesSemestre(object calendarioAnio, object calendarioSemestre, object? sede = null, bool? autorizada = null)
        {
            var q = ContainerApp.Db().Query("comision")
                .Fields()
                .Select("CONCAT($sede-numero, $division, '/', $planificacion-anio, $planificacion-semestre) AS numero")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                ")
                .Parameters(calendarioAnio, calendarioSemestre);
            var count = 2;
            if (!autorizada.IsNullOrEmpty())
            {
                q.Where("AND $autorizada = @" + count + " ");
                q.Parameters(autorizada!);
                count++;
            }
            if (!sede.IsNullOrEmpty())
            {
                q.Where("AND sede = @" + count + " ");
                q.Parameters(sede!);
            }

            return ContainerApp.DbCache().ListDict(q);
        }

        public List<object> IdsComisionesAutorizadasConSiguientePorSemestre(object calendarioAnio, object calendarioSemestre)
        {
            var q = ContainerApp.Db().Query("comision")
                .Fields("id")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $autorizada = true
                    AND $comision_siguiente IS NOT NULL
                ")
                .Parameters(calendarioAnio, calendarioSemestre);
            return ContainerApp.DbCache().Column<object>(q);
        }

        public List<Dictionary<string, object>> ComisionesPorIds(List<object> ids)
        {
            var q = ContainerApp.Db().Query("comision")
                .Fields()
                .Size(0)
                .Where(@"
                    $id IN ( @0 ) 
                ")
                .Parameters(ids);
            
            return ContainerApp.DbCache().ListDict(q);
        }
    }
}
