using System.Collections.Generic;
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
            Loaded += CargarNuevosAlumnos_Loaded;
        }

        private void CargarNuevosAlumnos_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            //Dictionary<string, object> data_ = ContainerApp.dao.Get(idComision);
            //DataContext = data.ToObj<Model>();
        }



        private void ProcesarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
