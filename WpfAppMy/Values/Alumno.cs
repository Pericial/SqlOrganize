using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Values
{
    class Alumno 
    { 
        public static string cuatrimestre_ingreso(DateTime alta)
        {
            return alta.ToSemester().ToString() + "ºC " + alta.Year;
        }
    }
}
