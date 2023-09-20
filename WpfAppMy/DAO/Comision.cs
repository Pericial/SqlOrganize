using SqlOrganize;
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

        public void UpdateValueRel(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.Db().Persist("comision").UpdateValueRel(key, value, source).Exec();
            ContainerApp.DbCache().Remove(p.detail);
        }

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

        public List<Dictionary<string, object>> ComisionesConSiguientePorCalendario(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("comision")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0
                    AND $calendario-semestre = @1 
                    AND $comision_siguiente IS NOT NULL
                ")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().ListDict(q);
        }

        public List<object> IdsComisionesAutorizadasPorCalendario(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("comision")
                .Fields(ContainerApp.db.config.id)
                .Size(0)
                .Where(@"
                    $calendario-anio = @0
                    AND $calendario-semestre = @1
                    AND $autorizada = true
                ")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().Column<object>(q);
        }

        public List<Dictionary<string, object>> ComisionesAutorizadasPorSemestre(object anio, object semestre)
        {
            List<object> ids = IdsComisionesAutorizadasPorCalendario(anio, semestre);
            return ContainerApp.DbCache().ListDict("comision", ids);

            var q = ContainerApp.Db().Query("comision")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0
                    AND $calendario-semestre = @1
                    AND $autorizada = true
                ")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().ListDict(q);
        }
    }
}
