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
        public string Numero()
        {
            var s = "";
            s += values.ContainsKey("sede-numero") && !values["sede-numero"].IsNullOrEmptyOrDbNull() ? values["sede-numero"].ToString() : "";
            s += values.ContainsKey("division") && !values["division"].IsNullOrEmptyOrDbNull() ? values["division"].ToString() : "";
            s += "/";
            s += values.ContainsKey("planificacion-anio") && !values["planificacion-anio"].IsNullOrEmptyOrDbNull() ? values["planificacion-anio"].ToString() : "";
            s += values.ContainsKey("planificacion-semestre") && !values["planificacion-semestre"].IsNullOrEmptyOrDbNull() ? values["planificacion-semestre"].ToString() : "";
            return s.Trim();
        }

        public string Label()
        {
            var s = Numero();
            s += " ";
            s += CalendarioLabel();
            return s.Trim();
        }

        public string LabelWithSede()
        {
            var s =  Label();
            s += " ";
            s += values.ContainsKey("sede-nombre") && !values["sede-nombre"].IsNullOrEmptyOrDbNull() ? values["sede-nombre"].ToString() : "";
            return s.Trim();
        }

        public string CalendarioLabel()
        {
            string s = values.ContainsKey("calendario-anio") && !values["calendario-anio"].IsNullOrEmptyOrDbNull() ? values["calendario-anio"].ToString() : "";
            s += "-";
            s += values.ContainsKey("calendario-semestre") && !values["calendario-semestre"].IsNullOrEmptyOrDbNull() ? values["calendario-semestre"].ToString() : "";
            return s.Trim();
        }


    }
}
