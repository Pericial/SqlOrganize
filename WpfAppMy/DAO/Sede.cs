using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfAppMy.DAO
{
    class Sede
    {

        public int CantidadTotal()
        {
            var q = ContainerApp.Db().Query("sede").
                Select("COUNT(*)").
                Size(0);

            return (int)ContainerApp.DbCache().Value<long>(q);
        }


        public List<Dictionary<string, object>> FiltroPaginacion(int page, int size)
        {
            var q = ContainerApp.Db().Query("sede").Size(size).Page(page);
            return ContainerApp.DbCache().ListDict(q);
        }

        public void UpdateValue(string key, object value, string _Id)
        {
            EntityPersist p = ContainerApp.Db().Persist("sede").UpdateValue(key, value, _Id).Exec();
            ContainerApp.DbCache().Remove(p.detail);
        }

    }
}
