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

namespace WpfAppMy.Forms.ListaReferentesSemestre
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Search search = new();

        private WpfAppMy.Forms.ListaReferentesSemestre.DAO.Designacion designacionDAO = new();

        public Window1()
        {
            InitializeComponent();

            DataContext = search;

            this.autorizadaCombo.SelectedValuePath = "Key";
            this.autorizadaCombo.DisplayMemberPath = "Value";
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool?, string>(null, "(Todos)"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(true, "Sí"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(false, "No"));

            referenteGrid.CellEditEnding += ReferenteGrid_CellEditEnding;

            Search();
        }

        private void Search()
        {
            IEnumerable<Dictionary<string, object>> list = designacionDAO.referentesSemestre(search);
            referenteGrid.ItemsSource = list.ToColOfObj<Designacion>();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void ReferenteGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    string key = ((Binding)column.Binding).Path.Path; //column's binding
                    Dictionary<string, object> source = (Dictionary<string, object>)((Designacion)e.Row.DataContext).ToDict();
                    string value = (e.EditingElement as TextBox)!.Text;
                    designacionDAO.UpdateValueRel(key, value, source);
                }
            }
        }
    }

    internal class Search
    {
        public string calendario__anio { get; set; } = DateTime.Now.Year.ToString();
        public int calendario__semestre { get; set; } = DateTime.Now.ToSemester();
        public bool? autorizada { get; set; } = true;
    }


    internal class Designacion
    {
        public string _Id { get; set; }
        public string pfid { get; set; }

        public string sede__pfid_organizacion { get; set; }

        public string sede__numero { get; set; }
        public string sede__nombre { get; set; }

        public string persona__nombres { get; set; }
        public string persona__apellidos { get; set; }
        public string persona__telefono { get; set; }
        public string persona__email { get; set; }


       

    }
}
