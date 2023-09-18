using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.DAO
{
    public class Alumno
    {

        public List<Dictionary<string, object>> AlumnosPorIds(List<object> ids)
        {
            if (ids.Count == 0) return new();
            var q = ContainerApp.Db().Query("alumno")
               .Size(0)
               .Where(@"
                    $id IN ( @0 )
                ")
               .Parameters(ids);

            return ContainerApp.DbCache().ListDict(q);
        }
    }
}
