using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMy.Data;

namespace WpfAppMy.Windows.AlumnoComision.ViewModel
{
    internal class Asignacion : Data_alumno_comision_rel
    {
        public string comision__numero { get; set; }
        public string domicilio__label { get; set; }

    }
}
