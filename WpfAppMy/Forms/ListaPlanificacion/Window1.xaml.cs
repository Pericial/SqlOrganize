using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using Utils;

namespace WpfAppMy.Forms.ListaPlanificacion
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private WpfAppMy.Forms.ListaPlanificacion.DAO.Planificacion planificacionDAO = new();

        public Window1()
        {
            InitializeComponent();
            planificacionGrid.CellEditEnding += PlanificacionGrid_CellEditEnding;
            IEnumerable<Dictionary<string, object>> list = planificacionDAO.All();
            planificacionGrid.ItemsSource = list.ToColOfObj<Planificacion>();
        }


        private void PlanificacionGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    string key = ((Binding)column.Binding).Path.Path; //column's binding
                    Dictionary<string, object> source = (Dictionary<string, object>)((Planificacion)e.Row.DataContext).ToDict();
                    string value = (e.EditingElement as TextBox)!.Text;
                    planificacionDAO.UpdateValueRel(key, value, source);
                }
            }
        }

    }

    internal class Planificacion
    {
        public string _Id { get; set; }
        public string anio { get; set; }
        public string semestre { get; set; }
        public string plan___Id { get; set; }
        public string plan__orientacion { get; set; }
        public string plan__resolucion { get; set; }
        public string plan__distribucion_horaria { get; set; }
        public string pfid { get; set; }




    }
}
