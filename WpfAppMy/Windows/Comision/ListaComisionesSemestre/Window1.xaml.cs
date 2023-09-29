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
using WpfAppMy.ViewModels;

namespace WpfAppMy.Windows.Comision.ListaComisionesSemestre
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ComisionSearch comisionSearch = new();
        private DAO.Sede sedeDAO = new();
        private ObservableCollection<Comision> comisionData = new();
        private SqlOrganize.DAO dao = new(ContainerApp.db);

        public Window1()
        {
            InitializeComponent();
            this.sedeList.Visibility = Visibility.Collapsed; //al iniciar que no se vea la lista de opciones (estara vacia)
            comisionGrid.CellEditEnding += ComisionGrid_CellEditEnding!;
            comisionGrid.ItemsSource = comisionData;

            DataContext = comisionSearch;

            this.autorizadaCombo.SelectedValuePath = "Key";
            this.autorizadaCombo.DisplayMemberPath = "Value";
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool?, string>(null, "(Todos)"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(true, "Sí"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(false, "No"));

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            IEnumerable<Dictionary<string, object>> list = dao.Search("comision", comisionSearch);
            comisionData.Clear();
            foreach (IDictionary<string, object> item in list)
            {
                var comision = (Values.Comision)ContainerApp.db.Values("comision").Values(item);
                var o = item.Obj<Comision>();
                o.label = comision.Label();
                o.domicilio__label = comision.Label("domicilio");
                comisionData.Add(o);
            }
        }


        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SedeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.sedeList.SelectedIndex > -1)
                if (this.sedeText.Text.Equals(((Data_sede)this.sedeList.SelectedItem).nombre))
                    return;
                else
                {
                    this.sedeList.SelectedIndex = -1;
                    this.comisionSearch.sede = null;
                }



            if (string.IsNullOrEmpty(this.sedeText.Text) || this.sedeText.Text.Length < 3)
            {
                this.sedeList.Visibility = Visibility.Collapsed;
                return;
            }

            this.sedeList.Visibility = Visibility.Visible;

            IEnumerable<Dictionary<string, object>> list = sedeDAO.BusquedaAproximada(this.sedeText.Text);
            this.sedeList.ItemsSource = list.ColOfObj<Data_sede>();
        }

        private void SedeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.sedeList.Visibility = Visibility.Collapsed;

            if (this.sedeList.SelectedIndex > -1)
            {
                this.sedeText.Text = ((Data_sede)this.sedeList.SelectedItem).nombre;
                this.comisionSearch.sede = ((Data_sede)this.sedeList.SelectedItem).id;
            }
        }


        private void ComisionGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    string key = ((Binding)column.Binding).Path.Path; //column's binding
                    Dictionary<string, object> source = (Dictionary<string, object>)((Data_comision_r)e.Row.DataContext).Dict();
                    string value = (e.EditingElement as TextBox)!.Text;
                    dao.UpdateValueRel("comision", key, value, source);
                }
            }
        }

        private void CargarAlumnos_Click(object sender, RoutedEventArgs e)
        {
            var button = (e.OriginalSource as Button);
            var comision = (Data_comision)button.DataContext;
            AlumnoComision.CargarNuevosAlumnos.Window1 win = new(comision.id);
            win.Show();
        }
    }
}
