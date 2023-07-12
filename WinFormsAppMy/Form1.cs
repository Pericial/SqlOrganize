namespace WinFormsAppMy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WinFormsAppMy.Forms.InformeCoordinacionDistrital.Form1 form = new();
            form.Show();
        }
    }
}