using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.DAO.Fines
{
    public class AlumnoComision
    {

        public IEnumerable<object> IdsAlumnosActivosDeComisionesAutorizadasPorCalendario(object anio, object semestre)
        {
            var q = ContainerApp.dbFines.Query("alumno_comision")
                .Fields("alumno")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $estado = 'Activo'
                ")
                .Parameters(anio, semestre);

            return ContainerApp.dbCacheFines.Column<object>(q);
        }
    }
}
