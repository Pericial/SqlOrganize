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

        public Domicilio Default_label_short()
        {
            values["label_short"] =  values["calle"] + " e/ " + values["entre"] + " N° " + values["numero"];
            return this;
        }

        public Domicilio Default_label()
        {
            string r = values["calle"] + " e/ " + values["entre"] + " N° " + values["numero"];
            if (!values["barrio"].IsNullOrEmpty())
                r += " " + values["barrio"];

            values["label"] = r + " " + values["localidad"];
            return this;
        }
    }
}
