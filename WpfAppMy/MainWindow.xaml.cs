using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utils;
using WpfAppMy.DAO;
using WpfAppMy.Forms;

namespace WpfAppMy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //To check the paging direction according to use selection.
        private int sedeCount = 0; //cantidad total de elementos
        protected int sedePage = 1; //pagina a mostrar
        private DAO.Sede sedeDAO = new (); //objeto de acceso a datos
        private ObservableCollection<Sede> sedeData = new ();

    
        public MainWindow()
        {
            InitializeComponent();

            sizeCombo.Items.Add(10);
            sizeCombo.Items.Add(20);
            sizeCombo.Items.Add(30);
            sizeCombo.Items.Add(50);
            sizeCombo.Items.Add(100);
            sizeCombo.Items.Add(200);
            sizeCombo.SelectedItem = 10;
            sedeGrid.ItemsSource = sedeData;
            this.Loaded += MainWindow_Loaded;
            
        }

        private void SedeGridData()
        {
            List<Dictionary<string, object>> sedeList = sedeDAO.ConsultaPaginacion(sedePage, (int)sizeCombo.SelectedItem);
            sedeData.Clear();
            sedeData.AddRange(sedeList.ConvertToObservableCollectionOfObject<Sede>());
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            sedeCount = sedeDAO.CantidadTotal();
            DisablePaginationButtons();
            SedeGridData();
            ChangeLabelContent();
            EnablePaginationButtons();
        }   

        private void ChangeLabelContent()
        {
            int count = sedePage * (int)sizeCombo.SelectedItem;
            if(count > sedeCount)
                count = sedeCount;
            lblpageInformation.Content = count + " of " + sedeCount;
        }

        private void DisablePaginationButtons()
        {
            btnPrev.IsEnabled = false;
            btnFirst.IsEnabled = false;
            btnNext.IsEnabled = false;
            btnLast.IsEnabled = false;
        }

        private void EnablePaginationButtons()
        {
            if (sedeCount > (sedePage * (int)sizeCombo.SelectedItem))
            {
                btnNext.IsEnabled = true;
                btnLast.IsEnabled = true;
            }
            if (sedePage > 1)
            {
                btnFirst.IsEnabled = true;
                btnPrev.IsEnabled = true;
            }
        }
        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            DisablePaginationButtons();
            sedePage = 1;
            SedeGridData();
            ChangeLabelContent();
            EnablePaginationButtons();
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            DisablePaginationButtons();
            sedePage++;
            SedeGridData();
            ChangeLabelContent();
            EnablePaginationButtons();

        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            DisablePaginationButtons();
            sedePage--;
            SedeGridData();
            ChangeLabelContent();
            EnablePaginationButtons();
        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            DisablePaginationButtons();
            sedePage = (int)Math.Ceiling((double)sedeCount / (int)sizeCombo.SelectedItem);
            SedeGridData();
            ChangeLabelContent();
            EnablePaginationButtons();
        }

        private void sizeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sedeCount == 0)
                return;
            btnFirst_Click(sender, e);
        }

        private void listaComisiones_Click(object sender, RoutedEventArgs e)
        {
            Forms.ListaComisiones.Window1 win = new();
            win.Show();
        }

        private void informeCoordinacionDistrital_Click(object sender, RoutedEventArgs e)
        {
            Forms.InformeCoordinacionDistrital.Window1 win = new();
            win.Show();

        }
    }

    internal class Sede
    {
        public string id { get; set; }
        public string numero { get; set; }
        public string nombre { get; set; }
    }
}

    