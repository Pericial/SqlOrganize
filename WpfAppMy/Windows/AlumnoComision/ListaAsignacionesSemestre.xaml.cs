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

namespace WpfAppMy.Windows.AlumnoComision
{
    /// <summary>
    /// Lógica de interacción para ListaAsignacionesSemestre.xaml
    /// </summary>
    public partial class ListaAsignacionesSemestre : Window
    {

        private DAO.AlumnoComision dataDAO = new();
        private ObservableCollection<Model> data = new();

        public ListaAsignacionesSemestre()
        {
            InitializeComponent();
            dataGrid.ItemsSource = data;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            List<Dictionary<string, object>> list = dataDAO.AsignacionesDeComisionesAutorizadasPorSemestre("2023", "2");
            data.Clear();
            data.AddRange(list.ToListOfObj<Model>());

        }
    }


    public class Model
    {
        public string id { get; set; }
        public string descripcion { get; set; }
        public DateTime creado { get; set; }
        public bool autorizado { get; set; }
    }
}
