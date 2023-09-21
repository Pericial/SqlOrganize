using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Windows.EnviarEmailToma
{
    internal class DAO
    {
        public IEnumerable<Dictionary<string, object>> TomaAll()
        {
            var q = ContainerApp.Db().Query("toma")
                .Fields()
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $confirmada = false
                    AND $docente-email_abc IS NOT NULL
                ")
                .Order("$comision-pfid ASC")
                .Parameters("2023", "2");

            return ContainerApp.DbCache().ColOfDict(q);
        }
    }
}
