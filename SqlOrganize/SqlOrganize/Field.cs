using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{
    public class Field
    {
        public Db db { get; set; }
        public string name { get; set; }

        public string? alias { get; set; }

        /* 
        nombre de la entidad 
        */
        public string entity_name { get; set; }

        /* 
        si es clave foranea: Nombre de la entidad referenciada por la clave foranea 
        */
        public string? entity_ref_name { get; set; }

        /* 
        si es clave foranea: Nombre del field al que hace referencia de la entidad referenciada
        */
        public string? field_ref_name { get; set; } = "id";

        /* 
        tipo de datos generico 
            int
            blob
            string
            boolean
            float
            text
            timestamp
            date               
         */
        public string type { get; set; }

        /* 
        string con el tipo de field
            "pk": Clave primaria
            "nf": Field normal
            "mo": Clave foranea muchos a uno
            "oo": Clave foranea uno a uno
        */
        public string field_type { get; set; }

        /* valor por defecto */
        public object default_value { get; set; }

        public bool required { get; set; } = false;



        /* longitud maxima permitida */
        //protected int? _length;  

        /* valor maximo permitido */
        //protected object? _max;  

        /* valor minimo permitido */
        //protected object? _min;  

        /* lista de valores permitidos */
        //List<object> _values;

        public Entity entity() => this.db.entity(entity_name);

        public Entity entity_ref() => this.db.entity(entity_ref_name!);

        public bool is_main()
        {
            return this.db.entity(entity_name).main.Contains(name);
        }
    }
}
