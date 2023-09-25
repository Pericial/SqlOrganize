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

namespace WpfAppMy.Windows.AlumnoComision.ActualizarPlanAlumnos
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        WpfAppMy.DAO.AlumnoComision asignacionDAO = new();
        WpfAppMy.DAO.Comision comisionDAO = new();

        List<string> logs = new();
        public Window1()
        {
            InitializeComponent();
        
            var comisiones = comisionDAO.ComisionesAutorizadasPorSemestre("2023", "1");

            var persist = ContainerApp.db.Persist("alumno");
            foreach (var comision in comisiones)
            {
                var idAlumnos = asignacionDAO.IdAlumnosConPlanDiferenteDeComision(comision["id"], comision["planificacion-plan"]);
                if (idAlumnos.IsNullOrEmpty()) continue;
                Dictionary<string, object> row = new() { { "plan", comision["planificacion-plan"] } };
                persist.UpdateIds(row, idAlumnos);
            }
            persist.Transaction().RemoveCache();
        }
    }
}
