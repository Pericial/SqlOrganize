using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppMy.Forms.InformeCoordinacionDistrital
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            List<Dictionary<string, object>> data = WinFormsAppMy.Controllers.AlumnoComision.InformeCoordinacionDistrital.GenerarInforme("2022", 2);
            informeCoordinacionDistritalBindingSource.Add(new InformeCoordinacionDistrital());
            informeCoordinacionDistritalBindingSource.Add(new InformeCoordinacionDistrital());
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
