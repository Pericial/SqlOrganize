using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

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
            var alumnoComisionData = new Data.AlumnoComision();
            List<Dictionary<string, object>> data = alumnoComisionData.InformeCoordinacionDistrital("1", "2023", 1);
            informeCoordinacionDistritalBindingSource.DataSource = data.ConvertToListOfObject<InformeCoordinacionDistrital>();

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
