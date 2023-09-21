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

namespace WpfAppMy.Windows.Curso.ListaCursoSemestreSinTomasAprobadas
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DAO.Curso cursoDAO = new();
        DAO.Toma tomaDAO = new();

        private ObservableCollection<Model> cursoData = new();



        public Window1()
        {
            InitializeComponent();
            cursoGrid.ItemsSource = cursoData;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            IEnumerable<object> idCursosConTomasAprobadas = tomaDAO.IdCursosConTomasAprobadasSemestre("2023", "2");

            IEnumerable<Dictionary<string, object>> cursosAutorizadosSemestre = cursoDAO.CursosAutorizadosSemestre("2023", "2");

            List<Dictionary<string, object>> cursosSinTomasAprobadasSemestre = new();
            foreach (var curso in cursosAutorizadosSemestre)
            {
                if (!idCursosConTomasAprobadas.Contains(curso["id"]))
                    cursosSinTomasAprobadasSemestre.Add(curso);
            }

            cursoData.Clear();
            cursoData.AddRange(cursosSinTomasAprobadasSemestre.ToColOfObj<Model>());
        }
    }

    internal class Model : INotifyPropertyChanged
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



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
