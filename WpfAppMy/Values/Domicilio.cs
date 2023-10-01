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
    public class Domicilio : EntityValues
    {
        public Domicilio(Db _db, string entityName, string? _fieldId = null) : base(_db, entityName, _fieldId)
        {
        }

        public string ToStringShort()
        {
            string s = "";
            s += GetOrNull("calle")?.ToString() ?? "?";
            s += " e/ ";
            s += GetOrNull("entre")?.ToString() ?? "?";
            s += " n° ";
            s += GetOrNull("numero")?.ToString() ?? "?";
            return s;
        }

        public override string ToString()
        {
            string s = "";
            s += GetOrNull("calle")?.ToString() ?? "?";
            s += " e/ ";
            s += GetOrNull("entre")?.ToString() ?? "?";
            s += " n° ";
            s += GetOrNull("numero")?.ToString() ?? "?";
            s += " ";
            s += GetOrNull("barrio")?.ToString();
            s += " ";
            s += GetOrNull("localidad")?.ToString() ?? "?";
            return s.RemoveMultipleSpaces();
        }
    }
}
