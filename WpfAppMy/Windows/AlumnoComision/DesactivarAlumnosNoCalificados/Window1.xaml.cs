using Org.BouncyCastle.Crypto;
using SqlOrganize;
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

namespace WpfAppMy.Windows.AlumnoComision.DesactivarAlumnosNoCalificados
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        WpfAppMy.DAO.AlumnoComision asignacionDAO = new();
        WpfAppMy.DAO.Calificacion calificacionDAO = new();


        public Window1()
        {
            InitializeComponent();

            var alumnosComisiones = asignacionDAO.AsignacionesActivasDeComisionesAutorizadasPorSemestre("2023", "1");
            List<AlumnoComision> data = new();
            List<object> ids = new();
            foreach (var alumnoComision in alumnosComisiones)
            {
                var q = calificacionDAO.CantidadCalificacionesAprobadasDeAlumnoPorTramo(alumnoComision["alumno"], alumnoComision["planificacion-anio"], alumnoComision["planificacion-semestre"]);
                if (q < 3)
                {
                    ids.Add(alumnoComision["id"]);
                    var a = alumnoComision.ToObject<AlumnoComision>();
                    data.Add(a);
                }
            }
            if(ids.Count > 0) { 
                alumnoComisionGrid.ItemsSource = data;
                var p = ContainerApp.db.Persist("alumno_comision").UpdateValue("estado", "No activo", ids).Exec();
                ContainerApp.dbCache.Remove(p.detail);
            }


        }
    }


    internal class AlumnoComision { 
        public string id { get; set; }
        public string persona__nombres { get; set; }
        public string persona__apellidos { get; set; }
        public string persona__numero_documento { get; set; }
        public string comision__division { get; set; }
        public string sede__numero { get; set; }
        public string planificacion__anio { get; set; }
        public string planificacion__semestre { get; set; }
    }
}
