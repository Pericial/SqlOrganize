using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Utils;

namespace WpfAppMy.Forms.ListaComisiones
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ComisionSearch comisionSearch = new ();

        private List<SedeItem> sedeSuggestionList = new ();

        private DAO.Sede sedeDAO = new ();
        private DAO.Comision comisionDAO = new();

        public Window1()
        {
            InitializeComponent();
            this.sedeList.Visibility = Visibility.Collapsed; //al iniciar que no se vea la lista de opciones (estara vacia)

            //DataContext = comisionSearch;

            this.autorizadaCombo.SelectedValuePath = "Key";
            this.autorizadaCombo.DisplayMemberPath = "Value";
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool?, string>(null, "(Todos)"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(true, "Sí"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(false, "No"));
            
            this.calendarioAnioText.Text = comisionSearch.calendario__anio;
            this.calendarioSemestreText.Text = comisionSearch.calendario__semestre.ToString();
            this.autorizadaCombo.SelectedValue = comisionSearch.autorizada;


            Search();

        }

        private void Search()
        {
            var cs = new ComisionSearch();
            cs.calendario__anio = calendarioAnioText.Text;
            cs.calendario__semestre = Int32.Parse(calendarioSemestreText.Text);
            cs.autorizada = (bool)autorizadaCombo.SelectedValue;
            List<Dictionary<string, object>> list = comisionDAO.Search(cs);
            ComisionDataGrid.ItemsSource = list.ConvertToListOfObject<Comision>();
        }


        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void SedeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.sedeList.SelectedIndex > -1)
                if (this.sedeText.Text.Equals(((SedeItem)this.sedeList.SelectedItem).nombre))
                    return;
                else 
                    this.sedeList.SelectedIndex = -1;

            if (string.IsNullOrEmpty(this.sedeText.Text) || this.sedeText.Text.Length < 3)
            {
                this.sedeList.Visibility = Visibility.Collapsed;
                return;
            }

            this.sedeList.Visibility = Visibility.Visible;

            List<Dictionary<string, object>> list = sedeDAO.Search(this.sedeText.Text);
            this.sedeList.ItemsSource = list.ConvertToListOfObject<SedeItem>();
        }

        private void SedeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                this.sedeList.Visibility = Visibility.Collapsed;

                if (this.sedeList.SelectedIndex > -1)
                    this.sedeText.Text = ((SedeItem)this.sedeList.SelectedItem).nombre;
        }
    }

    public class SedeItem
    {
        public string id { get; set; }
        public string numero { get; set; }
        public string nombre { get; set; }
    }

}
