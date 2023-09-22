using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMy.Data;

namespace WpfAppMy.ViewModels
{
    internal class AsignacionRel : Data_alumno_comision_r
    {
        public string? comision__numero { get; set; }
        public string? domicilio__label { get; set; }

    }
}
