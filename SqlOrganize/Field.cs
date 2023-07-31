﻿using System;
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
        public string entityName { get; set; }

        /* 
        si es clave foranea: Nombre de la entidad referenciada por la clave foranea 
        */
        public string? refEntityName { get; set; }

        /* 
        si es clave foranea: Nombre del field al que hace referencia de la entidad referenciada
        */
        public string? refFieldName { get; set; } = "id";

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
        public string dataType { get; set; }

        /* 
        string con el tipo de field
            "pk": Clave primaria
            "nf": Field normal
            "mo": Clave foranea muchos a uno
            "oo": Clave foranea uno a uno
        */

        /* valor por defecto */
        public object defaultValue { get; set; }


        /* longitud maxima permitida */
        //protected int? _length;  

        /* valor maximo permitido */
        //protected object? _max;  

        /* valor minimo permitido */
        //protected object? _min;  

        /* lista de valores permitidos */
        //List<object> _values;

        public Dictionary<string, object> checks;

        public Dictionary<string, object> resets;

        public Entity Entity() => this.db.Entity(entityName);

        public Entity RefEntity() => this.db.Entity(refEntityName!);

        public bool IsRequired()
        {
            var entity = this.db.Entity(entityName);
            return (entity.notNull.Contains(this.name));
        }

    }
}
