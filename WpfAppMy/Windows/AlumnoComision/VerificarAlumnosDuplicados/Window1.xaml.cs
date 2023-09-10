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

namespace WpfAppMy.Windows.AlumnoComision.VerificarAlumnosDuplicados
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    /// 

    public partial class Window1 : Window
    {
        WpfAppMy.DAO.AlumnoComision asignacionDAO = new();
        WpfAppMy.DAO.Alumno alumnoDAO = new();
        List<string> logs = new();

        public Window1()
        {
            InitializeComponent();
            var idsAlumnos = asignacionDAO.IdsAlumnosActivosDuplicadosPorSemestre("2023", "1");
            var alumnos = alumnoDAO.AlumnosPorIds(idsAlumnos);

            alumnosGrid.ItemsSource = alumnos.ConvertToListOfObject<Alumno>();
        }

        internal class Alumno
        {
            public string id { get; set; }
            public string persona__nombres { get; set; }
            public string persona__apellidos { get; set; }
            public string persona__numero_documento { get; set; }
        }
    }
}
