﻿using System;
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
        public List<object> IdsAlumnoDeComisionesAutorizadasPorCalendario(object anio, object semestre)
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

        public List<object> IdsAlumnosConCalificacionesAprobadasCruzadasNoArchivadas(List<object> ids)
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

        public List<Dictionary<string,object>> CalificacionesAprobadasDeAlumnosNoArchivadas(List<object> idsAlumnos)
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
            return ContainerApp.DbCache().ListDict(q);
        }

        public List<Dictionary<string, object>> AlumnosComisionesPorComision(object comision)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Size(0)
                .Where(@"
                    $comision = @0 AND $activo = true
  
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
            List<object> ids = IdsAlumnoDeComisionesAutorizadasPorCalendario(anio, semestre);
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

        public List<object> IdsAlumnosActivosDuplicadosPorSemestre(object anio, object semestre)
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


        public List<Dictionary<string, object>> AlumnosPorIds(List<object> ids)
        {
            var q = ContainerApp.Db().Query("alumno")
               .Size(0)
               .Where(@"
                    $id IN ( @0 )
                ")
               .Parameters(ids);

            return ContainerApp.DbCache().ListDict(q);
        }

        public List<Dictionary<string, object>> AlumnosComisionesAutorizadasPorCalendario(object anio, object semestre)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Size(0)
                .Where("$calendario-anio = @0 AND $calendario-semestre = @1 AND $comision-autorizada = true AND $estado = 'Activo'")
                .Parameters(anio, semestre);

            return ContainerApp.DbCache().ListDict(q);
        }

        /// <summary>
        /// Se devuelven las calificaciones por tramo, sin tener en cuenta el plan
        /// </summary>
        /// <param name="alumno"></param>
        /// <param name="anio"></param>
        /// <param name="semestre"></param>
        /// <returns></returns>
        public int CantidadCalificacionesAprobadasDeAlumnoPorTramo(object alumno, object anio, object semestre)
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
            return ContainerApp.DbCache().Value<int>(q);
        }

    }
}
