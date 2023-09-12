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

namespace WpfAppMy.Forms.InformeCoordinacionDistrital
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var alumnoComisionData = new DAO.AlumnoComision();
            List<Dictionary<string, object>> data = alumnoComisionData.InformeCoordinacionDistrital("1", "2023", 1);
            InformeCoordinacionDistritalDataGrid.ItemsSource = data.ToListOfObj<InformeCoordinacionDistrital>();
        }

    }
}
