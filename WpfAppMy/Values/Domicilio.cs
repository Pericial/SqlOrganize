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

        public string LabelShort()
        {
            return values["calle"] + " e/ " + values["entre"] + " N° " + values["numero"];
        }

        public string Label()
        {
            string r = values["calle"] + " e/ " + values["entre"] + " N° " + values["numero"];
            if (!values["barrio"].IsNullOrEmpty())
                r += " " + values["barrio"];

            r += " " + values["localidad"];
            return r;
        }
    }
}
