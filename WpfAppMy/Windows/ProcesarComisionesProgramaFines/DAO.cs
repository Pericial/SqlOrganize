using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Windows.ProcesarComisionesProgramaFines
{
    internal class DAO
    {
        public IEnumerable<string> PfidComisiones()
        {
            return ContainerApp.Db().Query("comision")
                .Fields("pfid")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $pfid IS NOT NULL
                ")
                .Parameters("2023", "2").ColumnCache<string>() ;

        }

        public string? IdCurso(string pfidComision, string asignaturaCodigo)
        {
            return ContainerApp.Db().Query("curso")
                .Fields("id")
                .Size(0)
                .Where(@"
                    $comision-pfid = @0 
                    AND $asignatura-codigo = @1
                    AND $calendario-anio = @2
                    AND $calendario-semestre = @3
                ")
                .Parameters(pfidComision, asignaturaCodigo, "2023", "2").ValueCache<string>();

        }

        public string? IdPersona(string dni)
        {
            return ContainerApp.Db().Query("persona")
                .Fields("id")
                .Size(0)
                .Where(@"
                    $numero_documento = @0 
                ")
                .Parameters(dni).ValueCache<string>();
        }
    }
}
