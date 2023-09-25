using Google.Protobuf.WellKnownTypes;
using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;

namespace WpfAppMy.Forms.ListaReferentesSemestre.DAO
{
    class Comision
    {

        public IEnumerable<object> IdSedesSemestre(Search search)
        {
            var q = ContainerApp.Db().Query("comision")
                .Fields("sede-_Id")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                ")
                .Parameters(search.calendario__anio, search.calendario__semestre);
            var count = 2;
            if (!search.autorizada.IsNullOrEmpty())
            {
                q.Where("AND $autorizada = @" + count + " ");
                q.Parameters(search.autorizada!);
                count++;
            }

            return q.ColumnCache();
    

        }

        

    }
}
