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
    class Alumno : EntityValues
    {
        public Alumno(Db db, string entityName, string? fieldId = null) : base(db, entityName, fieldId)
        {
        }

        public string PlanLabel()
        {
            string s = "";
            //s += values.ContainsKey("nombres") && !values["nombres"].IsNullOrEmpty() ? values["nombres"]!.ToString() + " " : "";
            //s += values.ContainsKey("apellidos") && !values["apellidos"].IsNullOrEmpty() ? values["apellidos"]!.ToString() + " " : "";
            //s += values.ContainsKey("numero_documento") && !values["numero_documento"].IsNullOrEmpty() ? values["numero_documento"]!.ToString() : "";
            return s.Trim();

        }
    }
}