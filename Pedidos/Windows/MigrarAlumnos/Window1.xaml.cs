using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
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

namespace Pedidos.Windows.MigrarAlumnos
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DAO dao = new();
        List<string> logs = new();


        public Window1()
        {
            InitializeComponent();

            var alumnos = dao.AlumnosAll();
            logs.Add("Cantidad de docentes a procesar" + alumnos.Count.ToString());

            info.Text += String.Join(@"
", logs);
        }
    }
}
