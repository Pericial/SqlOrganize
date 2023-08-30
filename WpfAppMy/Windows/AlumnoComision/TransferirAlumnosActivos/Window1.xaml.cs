using Newtonsoft.Json;
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
using WpfAppMy.Windows.ProcesarDocentesProgramaFines;

namespace WpfAppMy.Windows.AlumnoComision.TransferirAlumnosActivos
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
            var search = new Dictionary<string, object>() {
                { "calendario-anio", "2023" },
                { "calendario-semestre", "1" },
            };

            var alumnos = dao.AlumnoAllByCalendario(search);
            var comisiones = dao.ComisionConSiguienteAllByCalendario(search);


            foreach (var comision in comisiones)
            {





            }


        }
    }
}
