using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using SqlOrganize;
using System;
using System.Collections;
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
using WpfAppMy.Forms.ListaComisiones;

namespace WpfAppMy.Windows.ProcesarDocentesProgramaFines
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DAO dao = new ();

        public Window1()
        {
            InitializeComponent();
        }

        private void ProcesarButton_Click(object sender, RoutedEventArgs e)
        {
            ProcesarDocentes();
        }

        private void ProcesarDocentes()
        {
            var comisiones = dao.ComisionesConPfid();
            var pfidComisiones = comisiones.Column<string>("comision-pfid");
            var docentes = JsonConvert.DeserializeObject<List<Docente>>(data.Text)!;
            info.Text = "Cantidad de docentes a procesar " + docentes.Count.ToString() + @"
";

            foreach (Docente docente in docentes)
            {

                #region insertar o actualizar docente (se insertan o actualizan todos)
                var d = docente.ConvertToDict();
                EntityValues v = ContainerApp.db.Values("persona").Set(d);
                var row = dao.RowByEntityUnique("persona", v.values);
                if (row != null) { 
                    EntityValues v2 = ContainerApp.db.Values("persona").Set(row).SetNotNull(v.values);
                    v = v2;
                    v.Reset();
                    var p = ContainerApp.db.Persist("persona").Update(v2.values).Exec();
                    ContainerApp.dbCache.Remove(p.detail);
                } else
                {
                    v.Default().Reset();
                    var p = ContainerApp.db.Persist("persona").Insert(v.values).Exec();
                    ContainerApp.dbCache.Remove(p.detail);
                }
                #endregion

                #region insertar o actualizar cargo
                foreach (var cargo in docente.cargos)
                {
                    if (pfidComisiones.Contains(cargo["comision"]))
                    {
                        string idCurso = dao.IdCurso(cargo["comision"], cargo["codigo"]);
                        Dictionary<string, object> rowTomaActiva = dao.TomaActiva(idCurso);


                    }

                   
                }
                #endregion
            }
            info.Text += docentes.Count.ToString();
        }


    }

    internal class Docente
    {
        public string numero_documento { set; get; }
        public string nombres { set; get; }
        public string apellidos { set; get; }
        public string descripcion_domicilio { set; get; }
        public string telefono { set; get; }
        public string email_abc { set; get; }
        public List<Dictionary<string, string>> cargos { set; get; }
    }
}
