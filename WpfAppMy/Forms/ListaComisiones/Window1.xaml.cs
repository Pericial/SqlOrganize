using System;
using System.Collections.Generic;
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

namespace WpfAppMy.Forms.ListaComisiones
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ComisionSearch comisionSearch = new ();

       
        public Window1()
        {
            InitializeComponent();
            DataContext = comisionSearch;
            Search();

            this.autorizadaCombo.SelectedValuePath = "Key";
            this.autorizadaCombo.DisplayMemberPath = "Value";
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool?, string>(null, "(Todos)"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(true, "Sí"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(false, "No"));
        }

        private void Search()
        {
            DAO.Comision comisionDAO = new DAO.Comision();
            List<Dictionary<string, object>> list = comisionDAO.Search(comisionSearch);
            ComisionDataGrid.ItemsSource = list.ConvertToListOfObject<Comision>();
        }


        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
    }
}
