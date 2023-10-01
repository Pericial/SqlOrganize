using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

            EntityValues? v = ValuesTree("sede");
            s += (!v.IsNullOrEmpty()) ? v.GetOrNull("numero")?.ToString() : "?";
            s += GetOrNull("division")?.ToString() ?? "?";
            s += "/";
            v = ValuesTree("planificacion");
            if (!v.IsNullOrEmpty())
            {
                s += v.GetOrNull("anio")?.ToString() ?? "?"; ;
                s += v.GetOrNull("semestre")?.ToString() ?? "?"; ;
            } else
            {
                s += "?";
            }
            return s.Trim();
        }

    
        public string ToStringNombreSede()
        {
            var s = ToString();
            s += " ";
            s += ValuesTree("sede")?.GetOrNull("nombre")?.ToString() ?? "?";
            return s;
        }

        public string CalendarioAnioSemestre()
        {
            string s = "";
            var v = ValuesTree("calendario");
            if (v.IsNullOrEmpty())
            {
                s += v.GetOrNull("anio")?.ToString() ?? "?";
                s += "-";
                s += v.GetOrNull("semestre")?.ToString() ?? "?";
            }
            return s;
        }

        public override string ToString()
        {
            var s = Numero();
            s += " ";
            s += CalendarioAnioSemestre();
            return s;

        }


    }
}
