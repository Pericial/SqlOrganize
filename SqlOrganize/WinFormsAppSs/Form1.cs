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
            Db db = ContainerApp.db;
            using DbDataReader reader = db.Query("SUJETOS").Size(100).Execute();
            sujetoBindingSource1.DataSource = reader.ConvertToListOfObject<Sujeto>();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sujetoBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}