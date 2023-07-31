using SqlOrganize;
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

        private DAO dao = new();

        public Window1()
        {
            InitializeComponent();
            modalidadGrid.CellEditEnding += ModalidadGrid_CellEditEnding;
            List<Dictionary<string, object>> list = dao.AllModalidad();
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
                    dao.UpdateValueRelModalidad(key, value, source);
                }
            }
        }

        private void ModalidadGrid_CellEditEnding2(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    string key = ((Binding)column.Binding).Path.Path; //column's binding
                    Dictionary<string, object> source = (Dictionary<string, object>)((Modalidad)e.Row.DataContext).ConvertToDict();
                    string? fieldId = null;
                    if (key.Contains(ContainerApp.db.config.idAttrSeparatorString))
                    {
                        int indexSeparator = key.IndexOf(ContainerApp.db.config.idAttrSeparatorString);
                        fieldId = key.Substring(0, indexSeparator);
                    }
                    EntityValues v = ContainerApp.db.Values("modalidad", fieldId).Set(source);
                    if (v.Get("_Id").IsNullOrEmpty())
                    {
                        if (!v.Remove("_Id").Check())
                        {

                        }
                    }


                    string value = (e.EditingElement as TextBox)!.Text;
                    dao.UpdateValueRelModalidad(key, value, source);
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
