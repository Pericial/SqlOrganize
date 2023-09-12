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
using Utils;

namespace Pedidos.Windows.MigrarAlumnos
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DAO.Pedidos.Tickets ticketsDao = new();
        DAO.Fines.Alumno alumnoDao = new();
        List<string> logs = new();


        public Window1()
        {
            InitializeComponent();

            var dnis = ticketsDao.DnisAlumnosConTicketDeSeguimiento();
            List<Dictionary<string, object>> alumnos = alumnoDao.AlumnosActivosDeComisionesAutorizadasPorCalendario2("2023", "1", dnis);





            foreach (var alumno in alumnos)
            {
                Alumno a = alumno.ToObject<Alumno>();
                EntityValues ticketsValues = ContainerApp.dbPedidos.Values("wpwt_psmsc_tickets").Default().
                    Set("subject", "Seguimiento alumno " + a.persona__nombres + " " + a.persona__apellidos).
                    Set("status", 10).
                    Set("cust_24", a.persona__numero_documento).
                    Set("cust_27", a.persona__telefono).
                    Set("cust_28", "Carga automatica de alumno activo en período 2023-1").
                    Set("assigned_agent", "");

                ContainerApp.dbPedidos.Persist("wpwt_psmsc_tickets").Insert(ticketsValues.values).Exec();

                EntityValues threadsValues = ContainerApp.dbPedidos.Values("wpwt_psmsc_threads").Default().
                    Set("ticket", ticketsValues.Get("id")).
                    Set("body", @"
                        <p>Alumno activo en período 2023-1</p>
                    ");
                ContainerApp.dbPedidos.Persist("wpwt_psmsc_threads").Insert(threadsValues.values).Exec();
                /*EntityValues threadsValues2 = ContainerApp.dbPedidos.Values("wpwt_psmsc_threads").Default().
                    Set("ticket", ticketsValues.Get("id")).
                    Set("body", @"
                        <p>Alumno activo en período 2023-1</p>
                    ").
                    Set("type","note");
                ContainerApp.dbPedidos.Persist("wpwt_psmsc_threads").Insert(threadsValues2.values).Exec();*/

            }

            info.Text += String.Join(@"
", logs);
        }
    }

    internal class Alumno
    {
        public string persona__nombres { get; set; }
        public string persona__apellidos { get; set; }
        public string persona__numero_documento { get; set; }
        public string persona__telefono { get; set; }

        public string plan__orientacion { get; set; }
        public string plan__resolucion { get; set; }
        public string plan__distribucion_horaria { get; set; }


        public string id { set; get; }
        /*public string? anio_ingreso { set; get; }
        public string? estado_inscripcion { set; get; }
        public string? anio_inscripcion { set; get; }
        public string? semestre_inscripcion { set; get; }
        public bool? anio_inscripcion_completo { set; get; }
        public string? observaciones { set; get; }

        */


    }
}
