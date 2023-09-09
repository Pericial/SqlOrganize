using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.DAO
{
    internal class Curso
    {
        public List<Dictionary<string, object>> CursosAutorizadosSemestre(object calendarioAnio, object calendarioSemestre, object? sede = null, bool? autorizada = null)
        {
            var q = ContainerApp.Db().Query("curso")
                .Fields()
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $comision-autorizada = true 
                ")
                .Parameters(calendarioAnio, calendarioSemestre);

            return ContainerApp.DbCache().ListDict(q);
        }
    }
}
