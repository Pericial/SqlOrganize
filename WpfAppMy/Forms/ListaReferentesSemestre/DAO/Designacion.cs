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

        public IEnumerable<Dictionary<string, object>> referentesSemestre(Search search)
        {
            IEnumerable<object> idSedes = comisionDAO.IdSedesSemestre(search);
            return ContainerApp.db.Query("designacion").
                Where("$sede-_Id IN (@0) AND $cargo = '1'").
                Parameters(idSedes).ColOfDictCache();
        }

        public void UpdateValueRel(string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = ContainerApp.db.Persist("designacion").UpdateValueRel(key, value, source).Exec().RemoveCache();
        }
    }
}
