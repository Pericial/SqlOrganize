using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{
    public class Entity
    {
        /**
        Propiedades con sufijo "add" o "sub" existen para facilitar la configuracion
        Seran agregadas o quitadas de su atributo asociado en la inicializacion
        */
        public Db db { get; set; }

        /*
        Es necesario que se defina como propiedad con get y set, para poder 
        invocar dinamicamente al atributo mediante this!.GetType().GetProperty(name);
        */
        public string name { get; set; }
        
        public string alias { get; set; }

        public string? schema { get; set;  }

        public List<string> pk { get; set; } = new();
        public List<string> fields { get; set; } = new();
        public List<string> fk { get; set; } = new();


        /* 
        array dinamico para identificar univocamente a una entidad en un momento determinado
        @example
        identifier = ["fecha_anio", "fecha_semestre","persona-numero_documento"]
        */
        public List<string> identifier { get; set; } = new();

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
        Valores principales
        @example ["field1","field2",...]
        */
        public List<string> main { get; set; } = new();

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
        Cada juego de valores unicos multiples se define como una Lista
        */
        public List<List<string>> uniqueMultiple { get; set; } = new();

        public Dictionary<string, EntityTree> tree { get; set; } = new();

        public Dictionary<string, EntityRelation> relations { get; set; } = new();
        public string schema_ => String.IsNullOrEmpty(schema) ? schema : "";
        public string schemaName => schema + name;
        public string schemaNameAlias => schema + name + " AS " + alias;

        /*
        Campo de identificacion
        - Si existe un solo campo pk, entonces la pk sera el id. 
        - Si existe al menos un campo unique not null, se toma como id.     
        - Si existe multiples campos pk, se toman la concatenacion como id. 
        - Si existe multiples campos uniqueMultiple, se toman la concatenacion como id. 
        */
        public List<string> id { get; set; }


        protected List<Field> _Fields(List<string> fieldNames)
        {
            List<Field> fields = new();
            foreach (string fieldName in fieldNames)
                fields.Add(db.Field(name, fieldName));

            return fields;

        }

        /*
        fields no fk
        */
        public List<Field> Fields() => _Fields(fields);

        /*
        fields many to one
        */
        public List<Field> FieldsFk() => _Fields(fk);

    }
}
