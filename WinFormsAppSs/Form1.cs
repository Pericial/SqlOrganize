using SqlOrganize;
using System.Data.Common;
using System.Windows.Forms;
using Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            List<Dictionary<string, object>> dataDtosJudi = ContainerApp.db.Query("DTOSJUDI").
                Size(0).
                ListDict();

            List<Dictionary<string, object>> dataCantidadPersonal = ContainerApp.db.Query("PERSONAL").
                Select("COUNT(*) AS cantidad_personal").
                Group("$DTOJUD").
                ListDict();


            dataDtosJudi.Merge(dataCantidadPersonal, "DTOJUD");

            form1DTOSJUDIBindingSource.DataSource = dataDtosJudi.ConvertToListOfObject<Form1_DTOSJUDI>();
        }


        private void dataGridViewDTOSJUDI_SelectionChanged(object sender, EventArgs e)
        {
            var departamento = (Form1_DTOSJUDI)this.dataGridViewDTOSJUDI.CurrentRow.DataBoundItem;
            if (departamento.Personal.IsNullOrEmpty())
                departamento.Personal = ContainerApp.db.Query("PERSONAL").
                    Where("$DTOJUD = @0").
                    Parameters(departamento.DTOJUD).
                    Size(0).
                    ListObj<Form1_PERSONAL>();
            personalBindingSource.DataSource = departamento.Personal;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewPERSONAL_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewDTOSJUDI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewPERSONAL_CellEnter(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridViewPERSONAL_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            var persistPersonal = ContainerApp.db.Persist("PERSONAL");
            var Personal = (Form1_PERSONAL)this.dataGridViewPERSONAL.CurrentRow.DataBoundItem;
            var CellName = this.dataGridViewPERSONAL.CurrentCell.OwningColumn.DataPropertyName;
            var CellValue = this.dataGridViewPERSONAL.CurrentCell.Value;
            Dictionary<string, object> row = new()
            {
                { CellName, CellValue },
                { "_Id", Personal._Id }
            };

            ContainerApp.db.Persist("PERSONAL").Update(row).Exec();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormLocalidades.Form1 formLocalidades = new FormLocalidades.Form1();
            //this.Hide();
            formLocalidades.ShowDialog();
            //this.Show();
        }
    }
}