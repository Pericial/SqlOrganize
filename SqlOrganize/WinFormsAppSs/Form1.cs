using SqlOrganize;
using System.Data.Common;
using Utils;

namespace WinFormsAppSs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            EntityQuery q = ContainerApp.db.Query("SUJETOS").
                Size(100).
                Where("$FECHA_NACIM IS NOT NULL");
            sujetoBindingSource1.DataSource = ContainerApp.queryCache.ListObj<Sujeto>(q);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sujetoBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}