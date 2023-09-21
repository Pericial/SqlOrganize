using QRCoder;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
using WpfAppMy.Windows.TomaPosesionPdf;
using Utils;
using WpfAppMy.Windows.ListaTomas;

namespace WpfAppMy.Windows.TomaPosesionPdf
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        Search search = new();
        DAO dao = new();
        QRCodeGenerator qrGenerator = new QRCodeGenerator();

        public Window1()
        {
            InitializeComponent();
            IEnumerable<Dictionary<string, object>> list = dao.TomaAll(search);
            foreach(Dictionary<string, object> item in list)
            {
                Toma toma = item.Obj<Toma>();
                if (!toma.comision__pfid.Equals("10078") && !toma.comision__pfid.Equals("10089")) continue;
                QRCodeData qrCodeData = qrGenerator.CreateQrCode("https://planfines2.com.ar/validar-toma/" + toma.id, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                ImageConverter converter = new ImageConverter();
                toma.qr_code = (byte[])converter.ConvertTo(qrCodeImage, typeof(byte[]));
                Document document = new(toma);
                document.GeneratePdf("C:\\Users\\ivan\\Downloads\\" + toma.comision__pfid + "_" + toma.asignatura__codigo + "_" + toma.docente__numero_documento + ".pdf");
            
            }
        }


    }



    internal class Search
    {
        public string calendario__anio { get; set; } = DateTime.Now.Year.ToString();
        public int calendario__semestre { get; set; } = DateTime.Now.ToSemester();
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

        public string domicilio__calle { get; set; }
        public string domicilio__numero { get; set; }
        public string domicilio__entre { get; set; }
        public string domicilio__localidad { get; set; }
        public string domicilio__barrio { get; set; }

        public string asignatura__nombre { get; set; }
        public string asignatura__codigo { get; set; }


        public string comision__pfid { get; set; }

        public Byte[] qr_code { get; set; }
    }
}
