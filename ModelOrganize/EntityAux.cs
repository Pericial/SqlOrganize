using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelOrganize
{
    /*
    Clase Entity Auxiliar utilizada unicamente para mapear valores json 
    y asignarlos correctamente a la clase Entity
    */
    public class EntityAux : Entity
    {
        public List<string> fieldsAdd { get; set; } 
        public List<string> fieldsSub { get; set; }
        public List<string> fkAdd { get; set; }
        public List<string> fkSub { get; set; }
        public List<string> uniqueAdd { get; set; }
        public List<string> uniqueSub { get; set; }
        public List<string> notNullAdd { get; set; }
        public List<string> notNullSub { get; set; }
    }
}
