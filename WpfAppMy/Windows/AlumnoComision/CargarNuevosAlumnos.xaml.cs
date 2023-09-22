using System.Collections.Generic;
using System.Windows;
using Utils;
using WpfAppMy.ViewModels;

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
            Loaded += CargarNuevosAlumnos_Loaded;
        }

        private void CargarNuevosAlumnos_Loaded(object sender, RoutedEventArgs e)
        {
            var data = ContainerApp.dao.Get("comision", IdComision);
            var obj = data.Obj<ComisionRel>();
            obj.label_sede_r = (string)ContainerApp.db.Values("comision").Values(data).Default("label_sede_r").Get("label_sede_r");
            DataContext = obj;
        }


        private void ProcesarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
