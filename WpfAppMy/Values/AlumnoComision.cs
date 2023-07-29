using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Values
{
    class AlumnoComision : EntityValues
    {
        public AlumnoComision(Db _db, string _entity_name, string? _field_id) : base(_db, _entity_name, _field_id)
        {
        }

        public static string estado_ingreso(string estado, DateTime alta)
        {
            if (estado.ToLower() == "no activo")
                return "TRAYECTORIA INTERRUMPIDA";
            if (estado.ToLower() == "activo" && alta.ToYearSemester() == DateTime.Now.ToYearSemester())
                return "INGRESANTE";
            if (estado.ToLower() == "activo" && alta.ToYearSemester() != DateTime.Now.ToYearSemester())
                return "CONTINÚA TRAYECTORIA";
            return estado;
        }
    }
}
