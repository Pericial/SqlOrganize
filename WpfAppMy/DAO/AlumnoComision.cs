using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.DAO
{
    public class AlumnoComision
    {

        public List<Dictionary<string, object>> AsignacionesPorIdsComisiones(List<object> idsComisiones)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields()
                .Size(0)
                .Where(@"
                    $comision IN (@0) 
                ")
                .Parameters(idsComisiones);
            return ContainerApp.DbCache().ListDict(q);
        }

        public List<Dictionary<string, object>> AsignacionesActivasPorIdsComisiones(List<object> idsComisiones)
        {
            var q = ContainerApp.Db().Query("alumno_comision")
                .Fields()
                .Size(0)
                .Where(@"
                    $comision IN (@0) AND $estado = 'Activo'
                ")
                .Parameters(idsComisiones);
            return ContainerApp.DbCache().ListDict(q);
        }

    }
}
