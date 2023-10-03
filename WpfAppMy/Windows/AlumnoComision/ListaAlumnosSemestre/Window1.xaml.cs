using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Utils;
using WpfAppMy.Data;

namespace WpfAppMy.Windows.AlumnoComision.ListaAlumnosSemestre
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private DAO.AlumnoComision asignacionDAO = new();
        private SqlOrganize.DAO dao = new (ContainerApp.db);
        private Data_alumno_comision_r search = new();
        private ObservableCollection<Asignacion> asignacionData = new();
        
        public Window1()
        {
            InitializeComponent();

            estadoCombo.SelectedValuePath = "Key";
            estadoCombo.DisplayMemberPath = "Value";
            estadoCombo.Items.Add(new KeyValuePair<string?, string>(null, "(Todos)"));
            estadoCombo.Items.Add(new KeyValuePair<string, string>("Activo", "Activo"));
            estadoCombo.Items.Add(new KeyValuePair<string, string>("No activo", "No activo"));
            estadoCombo.Items.Add(new KeyValuePair<string, string>("Mesa", "Mesa"));

            asignacionGrid.ItemsSource = asignacionData;

            search.calendario__anio = 2023;
            search.calendario__semestre = 2;
            search.estado = "Activo";
            DataContext = search;

            Loaded += Window1_Loaded;
        }

        private void LoadAsignaciones()
        {
            var data = dao.Search("alumno_comision", search);
            asignacionData.Clear();
            foreach (var d in data)
            {
                var v = (Values.AlumnoComision)ContainerApp.db.Values("alumno_comision").Values(d);
                var o = d.Obj<Asignacion>();
                o.comision__label = v.ValuesTree("comision")?.ToString() ?? "";
                asignacionData.Add(o);
            }
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAsignaciones();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            LoadAsignaciones();
        }
    }
}
