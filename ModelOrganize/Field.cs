using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelOrganize
{
    public class Field
    {
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
        public string? refFieldName { get; set; }

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

        /// <summary>
        /// es not null?
        /// </summary>
        public bool notNull { get; set; }

        /* valor por defecto */
        public object? defaultValue { get; set; } = null;


        /// <summary>
        /// longitud maxima
        /// </summary>
        public ulong? maxLength { get; set; } = null;


        /// <summary>
        /// Lista de chequeos
        /// </summary>
        /// <example>
        /// [
        ///     field_name:true, //metodo exclusivo definido por el usuario
        ///     Type:"string",
        ///     Required:true,
        /// ]
        /// </example>
        public Dictionary<string, object> checks = new();

        /// <summary>
        /// Lista de reasignaciones
        /// </summary>
        /// <example>
        /// [
        ///     field_name:true, //metodo exclusivo definido por el usuario
        ///     Trim:" ",
        ///     Ltrim:" ", //no implementado
        ///     Rtrim:" ", //no implementado
        ///     RemoveMultipleSpaces:true, //no implementado (se puede definir un mejor nombre?)
        /// ]
        /// </example>
        public Dictionary<string, object> resets = new();

    }
}
