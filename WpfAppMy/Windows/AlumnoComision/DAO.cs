using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils;
using WpfAppMy.Values;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfAppMy.Windows.AlumnoComision
{
    internal class DAO
    {
        public List<string> IdsAlumnoDeComisionesAutorizadasPorCalendario(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields("alumno")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0
                    AND $calendario-semestre = @1 
                    AND $comision-autorizada = true
                ")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().Column<string>(q);
        }

        public List<string> FiltrarAlumnosConCalificacionesAprobadasCruzadas(List<object> ids)
        {
            var q = ContainerApp.Db().Query("calificacion")
                .Fields("$alumno")
                .Select("SUM($plan-id) as suma_planes")
                .Group("alumno")
                .Size(0)
                .Where(@"
                    $ids IN (@0)
                ")
                .Having("suma_planes > 1")
                .Parameters(ids);

            return ContainerApp.DbCache().Column<string>(q);
        }

        public List<Dictionary<string, object>> AlumnosComisionesPorComision(object comision)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Size(0)
                .Where(@"
                    $comision = @0
                ")
                .Parameters(comision);

            return ContainerApp.DbCache().ListDict(q);
        }

        public List<object> IdsAlumnosPorComisiones(List<object> comisiones)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields("alumno")
                .Size(0)
                .Where(@"
                    $comision IN (@0)
                ");

            return ContainerApp.DbCache().Column<object>(q);
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

        public List<Dictionary<string, object>> ComisionesAutorizadasPorCalendario(object anio, object semestre)
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

        public List<object> IdAlumnosConPlanDiferenteDeComision(object comision, object plan)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
               .Fields("alumno")
               .Size(0)
               .Where(@"
                    $alumno-plan != @0
                    AND $comision = @1
                    AND $activo = true
                ")
               .Parameters(plan, comision);

            return ContainerApp.DbCache().Column<object>(q);
        }



        public List<Dictionary<string, object>> AlumnosPorCalendario(object anio, object semestre)
        {
            List<string> ids = IdsAlumnoDeComisionesAutorizadasPorCalendario(anio, semestre);
            return ContainerApp.DbCache().ListDict("alumno", ids.ToArray());
        }

        /// <summary>
        /// Cantidad de disposiciones aprobadas por alumno de comision
        /// </summary>
        /// <param name="comision"></param>
        /// <returns></returns>
        public List<object> IdAlumnosParaTransferirDeComision(string comision, object anio, object semestre)
        {
            var alumnoComision_ = AlumnosComisionesPorComision(comision);
            var idAlumnos = alumnoComision_.Column<object>("alumno").Distinct().ToList();
            var idPlan = alumnoComision_[0]["planificacion-plan"];
            var q = ContainerApp.Db().Query("calificacion")
                .Select("$alumno, SUM($disposicion) AS cantidad")
                .Group("$alumno")
                .Size(0)
                .Where(@"
                    $alumno IN (@0)
                    AND $plan-alu = @1
                    AND $plan-alu = @1
                    AND ($nota_final >= 7 OR $crec >= 4)  
                 ")
                .Having("SUM($disposicion) > 3")
                .Parameters(idAlumnos, idPlan);

            return ContainerApp.DbCache().Column<object>(q); ;

        }

    }
}
