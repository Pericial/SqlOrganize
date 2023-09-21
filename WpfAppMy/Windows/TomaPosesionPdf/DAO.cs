using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Windows.TomaPosesionPdf
{
    internal class DAO
    {
        public IEnumerable<Dictionary<string, object>> TomaAll(Search search)
        {
            var q = ContainerApp.Db().Query("toma")
                .Fields()
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                ")
                .Parameters(search.calendario__anio, search.calendario__semestre);

            return ContainerApp.DbCache().ListDict(q);
        }
    }
}
