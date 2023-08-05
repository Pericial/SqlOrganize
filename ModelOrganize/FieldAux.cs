using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelOrganize
{
    public class FieldAux: Field
    {
        public Dictionary<string, object> checksAdd = new();
        public List<string> checksSub = new();
        public Dictionary<string, object> resetsAdd = new();
        public List<string> resetsSub = new();
    }
}
