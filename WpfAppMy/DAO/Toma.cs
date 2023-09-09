using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.DAO
{
    public class Toma
    {
        public List<object> IdCursosConTomasAprobadasSemestre(object calendarioAnio, object calendarioSemestre)
        {
            var q = ContainerApp.Db().Query("toma").
                Fields("curso").
                Size(0).
                Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1
                    AND $estado = 'Aprobada'
                ")
                .Parameters(calendarioAnio, calendarioSemestre);

            return ContainerApp.DbCache().Column<object>(q);
        }

    }
}
