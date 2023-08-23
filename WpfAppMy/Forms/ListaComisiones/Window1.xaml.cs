using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Utils;

namespace WpfAppMy.Forms.ListaComisiones
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ComisionSearch comisionSearch = new ();

        private DAO.Sede sedeDAO = new ();
        private DAO.Comision comisionDAO = new();

        public Window1()
        {
            InitializeComponent();
            this.sedeList.Visibility = Visibility.Collapsed; //al iniciar que no se vea la lista de opciones (estara vacia)
            comisionGrid.CellEditEnding += ComisionGrid_CellEditEnding!;

            DataContext = comisionSearch;

            this.autorizadaCombo.SelectedValuePath = "Key";
            this.autorizadaCombo.DisplayMemberPath = "Value";
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool?, string>(null, "(Todos)"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(true, "Sí"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(false, "No"));
            
            ComisionSearch();
        }

        private void ComisionSearch()
        {
            List<Dictionary<string, object>> list = comisionDAO.Search(comisionSearch);
            comisionGrid.ItemsSource = list.ConvertToListOfObject<Comision>();
        }


        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            ComisionSearch();
        }

        private void SedeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.sedeList.SelectedIndex > -1)
                if (this.sedeText.Text.Equals(((SedeItem)this.sedeList.SelectedItem).nombre))
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

            List<Dictionary<string, object>> list = sedeDAO.Search(this.sedeText.Text);
            this.sedeList.ItemsSource = list.ConvertToListOfObject<SedeItem>();
        }

        private void SedeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                this.sedeList.Visibility = Visibility.Collapsed;

            if (this.sedeList.SelectedIndex > -1)
            {
                this.sedeText.Text = ((SedeItem)this.sedeList.SelectedItem).nombre;
                this.comisionSearch.sede = ((SedeItem)this.sedeList.SelectedItem).id;
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
                    Dictionary<string, object> source = (Dictionary<string, object>)((Comision)e.Row.DataContext).ConvertToDict();
                    string value = (e.EditingElement as TextBox)!.Text;
                    comisionDAO.UpdateValueRel(key, value, source);
                }
            }
        }
    }

    internal class ComisionSearch
    {
        public string calendario__anio { get; set; } = DateTime.Now.Year.ToString();
        public int calendario__semestre { get; set; } = DateTime.Now.ToSemester();
        public bool? autorizada { get; set; } = true;
        public string? sede { get; set; }
    }

    internal class SedeItem
    {
        public string id { get; set; }
        public string numero { get; set; }
        public string nombre { get; set; }
    }

    internal class Comision
    {
        public string _Id { get; set; }

        public string numero { get; set; }

        public string sede__nombre { get; set; }

        public string sede__pfid { get; set; }

        public string identificacion { get; set; }

        public string pfid { get; set; }

        public string planificacion__anio { get; set; }
        public string planificacion__semestre { get; set; }
        public string planificacion__orientacion { get; set; }
        public string planificacion__pfid { get; set; }

        public string domicilio__calle { get; set; }
        public string domicilio__numero { get; set; }
        public string domicilio__entre { get; set; }
        public string domicilio__barrio { get; set; }
        public string domicilio__localidad { get; set; }



    }
}
