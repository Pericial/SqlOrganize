using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSs
{
    public class Form1_DTOSJUDI   
    {
        public string DTOJUD { set; get; }
        public string DESCRIPCION { set; get; }
        public int cantidad_personal { set; get; }
        public List<Form1_PERSONAL> Personal { set; get; }
    }
}
