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

namespace WpfAppMy.Forms.ListaModalidad
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private DAO.Modalidad modalidadDAO = new();

        public Window1()
        {
            InitializeComponent();
            modalidadGrid.CellEditEnding += ModalidadGrid_CellEditEnding;
            List<Dictionary<string, object>> list = modalidadDAO.All();
            modalidadGrid.ItemsSource = list.ConvertToListOfObject<Modalidad>();
        }

        private void ModalidadGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    string key = ((Binding)column.Binding).Path.Path; //column's binding
                    Dictionary<string, object> source = (Dictionary<string, object>)((Modalidad)e.Row.DataContext).ConvertToDict();
                    string value = (e.EditingElement as TextBox)!.Text;
                    modalidadDAO.UpdateValueRel(key, value, source);
                }
            }
        }

    }

    internal class Modalidad
    {
        public string _Id { get; set; }

        public string nombre { get; set; }

        public string pfid { get; set; }




    }
}
