using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMy.Data;

namespace WpfAppMy.ViewModels
{
    class Persona : Data_persona
    {
        public string? label { set; get; }

        public Persona InitLabel() {

            string s = "";
            s += nombres ?? "" + " ";
            s += apellidos ?? "" + " ";
            s += numero_documento ?? "";
            label = s.Trim();
            return this;
        }
    }
}
