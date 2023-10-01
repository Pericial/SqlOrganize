using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Windows.ProcesarComisionesProgramaFines
{
    internal class DAO
    {
        public IEnumerable<string> PfidComisiones()
        {
            return ContainerApp.db.Query("comision")
                .Fields("pfid")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $pfid IS NOT NULL
                ")
                .Parameters("2023", "2").ColOfDictCache().ColOfVal<string>("pfid");

        }

        public object? IdCurso(string pfidComision, string asignaturaCodigo)
        {
            var d = ContainerApp.db.Query("curso")
                .Fields("id")
                .Size(0)
                .Where(@"
                    $comision-pfid = @0 
                    AND $asignatura-codigo = @1
                    AND $calendario-anio = @2
                    AND $calendario-semestre = @3
                ")
                .Parameters(pfidComision, asignaturaCodigo, "2023", "2").DictCache();

            if (d.IsNullOrEmptyOrDbNull()) return null;
            return d["id"];

        }

        public object? IdPersona(string dni)
        {
            var d = ContainerApp.db.Query("persona")
                .Fields("id")
                .Size(0)
                .Where(@"
                    $numero_documento = @0 
                ")
                .Parameters(dni).DictCache();

            if (d.IsNullOrEmptyOrDbNull()) return null;
            return d["id"];

        }
    }
}
