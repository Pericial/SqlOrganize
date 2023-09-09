using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
using Utils;
using System.Diagnostics;
using WpfUtils;

namespace WpfAppMy.Windows.ListaTomas
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        Search search = new();
        DAO dao = new();
        private ObservableCollection<Toma> tomaData = new();

        public Window1()
        {
            InitializeComponent();
            DataContext = search;
            tomaGrid.ItemsSource = tomaData;
            this.Loaded += MainWindow_Loaded;
            tomaGrid.CellEditEnding += TomaGrid_CellEditEnding!;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            List<Dictionary<string, object>> list = dao.TomaAll(search);
            tomaData.Clear();
            tomaData.AddRange(list.ConvertToListOfObject<Toma>());
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void TomaGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    List<string> ignore = new List<string>() { "confirmada" };
                    string key = ((Binding)column.Binding).Path.Path; //column's binding
                    if (ignore.Contains(key)) return;
                    object value = (e.EditingElement as TextBox)!.Text;
                    Dictionary<string, object> source = (Dictionary<string, object>)((Toma)e.Row.DataContext).ConvertToDict();
                    string? fieldId = null;
                    string mainEntityName = "toma", entityName = "toma", fieldName = key;

                    if (key.Contains(ContainerApp.db.config.idAttrSeparatorString))
                        (fieldId, fieldName, entityName) = ContainerApp.db.KeyDeconstruction(entityName, key);

                    bool continueWhile;
                    bool reload = false;
                    do
                    {
                        continueWhile = (fieldId == null) ? false : true;
                        EntityValues v = ContainerApp.db.Values(entityName, fieldId).Set(source);
                        if (!v.values[fieldName].IsNullOrEmpty() && v.values[fieldName].Equals(value))
                        {
                            if (reload)
                                LoadData(); //debe recargarse para visualizar los cambios realizados en otras iteraciones.
                            break;
                        }

                        v.Sset(fieldName, value);
                        Dictionary<string, object>? row;

                        row = dao.RowByUniqueFieldOrUniqueValues(fieldName, v);

                        if (!row.IsNullOrEmpty())
                        {
                            v = ContainerApp.db.Values(entityName).Set(row!);
                            v.fieldId = fieldId;
                        }
                        else
                        {
                            if (!v.Check())
                            {
                                (e.Row.Item as Toma).CopyNotNullValues(v.Get().ConvertToObject<Toma>());
                                break;
                            }

                            dao.Persist(v);
                        }

                        (e.Row.Item as Toma).CopyNotNullValues(v.Get().ConvertToObject<Toma>());

                        if (fieldId != null)
                        {
                            string? parentId = ContainerApp.db.Entity(mainEntityName).relations[fieldId].parentId;
                            if (parentId != null)
                            {
                                var parentFieldName = ContainerApp.db.Entity(mainEntityName).relations[fieldId].fieldName;
                                value = v.Get()[fieldId + ContainerApp.db.config.idNameSeparatorString + ContainerApp.db.Entity(mainEntityName).relations[fieldId].refFieldName];
                                fieldId = parentId;
                                fieldName = parentFieldName;
                                entityName = ContainerApp.db.Entity(mainEntityName).relations[parentId].refEntityName;

                            }
                            else
                            {
                                entityName = mainEntityName;
                                value = v.Get()[fieldId + ContainerApp.db.config.idNameSeparatorString + ContainerApp.db.Entity(mainEntityName).relations[fieldId].refFieldName];
                                fieldName = ContainerApp.db.Entity(mainEntityName).relations[fieldId].fieldName;
                                fieldId = null;
                            }
                        }
                        reload = true;
                    }
                    while (continueWhile);
                }
            }
        }


        /// <summary>
        /// Actualizar checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TomaGrid_CellCheckBoxClick(object sender, RoutedEventArgs e)
        {
            var cell = (sender as DataGridCell);
            var column = cell.Column as DataGridBoundColumn;
            var value = (cell.Content as CheckBox).IsChecked;
            if (column != null && cell.DataContext is Toma)
            {
                string key = ((Binding)column.Binding).Path.Path; //column's binding.
                IDictionary<string, object> source = (cell.DataContext as Toma).ConvertToDict(); 
                if((bool)source[key] != value)
                {
                    string? fieldId = null;
                    string entityName = "toma", fieldName = key;
                    if (key.Contains(ContainerApp.db.config.idAttrSeparatorString))
                        (fieldId, fieldName, entityName) = ContainerApp.db.KeyDeconstruction(entityName, key);

                    EntityValues v = ContainerApp.db.Values(entityName, fieldId).Set(source);
                    v.Sset(fieldName, value);

                    if (v.Check())
                        dao.Persist(v);

                    DataGridRow row = DataGridRow.GetRowContainingElement(cell);
                    (row.Item as Toma).CopyNotNullValues(v.Get().ConvertToObject<Toma>());

                    if(!fieldId.IsNullOrEmpty())
                        LoadData(); //debe recargarse para visualizar los cambios realizados en otras iteraciones
                }
            }
        }
    }

    internal class Search
    {
        public string calendario__anio { get; set; } = DateTime.Now.Year.ToString();
        public int calendario__semestre { get; set; } = DateTime.Now.ToSemester();
    }


    internal class Toma : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }

        private DateTime _fecha_toma;
        public DateTime fecha_toma
        {
            get { return _fecha_toma; }
            set { _fecha_toma = value; NotifyPropertyChanged(); }
        }

        private string _estado;
        public string estado
        {
            get { return _estado; }
            set { _estado = value; NotifyPropertyChanged(); }
        }

        private string _estado_contralor;

        public string estado_contralor
        {
            get { return _estado_contralor; }
            set { _estado_contralor = value; NotifyPropertyChanged(); }
        }

        private string _tipo_movimiento;

        public string tipo_movimiento
        {
            get { return _tipo_movimiento; }
            set { _tipo_movimiento = value; NotifyPropertyChanged(); }
        }


        private string _docente__id;

        public string docente__id
        {
            get { return _docente__id; }
            set { _docente__id = value; NotifyPropertyChanged(); }
        }

        private string _docente__nombres;

        public string docente__nombres
        {
            get { return _docente__nombres; }
            set { _docente__nombres = value; NotifyPropertyChanged(); }
        }

        private string _docente__apellidos;

        public string docente__apellidos
        {
            get { return _docente__apellidos; }
            set { _docente__apellidos = value; NotifyPropertyChanged(); }
        }

        private string _docente__numero_documento;

        public string docente__numero_documento
        {
            get { return _docente__numero_documento; }
            set { _docente__numero_documento = value; NotifyPropertyChanged(); }
        }

        private string _docente__email_abc;

        public string docente__email_abc
        {
            get { return _docente__email_abc; }
            set { _docente__email_abc = value; NotifyPropertyChanged(); }
        }

        private string _docente;

        public string docente
        {
            get { return _docente; }
            set { _docente = value; NotifyPropertyChanged(); }
        }

        private bool _confirmada;

        public bool confirmada
        {
            get { return _confirmada; }
            set { _confirmada = value; NotifyPropertyChanged(); }
        }

        public string _observaciones;

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }

        public string comision__pfid { get; set; }
        public string asignatura__nombre { get; set; }
        public string asignatura__codigo { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}
