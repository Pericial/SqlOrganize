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

namespace WpfAppMy.Windows.AlumnoComision.DesactivarAlumnosNoCalificados
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        DAO dao = new();

        public Window1()
        {
            InitializeComponent();

            var alumnosComisiones = dao.AlumnosComisionesAutorizadasPorCalendario("2023", "1");

            foreach( var alumnoComision in alumnosComisiones )
            {
                dao.CantidadCalificacionesAprobadasDeAlumnoPorAnioTramo(alumnoComision["alumno"]);
            }

        }
    }
}
