﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMy.Data;

namespace WpfAppMy.Windows.Comision.ListaComisionesSemestre
{
    internal class Comision : Data_comision_r
    {
        public string label { get; set; }
        public string domicilio__label { get; set; }

    }
}