using System.Windows;

namespace WpfAppMy.Windows.AlumnoComision
{
    /// <summary>
    /// Lógica de interacción para CargarNuevosAlumnos.xaml
    /// </summary>
    public partial class CargarNuevosAlumnos : Window
    {
        private string? idComision;
        public CargarNuevosAlumnos(string? idComision = null)
        {

            InitializeComponent();
            this.idComision = idComision;
            this.comisionTextBox.Text = idComision;
        }

        private void ProcesarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
