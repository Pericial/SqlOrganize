using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMy.Controllers.AlumnoComision
{
    public class MainController
    {

        public static List<Dictionary<string, object>> AlumnoComisionPorAnioSemestre(string anioCalendario, int semestreCalendario)
        {
            return ContainerApp.Db().Query("alumno_comision").
                Size(0).
                Where("$estado != @0 AND $calendario-anio = @1 AND calendario-semestre = @2").
                Parameters("Mesa", anioCalendario, semestreCalendario).
                Order("$sede-numero ASC, comision-division ASC, persona-apellidos ASC, persona-nombres ASC").
                ListDict();
        }
    }
}
