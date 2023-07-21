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
        private DAO.Sede sedeDAO = new(); //objeto de acceso a datos

        #region sedeGrid con paginacion
        private int sedeCount = 0; //cantidad total de elementos
        protected int sedePage = 1; //pagina a mostrar
        private ObservableCollection<Sede> sedeData = new ();
    
        public MainWindow()
        {
            InitializeComponent();

            sedeSizeCombo.Items.Add(10);
            sedeSizeCombo.Items.Add(100);
            sedeSizeCombo.Items.Add(500);
            sedeSizeCombo.Items.Add(1000);
            sedeSizeCombo.SelectedItem = 10;
            sedeGrid.ItemsSource = sedeData;
            this.Loaded += MainWindow_Loaded;

            sedeGrid.CellEditEnding += SedeGrid_CellEditEnding;
        }

        private void SedeGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    string key = (column.Binding as Binding).Path.Path; //column's binding
                    string value = (e.EditingElement as TextBox).Text;
                    string _Id = (e.Row.DataContext as Sede).id;

                    sedeDAO.UpdateValue(key, value, _Id);
                }
            }
        }

        private void SedeGridData()
        {
            List<Dictionary<string, object>> sedeList = sedeDAO.FiltroPaginacion(sedePage, (int)sedeSizeCombo.SelectedItem);
            sedeData.Clear();
            sedeData.AddRange(sedeList.ConvertToObservableCollectionOfObject<Sede>());
        }

        private void OnSedePaginationChanged()
        {
            SedeGridData();
            ChangeLabelContent();
            EnablePaginationButtons();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DisablePaginationButtons();
            sedeCount = sedeDAO.CantidadTotal();
            OnSedePaginationChanged();
        }   

        private void ChangeLabelContent()
        {
            int count = sedePage * (int)sedeSizeCombo.SelectedItem;
            if(count > sedeCount)
                count = sedeCount;
            sedePageInformationLabel.Content = count + " of " + sedeCount;
        }

        private void DisablePaginationButtons()
        {
            sedePreviousButton.IsEnabled = false;
            sedeFirstButton.IsEnabled = false;
            sedeNextButton.IsEnabled = false;
            sedeLastButton.IsEnabled = false;
        }

        private void EnablePaginationButtons()
        {
            if (sedeCount > (sedePage * (int)sedeSizeCombo.SelectedItem))
            {
                sedeNextButton.IsEnabled = true;
                sedeLastButton.IsEnabled = true;
            }
            if (sedePage > 1)
            {
                sedeFirstButton.IsEnabled = true;
                sedePreviousButton.IsEnabled = true;
            }
        }
        private void sedeFirstButton_Click(object sender, System.EventArgs e)
        {
            DisablePaginationButtons();
            sedePage = 1;
            OnSedePaginationChanged();
        }

        private void sedeNextButton_Click(object sender, System.EventArgs e)
        {
            DisablePaginationButtons();
            sedePage++;
            OnSedePaginationChanged();
        }

        private void sedePreviousButton_Click(object sender, System.EventArgs e)
        {
            DisablePaginationButtons();
            sedePage--;
            OnSedePaginationChanged();
        }

        private void sedeLastButton_Click(object sender, System.EventArgs e)
        {
            DisablePaginationButtons();
            sedePage = (int)Math.Ceiling((double)sedeCount / (int)sedeSizeCombo.SelectedItem);
            OnSedePaginationChanged();
        }

        private void sedeSizeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sedeCount == 0)
                return;
            sedeFirstButton_Click(sender, e);
        }
        #endregion



        #region menu principal
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

        private void listaSedesSemestre_Click(object sender, RoutedEventArgs e)
        {
            Forms.ListaSedesSemestre.Window1 win = new();
            win.Show();

        }
        #endregion
    }

    internal class Sede
    {
        public string id { get; set; }
        public string numero { get; set; }
        public string nombre { get; set; }
        public string domicilio__calle { get; set; }
    }
}

    