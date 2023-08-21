using QRCoder;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
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

namespace WpfAppMy.Windows.TomaPosesionPdf
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


    }
    public class TomaPosesionData
    {
        public Byte[] qrCode { get; set; }
    }
}
