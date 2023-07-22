using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Values
{
    class alumno_comision : EntityValues
    {
        public alumno_comision(Db db, string entity_name, string field_id) : base(db, entity_name, field_id)
        {
        }

        public string estado_ingreso(string estado, DateTime alta)
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
