using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Forms.ListaComisiones.DAO
{
    class Sede
    {

        public List<Dictionary<string, object>> Search(string search)
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
                
            return ContainerApp.QueryCache().ListDict(q);
        }

    }
}
