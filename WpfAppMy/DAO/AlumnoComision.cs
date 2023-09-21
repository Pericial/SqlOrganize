using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.DAO
{
    public class AlumnoComision
    {





        /// <summary>
        /// Cantidad de disposiciones aprobadas por alumno de comision
        /// </summary>
        /// <param name="comision"></param>
        /// <returns></returns>
        public IEnumerable<object> IdAlumnosParaTransferirDeComision(string comision, object anio, object semestre)
        {
            var alumnoComision_ = AsignacionesActivasPorComision(comision);
            var idAlumnos = alumnoComision_.ColOfVal<object>("alumno").Distinct().ToList();
            var idPlan = alumnoComision_.ElementAt(0)["planificacion-plan"];
            var q = ContainerApp.Db().Query("calificacion")
                .Select("$SUM($disposicion) AS cantidad")
                .Group("$alumno")
                .Size(0)
                .Where(@"
                    $alumno IN (@0)
                    AND $plan-alu = @1
                    AND ($nota_final >= 7 OR $crec >= 4)  
                 ")
                .Having("SUM($disposicion) > 3")
                .Parameters(idAlumnos, idPlan);

            return ContainerApp.DbCache().Column<object>(q);


        }


        public IEnumerable<object> IdsAlumnosDeComisionesAutorizadasPorSemestre(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields("$alumno")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0
                    AND $calendario-semestre = @1 
                    AND $comision-autorizada = true
                ")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().Column<object>(q);
        }
        public IEnumerable<object> IdsAlumnosActivosDeComisionesAutorizadasPorSemestre(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields("$alumno")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0
                    AND $calendario-semestre = @1 
                    AND $comision-autorizada = true
                    AND $estado = 'Activo'
                ")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().Column<object>(q);
        }

        public IEnumerable<object> IdsAlumnosActivosDeComisionesAutorizadasPorSemestreSinGenero(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields("$alumno")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0
                    AND $calendario-semestre = @1 
                    AND $comision-autorizada = true
                    AND $estado = 'Activo'
                    AND $persona-genero IS NULL
                ")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().Column<object>(q);
        }


        public IEnumerable<Dictionary<string, object>> AsignacionesPorComisiones(List<object> idsComisiones)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields()
                .Size(0)
                .Where(@"
                    $comision IN (@0) 
                ")
                .Parameters(idsComisiones);
            return ContainerApp.DbCache().ColOfDict(q);
        }

        public IEnumerable<Dictionary<string, object>> AsignacionesActivasPorComisiones(IEnumerable<object> idsComisiones)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields()
                .Size(0)
                .Where(@"
                    $comision IN (@0) AND $estado = 'Activo'
                ")
                .Parameters(idsComisiones);
            return ContainerApp.DbCache().ColOfDict(q);
        }

        public IEnumerable<Dictionary<string, object>> AsignacionesActivasPorComision(object comision)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Size(0)
                .Where(@"
                    $comision = @0 AND $activo = true
  
                ")
                .Parameters(comision);

            return ContainerApp.DbCache().ColOfDict(q);
        }

        public IEnumerable<object> IdsAlumnosPorComisiones(List<object> comisiones)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields("alumno")
                .Size(0)
                .Where(@"
                    $comision IN (@0)
                ");

            return ContainerApp.DbCache().Column<object>(q);
        }

        public IEnumerable<object> IdAlumnosConPlanDiferenteDeComision(object comision, object plan)
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


        public IEnumerable<object> IdsAlumnosActivosDuplicadosPorSemestre(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
               .Select("COUNT($id) AS cantidad")
               .Group("$alumno")
               .Size(0)
               .Where(@"
                    $calendario-anio = @0
                    AND $calendario-semestre = @1
                    AND $estado = 'Activo'
                ")
               .Having("cantidad > 1")
               .Parameters(anio, semestre);

            return ContainerApp.DbCache().Column<object>(q, "alumno");
        }

        public IEnumerable<Dictionary<string, object>> AsignacionesActivasDeComisionesAutorizadasPorSemestre(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Size(0)
                .Where("$calendario-anio = @0 AND $calendario-semestre = @1 AND $comision-autorizada = true AND $estado = 'Activo'")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().ColOfDict(q);
        }

        public IEnumerable<Dictionary<string, object>> AsignacionesDeComisionesAutorizadasPorSemestre(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Size(0)
                .Where("$calendario-anio = @0 AND $calendario-semestre = @1 AND $comision-autorizada = true")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().ColOfDict(q);
        }

        public IEnumerable<Dictionary<string, object>> AlumnosDeComisionesAutorizadasPorSemestre(object anio, object semestre)
        {
            IEnumerable<object> ids = IdsAlumnosDeComisionesAutorizadasPorSemestre(anio, semestre);
            return ContainerApp.DbCache().ColOfDict("alumno", ids.ToArray());
        }

        public IEnumerable<Dictionary<string, object>> AlumnosActivosDeComisionesAutorizadasPorSemestre(object anio, object semestre)
        {
            var alumnoDao = new DAO.Alumno();
            IEnumerable<object> ids = IdsAlumnosActivosDeComisionesAutorizadasPorSemestre(anio, semestre);
            return alumnoDao.AlumnosPorIds(ids);
        }

        public IEnumerable<Dictionary<string, object>> AlumnosActivosDeComisionesAutorizadasPorSemestreSinGenero(object anio, object semestre)
        {
            var alumnoDao = new DAO.Alumno();
            IEnumerable<object> ids = IdsAlumnosActivosDeComisionesAutorizadasPorSemestreSinGenero(anio, semestre);
            return alumnoDao.AlumnosPorIds(ids);
        }
    }
}
