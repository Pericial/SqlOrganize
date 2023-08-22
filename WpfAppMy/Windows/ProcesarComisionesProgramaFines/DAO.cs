using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Windows.ProcesarComisionesProgramaFines
{
    internal class DAO
    {
        public List<string> PfidComisiones()
        {
            var q = ContainerApp.Db().Query("comision")
                .Fields("pfid")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $pfid IS NOT NULL
                ")
                .Parameters("2023", "2");

            return ContainerApp.DbCache().Column<string>(q);
        }

        public string IdCurso(string pfidComision, string asignaturaCodigo)
        {
            var q = ContainerApp.Db().Query("curso")
                .Fields("id")
                .Size(0)
                .Where(@"
                    $comision-pfid = @0 
                    AND $asignatura-codigo = @1
                    AND $calendario-anio = @2
                    AND $calendario-semestre = @3
                ")
                .Parameters(pfidComision, asignaturaCodigo, "2023", "2");

            return ContainerApp.DbCache().Value<string>(q);
        }

        public string IdPersona(string dni)
        {
            var q = ContainerApp.Db().Query("persona")
                .Fields("id")
                .Size(0)
                .Where(@"
                    $numero_documento = @0 
                ")
                .Parameters(dni);

            return ContainerApp.DbCache().Value<string>(q);
        }
    }
}
