using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pedidos.Windows.MigrarAlumnos
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DAO.Pedidos.Tickets ticketsDao = new();
        DAO.Fines.Alumno alumnoDao = new ();
        List<string> logs = new();


        public Window1()
        {
            InitializeComponent();

            var dnis = ticketsDao.DnisAlumnosConTicketDeSeguimiento();
            List<Dictionary<string, object>> alumnos = alumnoDao.AlumnosActivosDeComisionesAutorizadasPorCalendario2("2023","1",dnis);


            foreach(var alumno in alumnos)
            {
                EntityValues v = ContainerApp.dbPedidos.Values("wpwt_psmsc_tickets").Default();
            }

            info.Text += String.Join(@"
", logs);
        }
    }
}
