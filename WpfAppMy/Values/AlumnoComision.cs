using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Values
{
    class AlumnoComision
    {
        public static string estado_ingreso(string estado, DateTime alta)
        {
            if (estado.ToLower() == "no activo")
                return "TRAYECTORIA INTERRUMPIDA";
            if (estado.ToLower() == "activo" && alta.ToYearSemester() == DateTime.Now.ToYearSemester())
                return "INGRESANTE";
            if (estado.ToLower() == "activo" && alta.ToYearSemester() != DateTime.Now.ToYearSemester())
                return "CONTINÚA TRAYECTORIA";
            return estado;
        }
    }
}
