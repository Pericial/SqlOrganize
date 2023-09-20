using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace ModelOrganize
{
    public class Entity
    {
  
        public string name { get; set; }
        
        public string alias { get; set; }

        public string? schema { get; set;  }

        public List<string> pk { get; set; } = new();
        public List<string> fields { get; set; } = new();

        public List<string> fk { get; set; } = new();

        /*
        Valores por defecto para ordenamiento
        @example ["field1"=>"asc","field2"=>"desc",...];
        */
        public Dictionary<string, string> orderDefault { get; set; } = new();

        /*
        Valores no administrables
        @example ["field1","field2",...]
        */
        public List<string> noAdmin { get; set; } = new();

        /*
        Valores unicos
        Una entidad puede tener varios campos que determinen un valor unico
        @example ["field1","field2",...]
        */
        public List<string> unique { get; set; } = new();

        /*
        Valores no nulos        
        */
        public List<string> notNull { get; set; } = new();

        /*
        Valores unicos multiples
        Solo puede especificarse un juego de campos unique_multiple
        */
        public List<List<string>> uniqueMultiple { get; set; } = new();

        //public Dictionary<string, EntityTree> tree { get; set; } = new();

        //public Dictionary<string, EntityRelation> relations { get; set; } = new();
    

        /*
        Campo de identificacion
        - Si existe un solo campo pk, entonces la pk sera el id. 
        - Si existe al menos un campo unique not null, se toma como id.     
        - Si existe multiples campos pk, se toman la concatenacion como id. 
        - Si existe multiples campos uniqueMultiple, se toman la concatenacion como id. 
        */
        public List<string> id { get; set; }

        public Dictionary<string, EntityTree> tree { get; set; } = new();
        public Dictionary<string, EntityRelation> relations { get; set; } = new();

    }
}
