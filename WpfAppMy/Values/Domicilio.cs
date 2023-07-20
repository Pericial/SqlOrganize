using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Values
{
    public class Domicilio
    {
        public string label(Dictionary <string, object> values)
        {
            return values["calle"] + " N° " + values["numero"];
        }
    }
}
