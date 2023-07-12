using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WinFormsAppMy.Controllers.AlumnoComision
{
    public class InformeCoordinacionDistrital
    {
        public static List<Dictionary<string, object>> GenerarInforme(string anioCalendario, int semestreCalendario)
        {
            var q = ContainerApp.Db().Query("alumno_comision").
                 Size(10).
                 Where("$estado != @0 AND $calendario-anio = @1 AND calendario-semestre = @2").
                 Parameters("Mesa", anioCalendario, semestreCalendario).
                 Order("$sede-numero ASC, comision-division ASC, persona-apellidos ASC, persona-nombres ASC");

            List<Dictionary<string, object>> alumno_comision_ = ContainerApp.QueryCache().ListDict(q);

            foreach (Dictionary<string, object> alu_com in alumno_comision_)
            {
                alu_com["asignatura111"] = "";
                alu_com["asignatura112"] = "";
                alu_com["asignatura113"] = "";
                alu_com["asignatura114"] = "";
                alu_com["asignatura115"] = "";
                alu_com["asignatura121"] = "";
                alu_com["asignatura122"] = "";
                alu_com["asignatura123"] = "";
                alu_com["asignatura124"] = "";
                alu_com["asignatura125"] = "";
                alu_com["asignatura211"] = "";
                alu_com["asignatura212"] = "";
                alu_com["asignatura213"] = "";
                alu_com["asignatura214"] = "";
                alu_com["asignatura215"] = "";
                alu_com["asignatura221"] = "";
                alu_com["asignatura222"] = "";
                alu_com["asignatura223"] = "";
                alu_com["asignatura224"] = "";
                alu_com["asignatura225"] = "";
                alu_com["asignatura311"] = "";
                alu_com["asignatura312"] = "";
                alu_com["asignatura313"] = "";
                alu_com["asignatura314"] = "";
                alu_com["asignatura315"] = "";
                alu_com["asignatura321"] = "";
                alu_com["asignatura322"] = "";
                alu_com["asignatura323"] = "";
                alu_com["asignatura324"] = "";
                alu_com["asignatura325"] = "";

                q = ContainerApp.Db().Query("calificacion")
                    .Size(0)
                    .Where("$alumno = @0 AND ($nota_final >= 7 OR $crec >= 4")
                    .Parameters(alu_com["planificacion-plan"])
                    .Order("$planificacion-anio ASC, $planificacion-semestre ASC, $asignatura-nombre ASC");

                /*$calificacion_ = $this->container->query("calificacion")
      ->cond([
        ["alumno", "=",$idAlumno_],
        [
          ["nota_final",">=","7"],
          ["crec",">=","4","OR"]
        ],
        ["planificacion_dis-plan","=",$plan],

      ])
      ->order(["planificacion_dis-anio"=>"ASC", "planificacion_dis-semestre"=>"ASC"])
      ->all();
                List<Dictionary<string, object>> calificaciones = ContainerApp.QueryCache().ListDict(q);

                foreach(Dictionary<string, object> calificacion in calificaciones)
                {
                    
                }
{


            }





            List<string> ids_alumno = alumno_comision.Column<string>("alumno");




            Dictionary<string, List<Dictionary<string, object>>> comision__alumno_comision = alumno_comision.GroupByKey("comision");

            Dictionary<string, object> comisiones = new();
            foreach (var (id_comision, alu_com) in comision__alumno_comision)
            {
                comisiones[id_comision] = new();
                List<string> ids_alumno = alu_com.Column<string>("alumno");
                comisiones[id_comision]["disposiciones"] = Disposicion(alu_com[0]["planificacion-plan"]);
            }
            $comision_ = [];
            foreach ($alumnoComision_ as $idComision => $aluCom_){
        $comision_[$idComision] = [];
        $idAlumno_ = array_column($aluCom_, "alumno");
        $comision_[$idComision]["disposicion_"] = $this->disposicion_($aluCom_[0]["planificacion-plan"]);
        $comision_[$idComision]["calificacion_"] = $this->alumno__calificacionAprobada_($idAlumno_, $aluCom_[0]["planificacion-plan"]);

                */
            }

            return new();
        }


    }
}
