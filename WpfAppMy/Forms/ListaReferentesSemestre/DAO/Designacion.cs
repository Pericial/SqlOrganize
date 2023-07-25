using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Forms.ListaReferentesSemestre.DAO
{
    class Designacion
    {
        DAO.Comision comisionDAO = new ();

        public List<Dictionary<string, object>> referentesSemestre(Search search)
        {
            List<object> idSedes = comisionDAO.IdSedesSemestre(search);
            EntityQuery q = ContainerApp.Db().Query("designacion").
                Where("$sede-_Id IN (@0) AND $cargo = '1'").
                Parameters(idSedes);
            return ContainerApp.DbCache().ListDict(q);
        }

        public void UpdateValueRel(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.Db().Persist("comision").UpdateValueRel(key, value, source).Exec();
            ContainerApp.DbCache().Remove(p.detail);
        }
    }
}
