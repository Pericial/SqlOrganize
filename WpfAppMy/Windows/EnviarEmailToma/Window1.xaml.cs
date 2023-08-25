using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using static QRCoder.PayloadGenerator;
using QRCoder;
using System.Drawing;
using Utils;

namespace WpfAppMy.Windows.EnviarEmailToma
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        DAO dao = new();
        public Window1()
        {
            InitializeComponent();
            List<Dictionary<string, object>> list = dao.TomaAll();
            foreach (Dictionary<string, object> item in list)
            {
                List<string> comisiones = new() { "10086"};
                Toma toma = item.ConvertToObject<Toma>();
                if (comisiones.Contains(toma.comision__pfid)) {
                    if (toma.docente__email_abc.IsNullOrEmpty())
                    {
                        info.Text += $@"El email de la docente no esta definido en: {toma.comision__pfid} {toma.asignatura__nombre}
";
                        continue;
                    }
                    Email email = new Email(toma);
                    email.Send();
                    info.Text += email.Subject + @"
";
                }
                /*info.Text += email.To + @"
";
                info.Text += email.Attachment + @"
";
                
                info.Text += email.Body + @"



"; */
            }

            

        }
    }

    internal class Toma
    {
        public string id { get; set; }

        public int curso__horas_catedra { get; set; }

        public string curso__descripcion_horario { get; set; }

        public string docente__nombres { get; set; }
        public string docente__apellidos { get; set; }
        public string docente__cuil { get; set; }
        public string docente__numero_documento { get; set; }
        public DateTime docente__fecha_nacimiento { get; set; }
        public string docente__email { get; set; }
        public string docente__email_abc { get; set; }

        public string docente__descripcion_domicilio { get; set; }
        
        public string sede__nombre { get; set; }
        public string sede__numero { get; set; }
        public string domicilio__calle { get; set; }
        public string domicilio__numero { get; set; }
        public string domicilio__entre { get; set; }
        public string domicilio__localidad { get; set; }
        public string domicilio__barrio { get; set; }

        public string asignatura__nombre { get; set; }
        public string asignatura__codigo { get; set; }


        public string comision__pfid { get; set; }
        public string comision__division { get; set; }
        public string planificacion__anio { get; set; }
        public string planificacion__semestre { get; set; }

    }
}
