using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Windows.MigrarAlumnos
{
    internal class DAO
    {
        public List<Dictionary<string, object>> AlumnosAll()
        {
            var q = ContainerApp.dbFines.Query("alumno_comision")
                .Fields("alumno")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $estado = 'Activo'
                ")
                .Parameters("2023", "1");

            List<string> idAlumnos = ContainerApp.dbCacheFines.Column<string>(q);

            return ContainerApp.dbCacheFines.ListDict("alumno", idAlumnos.ToArray());
        }
    }
}
