using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.DAO
{
    public class Alumno
    {

        public IEnumerable<Dictionary<string, object>> AlumnosPorIds(IEnumerable<object> ids)
        {
            if (ids.Count() == 0) return Enumerable.Empty<Dictionary<string, object>>();
            return ContainerApp.Db().Query("alumno").ColOfDictCacheByIds(ids);
        }
    }
}
