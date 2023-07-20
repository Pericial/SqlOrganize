using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.DAO
{
    class Sede
    {

        public int CantidadTotal()
        {
            var q = ContainerApp.Db().Query("sede").
                Select("COUNT(*)").
                Size(0);

            return (int)ContainerApp.QueryCache().Value<long>(q);
        }


        public List<Dictionary<string, object>> ConsultaPaginacion(int page, int size)
        {
            var q = ContainerApp.Db().Query("sede").Size(size).Page(page);
            return ContainerApp.QueryCache().ListDict(q);
        }


        public List<Dictionary<string, object>> Search(string search)
        {
            var q = ContainerApp.Db().Query("sede").
                Fields("id, numero, nombre").
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
