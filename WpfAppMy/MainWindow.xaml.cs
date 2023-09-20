using QuestPDF.Infrastructure;
using System.Windows;

namespace WpfAppMy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            InitializeComponent();
        }


        private void listaComisiones_Click(object sender, RoutedEventArgs e)
        {
            Windows.Comision.ListaSemestre win = new();
            win.Show();
        }

        private void informeCoordinacionDistrital_Click(object sender, RoutedEventArgs e)
        {
            Forms.InformeCoordinacionDistrital.Window1 win = new();
            win.Show();

        }

        private void listaSedesSemestre_Click(object sender, RoutedEventArgs e)
        {
            Forms.ListaSedesSemestre.Window1 win = new();
            win.Show();

        }

        private void listaPlanificaciones_Click(object sender, RoutedEventArgs e)
        {
            Forms.ListaPlanificacion.Window1 win = new();
            win.Show();

        }

        private void listaModalidades_Click(object sender, RoutedEventArgs e)
        {
            Forms.ListaModalidad.Window1 win = new();
            win.Show();

        }

        private void ListaReferentes_Click(object sender, RoutedEventArgs e)
        {
            Forms.ListaReferentesSemestre.Window1 win = new();
            win.Show();

        }

        private void ListaCursos_Click(object sender, RoutedEventArgs e)
        {
            Windows.ListaCursos.Window1 win = new();
            win.Show();

        }
        private void ListaTomas_Click(object sender, RoutedEventArgs e)
        {
            Windows.ListaTomas.Window1 win = new();
            win.Show();

        }

        private void ProcesarDocentesProgramaFines_Click(object sender, RoutedEventArgs e)
        {
            Windows.ProcesarDocentesProgramaFines.Window1 win = new();
            win.Show();
        }
        private void ProcesarComisionesProgramaFines_Click(object sender, RoutedEventArgs e)
        {
            Windows.ProcesarComisionesProgramaFines.Window1 win = new();
            win.Show();
        }

        private void PruebaPdf_Click(object sender, RoutedEventArgs e)
        {
            Windows.TomaPosesionPdf.Window1 win = new();
            win.Show();
        }

        private void EnviarEmailToma_Click(object sender, RoutedEventArgs e)
        {
            Windows.EnviarEmailToma.Window1 win = new();
            win.Show();
        }

        private void ActualizarPlanAlumnos_Click(object sender, RoutedEventArgs e)
        {
            Windows.AlumnoComision.ActualizarPlanAlumnos.Window1 win = new();
            win.Show();
        }
        private void VerificarMateriasCruzadas_Click(object sender, RoutedEventArgs e)
        {
            Windows.AlumnoComision.VerificarMateriasCruzadas.Window1 win = new();
            win.Show();
        }

        private void VerificarAlumnosDuplicados_Click(object sender, RoutedEventArgs e)
        {
            Windows.AlumnoComision.VerificarAlumnosDuplicados.Window1 win = new();
            win.Show();
        }

        private void DesactivarAlumnosNoCalificados_Click(object sender, RoutedEventArgs e)
        {
            Windows.AlumnoComision.DesactivarAlumnosNoCalificados.Window1 win = new();
            win.Show();
        }

        private void ListaCursosSinTomasAprobadas_Click(object sender, RoutedEventArgs e)
        {
            Windows.Curso.ListaCursoSemestreSinTomasAprobadas.Window1 win = new();
            win.Show();
        }

        private void TransferirAlumnosActivos_Click(object sender, RoutedEventArgs e)
        {
            Windows.AlumnoComision.TransferirAlumnosActivos win = new();
            win.Show();
        }

        private void listaAsignacionesSemestre_Click(object sender, RoutedEventArgs e)
        {
            Windows.AlumnoComision.ListaAsignacionesSemestre win = new();
            win.Show();
        }

        private void listaAsignacionesProgramaFines_Click(object sender, RoutedEventArgs e)
        {
            PageManipulator.FormEstudiantes win = new();
            win.Show();
        }

        private void ListaAlumnosSinGenero_Click(object sender, RoutedEventArgs e)
        {
            Windows.AlumnoComision.AlumnosSemestreSinGenero win = new();
            win.Show();
        }

        private void CargarNuevosAlumnos_Click(object sender, RoutedEventArgs e)
        {
            Windows.AlumnoComision.CargarNuevosAlumnos win = new();
            win.Show();
        }
    }
}


    