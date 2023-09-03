using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.DAO.Pedidos
{
    public class Tickets
    {
        public List<Dictionary<string, object>> TicketPorId(int id)
        {
            var query = ContainerApp.dbPedidos.Query("wpwt_psmsc_tickets").
                Where("$id = @0").
                Parameters(id);

            return ContainerApp.dbCachePedidos.ListDict(query);
        }

        public List<object> DnisAlumnosConTicketDeSeguimiento()
        {
            var query = ContainerApp.dbPedidos.Query("wpwt_psmsc_tickets").
                Fields("cust_24").
                Where(@"
                    cust_24 IS NOT NULL
                    AND (
                        LOWER(subject) LIKE '%seguimiento%'
                        OR LOWER(subject) LIKE '%trayectoria%'
                        OR LOWER(subject) LIKE '%legajo%'
                    )
                ").
                Size(0);
            return ContainerApp.dbCachePedidos.Column<object>(query);

        }
    }
}
