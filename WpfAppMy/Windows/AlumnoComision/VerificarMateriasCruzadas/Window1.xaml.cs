using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfAppMy.Windows.AlumnoComision.VerificarMateriasCruzadas
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DAO.AlumnoComision asignacionDAO = new();
        DAO.Calificacion calificacionDAO = new();

        List<string> logs = new();


        public Window1()
        {
            InitializeComponent();

            var idsAlumnos = asignacionDAO.IdsAlumnosDeComisionesAutorizadasPorSemestre("2023", "1");
            var idsAlumnosMateriasCruzadas = calificacionDAO.IdsAlumnosConCalificacionesAprobadasCruzadasNoArchivadas(idsAlumnos);
            var calificaciones = calificacionDAO.CalificacionesAprobadasDeAlumnosNoArchivadas(idsAlumnosMateriasCruzadas);

            calificacionesGrid.ItemsSource = calificaciones.ConvertToListOfObject<Calificacion>();

        }
    }

    internal class Calificacion
    {
        public string id { get; set; }

        public string persona__nombres { get; set; }
        public string persona__apellidos { get; set; }
        public string persona__numero_documento { get; set; }
        public string plan_pla__id { get; set; }
        public string plan_pla__orientacion { get; set; }

        public string plan_pla__distribucion_horaria { get; set; }

        public string planificacion_dis__anio { get; set; }
        public string planificacion_dis__semestre { get; set; }

        public string asignatura_dis__nombre { get; set; }
        public decimal nota_final { get; set; }

        public decimal crec { get; set; }










    }
}
