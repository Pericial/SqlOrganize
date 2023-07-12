namespace WinFormsAppSs.FormLocalidades
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var q = ContainerApp.db.
                Query("LOCALIDADES").
                Size(0);

            var data = ContainerApp.queryCache.ListObj<Localidades>(q);

            localidadesBindingSource.DataSource = data;
        }
    }
}
