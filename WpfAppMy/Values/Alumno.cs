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
        public Alumno(Db _db, string _entityName, string? _fieldId = null) : base(_db, _entityName, _fieldId)
        {
        }

        public string PlanLabel()
        {
            string s = (string)values["plan-orientacion"] ?? "" + " ";
            s += (string)values["plan-resolucion"] ?? "" + " ";
            return s;

        }

        public static string cuatrimestre_ingreso(DateTime alta)
        {
            return alta.ToSemester().ToString() + "ºC " + alta.Year;
        }
    }
}
