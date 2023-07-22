using SqlOrganize;
using SqlOrganizeMy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMy.Forms.InformeCoordinacionDistrital.Data
{
    public class Calificacion 
    {
        public List<Dictionary<string,object>> AprobadasPorAlumnoPlan(string idAlumno, string idPlan)
        {
            var q = ContainerApp.Db().Query("calificacion")
                    .Size(0)
                    .Where("$alumno = @0 AND ($nota_final >= 7 OR $crec >= 4) AND $planificacion_dis-plan = @1")
                    .Parameters(idAlumno, idPlan)
                    .Order("$planificacion_dis-anio ASC, $planificacion_dis-semestre ASC, $disposicion-orden_informe_coordinacion_distrital ASC");
            return ContainerApp.DbCache().ListDict(q);
        }
    }

}
