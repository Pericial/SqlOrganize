using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Values
{
    class Comision : EntityValues
    {
        public Comision(Db _db, string _entity_name, string? _field_id) : base(_db, _entity_name, _field_id)
        {
        }
   
        /// <summary>
        /// Valor por defecto al campo derivado numero
        /// </summary>
        /// <remarks>Solo funciona con relaciones</remarks>
        /// <returns></returns>
        public Comision Default_numero_r()
        {
            values["numero_r"] = values["sede-numero"].ToString() + values["division"].ToString() + "/"+ values["planificacion-anio"].ToString() + values["planificacion-semestre"].ToString() ;
            return this;
        }

        /// <summary>
        /// Valor por defecto al campo derivado label
        /// </summary>
        /// <remarks>Solo funciona con relaciones</remarks>
        /// <returns></returns>
        public Comision Default_label_r()
        {
            Default("numero_r");
            values["label_r"] = values["numero_r"] + " " + values["calendario-anio"] + "-" + values["calendario-semestre"];
            return this;
        }

        public Comision Default_label_sede_r()
        {
            Default("label_r");
            values["label_sede_r"] = values["label_r"] + " " + values["sede-nombre"];
            return this;
        }

        /// <summary>
        /// Valor por defecto al campo derivado calendario_label
        /// </summary>
        /// <remarks>Solo funciona con relaciones</remarks>
        /// <returns></returns>
        public Comision Default_calendario__label()
        {
            values["calendario-label"] = values["calendario-anio"] + "-" + values["calendario-semestre"];
            return this;
        }

    }
}
