using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Forms.InformeCoordinacionDistrital.DAO
{
    class AlumnoComision
    {
        protected List<Dictionary<string, object>> FiltroInformeCoordinacionDistrital(string modalidad, string anioCalendario, int semestreCalendario, bool? comisionSiguienteNull = null)
        {
            var q = ContainerApp.db.Query("alumno_comision").
                Fields("estado, sede-nombre, comision-identificacion, alumno-id, plan_alu-id, persona-apellidos, persona-nombres, persona-numero_documento, persona-genero, persona-fecha_nacimiento, persona-telefono, persona-email, alumno-tiene_dni, alumno-tiene_partida, alumno-tiene_certificado, alumno-creado, alumno-estado_inscripcion, planificacion-plan").
                Select(@"CONCAT($planificacion-anio, '°', $planificacion-semestre, 'C') AS tramo").
                Size(0).
                Where(@"
                    $modalidad-id = @0 AND $calendario-anio = @1 AND $calendario-semestre = @2 AND
                    $estado != 'Mesa' AND $comision-autorizada IS TRUE
                ").
                Order("$sede-numero ASC, $comision-division ASC, $persona-apellidos ASC, $persona-nombres ASC").
                Parameters(modalidad, anioCalendario, semestreCalendario);

            if (!comisionSiguienteNull.IsNullOrEmpty() && comisionSiguienteNull == true)
                q.Where("AND $comision-comision_siguiente IS NULL");
            else if (!comisionSiguienteNull.IsNullOrEmpty() && !comisionSiguienteNull == false)
                q.Where("AND $comision-comision_siguiente IS NOT NULL");

            return q.ListDict();
        }

        public List<Dictionary<string, object>> InformeCoordinacionDistrital(string modalidad, string anioCalendario, int semestreCalendario, bool? comisionSiguienteNull = null)
        {
            var calificacionDAO = new Calificacion();
            var alumno_comision_ = FiltroInformeCoordinacionDistrital(modalidad, anioCalendario, semestreCalendario, comisionSiguienteNull);

            foreach (Dictionary<string, object> alu_com in alumno_comision_)
            {
                alu_com["persona-genero"] = alu_com["persona-genero"].ToString().ToUpper();
                alu_com["tiene_dni"] = (bool)alu_com["alumno-tiene_dni"] ? "SÍ" : "NO";
                alu_com["tiene_cuil"] = (bool)alu_com["alumno-tiene_dni"] ? "SÍ" : "NO";
                alu_com["tiene_partida"] = (bool)alu_com["alumno-tiene_partida"] ? "SÍ" : "NO";
                alu_com["tiene_certificado"] = (bool)alu_com["alumno-tiene_certificado"] ? "SÍ" : "NO";
                DateTime creado = (DateTime)alu_com["alumno-creado"];
                string estado = (alu_com["estado"].IsDbNull()) ? "Activo" : (string)alu_com["estado"];
                alu_com["cuatrimestre_ingreso"] = Values.Alumno.cuatrimestre_ingreso(creado);
                alu_com["estado_ingreso"] = Values.AlumnoComision.estado_ingreso(estado, creado);
                alu_com["asignatura111"] = null;
                alu_com["asignatura112"] = null;
                alu_com["asignatura113"] = null;
                alu_com["asignatura114"] = null;
                alu_com["asignatura115"] = null;
                alu_com["asignatura121"] = null;
                alu_com["asignatura122"] = null;
                alu_com["asignatura123"] = null;
                alu_com["asignatura124"] = null;
                alu_com["asignatura125"] = null;
                alu_com["asignatura211"] = null;
                alu_com["asignatura212"] = null;
                alu_com["asignatura213"] = null;
                alu_com["asignatura214"] = null;
                alu_com["asignatura215"] = null;
                alu_com["asignatura221"] = null;
                alu_com["asignatura222"] = null;
                alu_com["asignatura223"] = null;
                alu_com["asignatura224"] = null;
                alu_com["asignatura225"] = null;
                alu_com["asignatura311"] = null;
                alu_com["asignatura312"] = null;
                alu_com["asignatura313"] = null;
                alu_com["asignatura314"] = null;
                alu_com["asignatura315"] = null;
                alu_com["asignatura321"] = null;
                alu_com["asignatura322"] = null;
                alu_com["asignatura323"] = null;
                alu_com["asignatura324"] = null;
                alu_com["asignatura325"] = null;

                var plan = (!alu_com["plan_alu-id"].IsDbNull()) ? alu_com["plan_alu-id"] : alu_com["planificacion-plan"];
                var calificaciones = calificacionDAO.AprobadasPorAlumnoPlan((string)alu_com["alumno-id"], (string)plan);

                foreach (Dictionary<string, object> calificacion in calificaciones)
                {
                    string? nota = null;
                    if ((!calificacion["nota_final"].IsDbNull() && (decimal)calificacion["nota_final"] >= 7))
                    {
                        nota = Decimal.ToInt32((decimal)calificacion["nota_final"]).ToString();
                    }
                    else if ((!calificacion["crec"].IsDbNull() && (decimal)calificacion["crec"] >= 4))

                    {
                        nota = Decimal.ToInt32((decimal)calificacion["crec"]).ToString() + "c";
                    }
                    string key = "asignatura" + calificacion["planificacion_dis-anio"].ToString() + calificacion["planificacion_dis-semestre"].ToString() + (string)calificacion["disposicion-orden_informe_coordinacion_distrital"].ToString();
                    alu_com[key] = nota;
                }
            }

            return alumno_comision_;
        }
    }
}
