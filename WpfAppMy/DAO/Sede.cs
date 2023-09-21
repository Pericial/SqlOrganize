using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.DAO
{
    public class Sede
    {
        public IEnumerable<Dictionary<string, object>> BusquedaAproximada(string search)
        {
            var q = ContainerApp.Db().Query("sede").
                Fields("id, nombre").
                Size(10).
                Where(@"
                    $nombre LIKE @0 
                    OR $numero LIKE @1
                ")
                .Parameters("%" + search + "%", "%" + search + "%")
                .Order("$nombre ASC");

            return ContainerApp.DbCache().ColOfDict(q);
        }

    }
}
