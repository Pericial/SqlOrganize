using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.DAO
{
    public class Calificacion
    {
        public IEnumerable<object> IdsAlumnosConCalificacionesAprobadasCruzadasNoArchivadas(IEnumerable<object> ids)
        {
            var q = ContainerApp.Db().Query("calificacion")
                .Select("COUNT(DISTINCT $plan_pla-id) as cantidad_planes")
                .Group("$alumno")
                .Size(0)
                .Where(@"
                    $alumno IN (@0)
                    AND ($nota_final >= 7
                    OR $crec >= 4)
                    AND $archivado = false
                ")
                .Having("cantidad_planes > 1")
                .Parameters(ids);

            //var qq = q.Sql();
            return ContainerApp.DbCache().Column<object>(q, "alumno");
        }

        public IEnumerable<Dictionary<string, object>> CalificacionesAprobadasDeAlumnosNoArchivadas(IEnumerable<object> idsAlumnos)
        {
            var q = ContainerApp.Db().Query("calificacion")
                .Size(0)
                .Where(@"
                    $alumno IN (@0)
                    AND ($nota_final >= 7
                    OR $crec >= 4) 
                    AND $archivado = false
                ")
                .Parameters(idsAlumnos)
                .Order("$persona-apellidos ASC, $persona-nombres ASC, $planificacion_dis-anio ASC, $planificacion_dis-semestre ASC, $planificacion_dis-plan");

            //var qq = q.Sql();
            return ContainerApp.DbCache().ColOfDict(q);
        }


        /// <summary>
        /// Se devuelven las calificaciones por tramo, sin tener en cuenta el plan
        /// </summary>
        /// <param name="alumno"></param>
        /// <param name="anio"></param>
        /// <param name="semestre"></param>
        /// <returns></returns>
        public Int64 CantidadCalificacionesAprobadasDeAlumnoPorTramo(object alumno, object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("calificacion")
                .Select("COUNT($id) as cantidad")
                .Size(0)
                .Where(@"
                    $alumno = @0
                    AND $planificacion_dis-anio = @1
                    AND $planificacion_dis-semestre = @2
                ")
                .Parameters(alumno, anio, semestre);

            //var qq = q.Sql();
            return ContainerApp.DbCache().Value<Int64>(q);
        }

    }
}
