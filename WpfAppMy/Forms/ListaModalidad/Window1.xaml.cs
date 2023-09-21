using MySqlX.XDevAPI.Relational;
using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<Modalidad> modalidadData = new();

        public Window1()
        {
            InitializeComponent();
            modalidadGrid.ItemsSource = modalidadData;
            this.Loaded += MainWindow_Loaded;
            modalidadGrid.CellEditEnding += ModalidadGrid_CellEditEnding;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ModalidadGridData();
        }

        private void ModalidadGridData()
        {
            IEnumerable<Dictionary<string, object>> list = dao.AllModalidad();
            modalidadData.Clear();
            modalidadData.AddRange(list.ToListOfObj<Modalidad>());
        }
        private void ModalidadGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    string key = ((Binding)column.Binding).Path.Path; //column's binding
                    string value = (e.EditingElement as TextBox)!.Text;
                    Dictionary<string, object> source = (Dictionary<string, object>)((Modalidad)e.Row.DataContext).ToDict();
                    string? fieldId = null;
                    string entityName = "modalidad";
                    string fieldName = key;
                    string? parentId = null;

                    if (key.Contains(ContainerApp.db.config.idAttrSeparatorString))
                    {
                        int indexSeparator = key.IndexOf(ContainerApp.db.config.idAttrSeparatorString);
                        fieldId = key.Substring(0, indexSeparator);
                        entityName = ContainerApp.db.Entity(entityName!).relations[fieldId].refEntityName;
                        fieldName = key.Substring(indexSeparator + ContainerApp.db.config.idAttrSeparatorString.Length);
                    }

                    do
                    {
                        EntityValues v = ContainerApp.db.Values(entityName, fieldId).Set(source).Set(fieldName, value);
                        IDictionary<string, object>? row;

                        //en caso de que el campo editado sea unico, se consultan sus valores
                        if (ContainerApp.db.Field(entityName, fieldName).IsUnique())
                            row = dao.RowByEntityFieldValue(entityName, fieldName, value);
                        else
                            row = dao.RowByEntityUnique(entityName, v.values);

                        if (!row.IsNullOrEmpty())
                        {
                            v = ContainerApp.db.Values(entityName).Set(row!);
                            v.fieldId = fieldId;
                        }
                        else
                        {
                            if (v.Get("_Id").IsNullOrEmpty() && v.Check())
                            {
                                v.Default().Reset();
                                EntityPersist p = ContainerApp.db.Persist(entityName).Insert(v.values).Exec();
                                ((Modalidad)e.Row.Item)._Id = (string)v.values["_Id"];
                            }
                            else
                            {
                                ContainerApp.db.Persist(entityName).Update(v.values).Exec();
                            }
                        }

                        Dictionary<string, object> r = v.Get();

                        if (fieldId != null) { 
                            parentId = ContainerApp.db.Entity(entityName).relations[fieldId].parentId;
                            if (parentId != null) { 
                                var parentFieldName = ContainerApp.db.Entity(entityName).relations[parentId].fieldName;
                                r[parentId + ContainerApp.db.config.idNameSeparatorString + parentFieldName] = r[fieldId + ContainerApp.db.config.idNameSeparatorString + fieldName];
                                fieldId = parentId;
                                fieldName = parentFieldName;
                            }
                        } else
                        {
                            fieldId = null;
                        }

                        (e.Row.Item as Modalidad).CopyNotNullValues(v.Get().ToObj<Modalidad>());
                    }
                    while ((fieldId != null) && (parentId != null));
                }
            }
        }
    }

    internal class Modalidad : INotifyPropertyChanged
    {
        private string __Id;
        public string _Id
        {
            get { return __Id; }
            set { __Id = value; NotifyPropertyChanged(); }
        }

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; NotifyPropertyChanged(); }
        }

        private string _pfid;

        public string pfid
        {
            get { return _pfid; }
            set { _pfid = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
