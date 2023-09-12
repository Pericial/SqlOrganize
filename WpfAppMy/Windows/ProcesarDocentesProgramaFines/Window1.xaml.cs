﻿using MySqlX.XDevAPI.Relational;
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
        List<string> logs = new();

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
            var pfidComisiones = dao.PfidComisiones();
            var docentes = JsonConvert.DeserializeObject<List<Docente>>(data.Text)!;
            logs.Add("Cantidad de docentes a procesar" + docentes.Count.ToString());

            foreach (Docente docente in docentes)
            {

                #region insertar o actualizar docente (se insertan o actualizan todos)
                var d = docente.ToDict();
                EntityValues vPersona = ContainerApp.db.Values("persona").Set(d).Reset();
                var row = dao.RowByEntityUnique("persona", vPersona.values);
                if (row != null) {
                    EntityValues vPersonaAux = ContainerApp.db.Values("persona").Set(row);
                    Dictionary<string, object> valuesToUpdate = vPersonaAux.Compare(vPersona.values);
                    vPersonaAux.SetNotNull(valuesToUpdate);
                    vPersona = vPersonaAux;
                    vPersona.Reset();
                    if (!valuesToUpdate.IsNullOrEmpty())
                    {
                        var p = ContainerApp.db.Persist("persona").Update(vPersona.values).Exec();
                        ContainerApp.dbCache.Remove(p.detail);
                    }
                } else
                {
                    vPersona.Default().Reset();
                    var p = ContainerApp.db.Persist("persona").Insert(vPersona.values).Exec();
                    ContainerApp.dbCache.Remove(p.detail);
                }
                #endregion

                #region insertar o actualizar cargo
                foreach (var cargo in docente.cargos)
                {
                    if (pfidComisiones.Contains(cargo["comision"]))
                    {
                        string idCurso = dao.IdCurso(cargo["comision"], cargo["codigo"]);
                        if (idCurso.IsNullOrEmpty())
                        {
                            logs.Add("No existe curso " + cargo["comision"] + " " + cargo["codigo"]);
                            continue;

                        }

                        Dictionary<string, object> rowTomaActiva = dao.TomaActiva(idCurso);
                        if(rowTomaActiva != null)
                        {
                            if (!rowTomaActiva["docente"].Equals(vPersona.Get("id")))
                                logs.Add("Existe una toma activa con otro docente en " + cargo["comision"] + " " + cargo["codigo"]);
                            else 
                                logs.Add("La toma ya se encuentra cargada " + cargo["comision"] + " " + cargo["codigo"]);
                        }
                        else
                        {
                            EntityValues vToma = ContainerApp.db.Values("toma").
                                Set("curso", idCurso).
                                Set("docente", vPersona.Get("id")).
                                Set("estado", "Aprobada").
                                Set("estado_contralor", "Pendiente").
                                Set("tipo_movimiento", "AI").
                                Set("fecha_toma",new DateTime(2023,08,07));
                            vToma.Default().Reset();
                            var p = ContainerApp.db.Persist("toma").Insert(vToma.values).Exec();
                            ContainerApp.dbCache.Remove(p.detail);
                        }

                    }

                   
                }
                #endregion
            }
            info.Text += String.Join(@"
",logs);
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
