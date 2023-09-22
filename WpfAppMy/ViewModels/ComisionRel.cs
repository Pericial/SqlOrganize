using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMy.Data;

namespace WpfAppMy.ViewModels
{
    internal class ComisionRel : Data_comision_r
    {
        public string? label_r { get; set; }

        public string? label_sede_r { get; set; }

        public string? numero { get; set; }

        public string? calendario__label { get; set; }
        public string? domicilio__label { get; set; }

    }
}
