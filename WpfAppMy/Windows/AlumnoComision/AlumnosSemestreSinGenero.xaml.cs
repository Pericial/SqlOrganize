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
using WpfAppMy.Data;

namespace WpfAppMy.Windows.AlumnoComision
{
    /// <summary>
    /// Lógica de interacción para AlumnosSemestreSinGenero.xaml
    /// </summary>
    public partial class AlumnosSemestreSinGenero : Window
    {
        private DAO.AlumnoComision dataDAO = new();
        private ObservableCollection<Data_alumno_rel> alumnosSinGenero = new();
        public AlumnosSemestreSinGenero()
        {
            InitializeComponent();
            dataGrid.ItemsSource = alumnosSinGenero;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            IEnumerable<Dictionary<string, object>> alumnos = dataDAO.AlumnosActivosDeComisionesAutorizadasPorSemestreSinGenero("2023", "2");

            foreach (var alumno in alumnos)
            {
                var a = alumno.ToObj<Data_alumno_rel>();
                var nombres = a.persona__nombres.Split(" ");
                string? genero = null;

                foreach(var nombre in nombres)
                {
                    var p = ContainerApp.db.Persist("persona");

                    var lastChar = nombre.ToLower()[nombre.Length - 1];
                    
                    if (lastChar.Equals('o'))
                    {
                        genero = "Masculino";
                    } else if (lastChar.Equals('a'))
                    {
                        genero = "Femenino";
                    }

                    if (!genero.IsNullOrEmpty())
                    {
                        p.UpdateValue("genero", genero, new List<object>() { a.persona__id }).Exec();
                        ContainerApp.dbCache.Remove(p.detail);
                        genero = null;
                        break;
                    }
                }

                alumnosSinGenero.Clear();
                alumnos = dataDAO.AlumnosActivosDeComisionesAutorizadasPorSemestreSinGenero("2023", "2");
                alumnosSinGenero.AddRange(alumnos.ToListOfObj<Data_alumno_rel>());
            }


        }
    }


    
}
