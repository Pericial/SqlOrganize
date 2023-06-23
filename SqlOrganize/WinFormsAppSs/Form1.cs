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
            sujetoBindingSource1.Add(new Sujeto() { APELLIDO = "Castañeda", NOMBRES = "Iván", FECHA_NACIM = new DateTime(1984, 09, 01), NDOC = "31073351" });

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sujetoBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}