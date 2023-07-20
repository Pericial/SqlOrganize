using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Values
{
    public class Sede
    {
        public string label_domicilio(Dictionary<string, object> valuesSede, Dictionary<string, object> valuesDomicilio)
        {
            Domicilio domicilio = new Domicilio();
            return valuesSede["numero"] + " " + valuesSede["nombre"] + " " + domicilio.label(valuesDomicilio) ;
        }
    }
}
