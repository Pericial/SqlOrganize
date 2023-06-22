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

        public string pk { get; set; }
        public List<string> fields { get; set; }
        public List<string> fieldsAdd { get; set; } 
        public List<string> fieldsSub { get; set; }
        public List<string> fk { get; set; }
        public List<string> fkAdd { get; set; }
        public List<string> fkSub { get; set; }


        /* 
        array dinamico para identificar univocamente a una entidad en un momento determinado
        @example
        identifier = ["fecha_anio", "fecha_semestre","persona-numero_documento"]
        */
        public List<string> identifier { get; set; }

        /*
        Valores por defecto para ordenamiento
        @example ["field1"=>"asc","field2"=>"desc",...];
        */
        public Dictionary<string, string> orderDefault { get; set; }

        /*
        Valores no administrables
        @example ["field1","field2",...]
        */
        public List<string> noAdmin { get; set; }

        /*
        Valores principales
        @example ["field1","field2",...]
        */
        public List<string> main { get; set; }

        /*
        Valores unicos
        Una entidad puede tener varios campos que determinen un valor unico
        @example ["field1","field2",...]
        */
        public List<string> unique { get; set; }
        public List<string> uniqueAdd { get; set; }
        public List<string> uniqueSub { get; set; }

        /*
        Valores no nulos        
        */
        public List<string> notNull { get; set; }
        public List<string> notNullAdd { get; set; }
        public List<string> notNullSub { get; set; }

        /*
        Valores unicos multiples
        Solo puede especificarse un juego de campos unique_multiple
        */
        public List<string> uniqueMultiple { get; set; }

        public string schema_ => String.IsNullOrEmpty(schema) ? schema : "";
        public string schemaName => schema + name;
        public string schemaNameAlias => schema + name + " AS " + alias;

       
        protected List<Field> _Fields(List<string> fieldNames)
        {
            List<Field> fields = new();
            foreach (string fieldName in fieldNames)
                fields.Add(db.field(name, fieldName));

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