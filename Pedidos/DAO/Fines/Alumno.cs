using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.DAO.Fines
{
    public class Alumno
    {


        public List<Dictionary<string, object>> AlumnosActivosDeComisionesAutorizadasPorCalendario(object anio, object semestre)
        {
            var ac = new AlumnoComision();
            var idAlumnos = ac.IdsAlumnosActivosDeComisionesAutorizadasPorCalendario(anio, semestre);
            var q = ContainerApp.dbFines.Query("alumno")
                .Size(0).Where(@"$id IN ( @0 )").Parameters(idAlumnos);
            
            return ContainerApp.dbCacheFines.ColOfDict(q);
        }

        public List<Dictionary<string, object>> AlumnosActivosDeComisionesAutorizadasPorCalendario2(object anio, object semestre, List<object> dnisAQuitar)
        {
            var ac = new AlumnoComision();
            var idAlumnos = ac.IdsAlumnosActivosDeComisionesAutorizadasPorCalendario(anio, semestre);
            var q = ContainerApp.dbFines.Query("alumno")
                .Size(0)
                .Where(@"$id IN ( @0 ) AND $persona-numero_documento NOT IN ( @1 )")
                .Parameters(idAlumnos, dnisAQuitar);

            return ContainerApp.dbCacheFines.ColOfDict(q);
        }
    }
}
