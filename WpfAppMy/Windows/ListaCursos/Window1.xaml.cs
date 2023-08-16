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

        private string _horas_catedra;
        public string horas_catedra
        {
            get { return _horas_catedra; }
            set { _horas_catedra = value; NotifyPropertyChanged(); }
        }

        private string _ige;

        public string ige
        {
            get { return _ige; }
            set { _ige = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
