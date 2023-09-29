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

        public string Label()
        {
            string r = "";
            r += (values.ContainsKey("calle") && !values["calle"].IsNullOrEmptyOrDbNull() ) ? 
                values["calle"] : "";

            r += (values.ContainsKey("entre") && !values["entre"].IsNullOrEmptyOrDbNull()) ?
              " e/ " + values["entre"] : "";

            r += (values.ContainsKey("numero") && !values["numero"].IsNullOrEmptyOrDbNull()) ?
              " N° " + values["numero"] : "";

            r += (values.ContainsKey("barrio") && !values["barrio"].IsNullOrEmptyOrDbNull()) ?
              " " + values["barrio"] : "";

            r += (values.ContainsKey("localidad") && !values["localidad"].IsNullOrEmptyOrDbNull()) ?
              " " + values["localidad"] : "";

            return r.Trim().RemoveMultipleSpaces();
        }
    }
}
