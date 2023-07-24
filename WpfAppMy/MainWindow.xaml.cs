using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utils;
using WpfAppMy.Forms;

namespace WpfAppMy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }


        private void listaComisiones_Click(object sender, RoutedEventArgs e)
        {
            Forms.ListaComisiones.Window1 win = new();
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
    }
}


    