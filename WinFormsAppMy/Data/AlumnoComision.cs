using SqlOrganize;
using SqlOrganizeMy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
