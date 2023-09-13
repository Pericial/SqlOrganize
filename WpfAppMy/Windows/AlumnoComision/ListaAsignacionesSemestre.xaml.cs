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
            foreach(var item in list)
            {
                var o = item.ToObj<Model>();
                o.comision__label = item["sede-numero"].ToString() + " " + item["comision-division"].ToString() + "/" + item["planificacion-anio"] + " " + item["planificacion-semestre"];
                data.Add(o);
            }
        }
    }


    internal class Model : WpfAppMy.Model.Data.Model_alumno_comision
    {
        private string _persona__apellidos;
        public string persona__apellidos
        {
            get { return _persona__apellidos; }
            set { _persona__apellidos = value; NotifyPropertyChanged(); }
        }

        private string _persona__nombres;
        public string persona__nombres
        {
            get { return _persona__nombres; }
            set { _persona__nombres = value; NotifyPropertyChanged(); }
        }

        private string _persona__numero_documento;
        public string persona__numero_documento
        {
            get { return _persona__numero_documento; }
            set { _persona__numero_documento = value; NotifyPropertyChanged(); }
        }

        private DateTime _persona__fecha_nacimiento;
        public DateTime persona__fecha_nacimiento
        {
            get { return _persona__fecha_nacimiento; }
            set { _persona__fecha_nacimiento = value; NotifyPropertyChanged(); }
        }


        private string _persona__telefono;
        public string persona__telefono
        {
            get { return _persona__telefono; }
            set { _persona__telefono = value; NotifyPropertyChanged(); }
        }

        private string _persona__email;
        public string persona__email
        {
            get { return _persona__email; }
            set { _persona__email = value; NotifyPropertyChanged(); }
        }

        public string comision__label { get; set; }
    }
}
