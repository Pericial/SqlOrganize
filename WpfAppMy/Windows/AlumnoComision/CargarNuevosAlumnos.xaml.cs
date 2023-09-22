using System.Collections.Generic;
using System.Windows;
using Utils;

namespace WpfAppMy.Windows.AlumnoComision
{
    /// <summary>
    /// Lógica de interacción para CargarNuevosAlumnos.xaml
    /// </summary>
    public partial class CargarNuevosAlumnos : Window
    {
        private string? IdComision;

        public CargarNuevosAlumnos(string? idComision = null)
        {

            InitializeComponent();
            this.IdComision = idComision;
            this.comisionTextBox.Text = idComision;
            Loaded += CargarNuevosAlumnos_Loaded;
        }

        private void CargarNuevosAlumnos_Loaded(object sender, RoutedEventArgs e)
        {
            var data = ContainerApp.dao.Get("comision", IdComision);
            DataContext = data.Obj<Comision>
        }

        private void LoadData()
        {
            //Dictionary<string, object> data_ = ContainerApp.dao.Get(idComision);
            //DataContext = data.Obj<Model>();
        }



        private void ProcesarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
