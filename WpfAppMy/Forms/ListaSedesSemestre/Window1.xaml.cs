﻿using SqlOrganize;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Utils;

namespace WpfAppMy.Forms.ListaSedesSemestre
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        ComisionSearch comisionSearch = new();

        private DAO.Comision comisionDAO = new();

        public Window1()
        {
            InitializeComponent();

            DataContext = comisionSearch;

            this.autorizadaCombo.SelectedValuePath = "Key";
            this.autorizadaCombo.DisplayMemberPath = "Value";
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool?, string>(null, "(Todos)"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(true, "Sí"));
            this.autorizadaCombo.Items.Add(new KeyValuePair<bool, string>(false, "No"));

            sedeGrid.CellEditEnding += SedeGrid_CellEditEnding;

            ComisionSearch();
        }

        private void ComisionSearch()
        {
            List<Dictionary<string, object>> list = comisionDAO.Search(comisionSearch);
            sedeGrid.ItemsSource = list.ConvertToListOfObject<Sede>();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            ComisionSearch();
        }

        private void SedeGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    string entityName = "comision";
                    string key = (column.Binding as Binding).Path.Path; //column's binding
                    string _IdKey = "_Id";

                    Sede source = (Sede)e.Row.DataContext;
                    if (key.Contains("__"))
                    {
                        int indexSeparator = key.IndexOf("__");
                        string fieldId = key.Substring(0, indexSeparator);
                        entityName = ContainerApp.Db().Entity("comision").relations[fieldId].refEntityName;
                        _IdKey = entityName + "___Id";
                        key = key.Substring(indexSeparator+2); //se suma la cantidad de caracteres del separador
                    }

                    string value = (e.EditingElement as TextBox)!.Text;
                    string _Id = (string)source.GetType().GetProperty(_IdKey)!.GetValue(source, null)!;
                    EntityPersist p = ContainerApp.Db().Persist(entityName).UpdateValue(key, value, _Id).Exec();
                    ContainerApp.QueryCache().Remove(p.detail);
                }
            }
        }
    }

    internal class ComisionSearch
    {
        public string calendario__anio { get; set; } = DateTime.Now.Year.ToString();
        public int calendario__semestre { get; set; } = DateTime.Now.ToSemester();
        public bool? autorizada { get; set; } = true;
    }


    internal class Sede
    {

        public string sede___Id { get; set; }
        public string sede__numero { get; set; }
        public string sede__nombre { get; set; }
        public string sede__pfid { get; set; }


        public string domicilio___Id { get; set; }
        public string domicilio__calle { get; set; }
        public string domicilio__numero { get; set; }
        public string domicilio__entre { get; set; }
        public string domicilio__localidad { get; set; }
        public string domicilio__barrio { get; set; }

    }
}