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
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CursoSearch();
        }

        private void CursoSearch()
        {
            List<Dictionary<string, object>> list = dao.CursoAll(search);
            cursoData.Clear();
            cursoData.AddRange(list.ConvertToListOfObject<Curso>());
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            CursoSearch();
        }

       
    }

    internal class Search
    {
        public string calendario__anio { get; set; } = DateTime.Now.Year.ToString();
        public int calendario__semestre { get; set; } = DateTime.Now.ToSemester();
    }


    internal class Curso : INotifyPropertyChanged
    {
        private string __Id;
        public string _Id
        {
            get { return __Id; }
            set { __Id = value; NotifyPropertyChanged(); }
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

        private string _asignatura___Id;

        public string asignatura___Id
        {
            get { return _asignatura___Id; }
            set { _asignatura___Id = value; NotifyPropertyChanged(); }
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

        private string _comision___Id;

        public string comision___Id
        {
            get { return _comision___Id; }
            set { _comision___Id = value; NotifyPropertyChanged(); }
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
