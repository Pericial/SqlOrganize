using SqlOrganize;
using SqlOrganizeMy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WinFormsAppMy.Data
{
    public static class AlumnoComision 
    {

        public static List<Dictionary<string,object>> AnioSemestreCalendario(string anioCalendario, int semestreCalendario)
        {
            return ContainerApp.db.Query("alumno_comision").
                Fields("$sede-nombre, $comision-identificacion,  $alumno-id, $plan_alu-id, $persona-apellidos, $persona-nombres, $persona-numero_documento, $persona-genero, $persona-fecha_nacimiento, $persona-telefono, $persona-email").
                Select(@"CONCAT($planificacion-anio, '°', $planificacion-semestre, 'C') AS tramo").
                Size(0).
                Where("$estado != @0 AND $calendario-anio = @1 AND $calendario-semestre = @2").
                Order("$sede-numero ASC, $comision-division ASC, $persona-apellidos ASC, $persona-nombres ASC").
                Parameters("Mesa",anioCalendario,semestreCalendario).
                ListDict();
        }

        public static List<Dictionary<string, object>> InformeCoordinacionDistrital(string anioCalendario, int semestreCalendario)
        {
            var alumno_comision_ = AnioSemestreCalendario(anioCalendario, semestreCalendario);

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

                var calificaciones = Calificacion.AprobadasPorAlumnoPlan((string)alu_com["alumno-id"], (string)alu_com["plan_alu-id"]);

                foreach (Dictionary<string, object> calificacion in calificaciones)
                {
                    var nota = (!calificacion["nota_final"].IsNullOrEmpty() || (decimal)calificacion["nota_final"] >= 7) ? calificacion["nota_final"] : calificacion["crec"];
                    string key = "asignatura" + calificacion["planificacion_dis-anio"].ToString() + calificacion["planificacion_dis-semestre"].ToString() + (string)calificacion["disposicion-orden_informe_coordinacion_distrital"].ToString();
                    alu_com[key] = nota;
                }
            }

            return new();
        }

    }
}
