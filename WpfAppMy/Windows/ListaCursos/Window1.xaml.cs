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
using WpfAppMy.Forms.ListaModalidad;

namespace WpfAppMy.Windows.ListaCursos
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        Search search = new();
        DAO dao = new();
        private ObservableCollection<Curso> cursoData = new();

        public Window1()
        {
            InitializeComponent();
            DataContext = search;
            cursoGrid.ItemsSource = cursoData;
            this.Loaded += MainWindow_Loaded;
            cursoGrid.CellEditEnding += CursoGrid_CellEditEnding!;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            IEnumerable<Dictionary<string, object>> list = dao.CursoAll(search);
            cursoData.Clear();
            cursoData.AddRange(list.ColOfObj<Curso>());
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void CursoGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {

                    string key = ((Binding)column.Binding).Path.Path; //column's binding
                    object value = (e.EditingElement as TextBox)!.Text;
                    Dictionary<string, object> source = (Dictionary<string, object>)((Curso)e.Row.DataContext).Dict();
                    string? fieldId = null;
                    string mainEntityName = "curso";
                    string entityName = "curso";
                    string fieldName = key;

                    if (key.Contains(ContainerApp.db.config.idAttrSeparatorString))
                    {
                        int i = key.IndexOf(ContainerApp.db.config.idAttrSeparatorString);
                        fieldId = key.Substring(0, i);
                        entityName = ContainerApp.db.Entity(entityName!).relations[fieldId].refEntityName;
                        fieldName = key.Substring(i + ContainerApp.db.config.idAttrSeparatorString.Length);
                    }

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
                        IDictionary<string, object> ? row;

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
                            if (!v.Check())
                            {
                                (e.Row.Item as Curso).CopyNotNullValues(v.Get().Obj<Curso>());
                                break;
                            }

                            if (v.Get(ContainerApp.config.id).IsNullOrEmpty())
                            {
                                v.Default().Reset();
                                var p = ContainerApp.db.Persist(entityName).Insert(v.values).Exec().RemoveCache();
                            }
                            else
                            {
                                v.Reset();
                                var p = ContainerApp.db.Persist(entityName).Update(v.values).Exec().RemoveCache();
                            }
                        }

                        (e.Row.Item as Curso).CopyNotNullValues(v.Get().Obj<Curso>());

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


    }

    internal class Search
    {
        public string calendario__anio { get; set; } = DateTime.Now.Year.ToString();
        public int calendario__semestre { get; set; } = DateTime.Now.ToSemester();
    }


    internal class Curso : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }

        private int _horas_catedra;
        public int horas_catedra
        {
            get { return _horas_catedra; }
            set { _horas_catedra = value; NotifyPropertyChanged(); }
        }

        private string _asignatura;

        public string asignatura
        {
            get { return _asignatura; }
            set { _asignatura = value; NotifyPropertyChanged(); }
        }

        private string _asignatura__id;

        public string asignatura__id
        {
            get { return _asignatura__id; }
            set { _asignatura__id = value; NotifyPropertyChanged(); }
        }

        private string _asignatura__nombre;

        public string asignatura__nombre
        {
            get { return _asignatura__nombre; }
            set { _asignatura__nombre = value; NotifyPropertyChanged(); }
        }

        private string _asignatura__codigo;

        public string asignatura__codigo
        {
            get { return _asignatura__codigo; }
            set { _asignatura__codigo = value; NotifyPropertyChanged(); }
        }

        private string _comision;

        public string comision
        {
            get { return _comision; }
            set { _comision = value; NotifyPropertyChanged(); }
        }

        private string _comision__id;

        public string comision__id
        {
            get { return _comision__id; }
            set { _comision__id = value; NotifyPropertyChanged(); }
        }

        private string _comision__division;

        public string comision__division
        {
            get { return _comision__division; }
            set { _comision__division = value; NotifyPropertyChanged(); }
        }

        private string _comision__pfid;

        public string comision__pfid
        {
            get { return _comision__pfid; }
            set { _comision__pfid = value; NotifyPropertyChanged(); }
        }

        private string _comision__identificacion;

        public string comision__identificacion
        {
            get { return _comision__identificacion; }
            set { _comision__identificacion = value; NotifyPropertyChanged(); }
        }


        private string _planificacion__anio;

        public string planificacion__anio
        {
            get { return _planificacion__anio; }
            set { _planificacion__anio = value; NotifyPropertyChanged(); }
        }

        private string _planificacion__semestre;

        public string planificacion__semestre
        {
            get { return _planificacion__semestre; }
            set { _planificacion__semestre = value; NotifyPropertyChanged(); }
        }

        private string _sede__numero;

        public string sede__numero
        {
            get { return _sede__numero; }
            set { _sede__numero = value; NotifyPropertyChanged(); }
        }

        private string _sede__nombre;

        public string sede__nombre
        {
            get { return _sede__nombre; }
            set { _sede__nombre = value; NotifyPropertyChanged(); }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
