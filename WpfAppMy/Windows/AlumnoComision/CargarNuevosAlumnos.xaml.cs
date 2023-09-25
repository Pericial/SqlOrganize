using SqlOrganize;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Utils;
using WpfAppMy.Data;
using WpfAppMy.Values;
using WpfAppMy.ViewModels;

namespace WpfAppMy.Windows.AlumnoComision
{
    /// <summary>
    /// Lógica de interacción para CargarNuevosAlumnos.xaml
    /// </summary>
    public partial class CargarNuevosAlumnos : Window
    {
        private string? IdComision;
        private ComisionRel comision;
        private ObservableCollection<CargarNuevosAlumnosViewModel> statusData = new();

        public CargarNuevosAlumnos(string? idComision = null)
        {

            InitializeComponent();
            this.IdComision = idComision;
            labelSedeRTextBox.IsReadOnly = true;
            Loaded += CargarNuevosAlumnos_Loaded;
            headers.Text = "persona-nombres, persona-apellidos, persona-numero_documento, persona-fecha_nacimiento";
        }

        private void CargarNuevosAlumnos_Loaded(object sender, RoutedEventArgs e)
        {
            var data = ContainerApp.dao.Get("comision", IdComision);
            comision = data.Obj<ComisionRel>();
            comision.label_sede_r = (string)ContainerApp.db.Values("comision").Values(data).Default("label_sede_r").Get("label_sede_r");
            DataContext = comision;
        }


        private void ProcesarButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> _headers = headers.Text.Split(",").Select(s => s.Trim());
            var _data = data.Text.Split("\r\n");
            var persist = ContainerApp.db.Persist();
            for(var j = 0; j < _data.Length; j++)
            {
                var d = _data[j];
                if (d.IsNullOrEmpty())
                    continue;

                var values = d.Split("\t");

                var personaData = new Dictionary<string, object>();
                for (var i = 0; i < _headers.Count(); i++) { 
                    if (values.ElementAt(i).IsNullOrEmpty()) continue;
                    personaData.Add(_headers.ElementAt(i), values.ElementAt(i));
                }

                #region Procesar persona
                var persona = (Persona)ContainerApp.db.Values("persona", "persona").Set(personaData).Default("label").Reset();
                var query = ContainerApp.db.Query("persona").Unique(persona);
                var personaExistenteData = ContainerApp.dbCache.Dict(query);
                if (!personaExistenteData.IsNullOrEmpty()) //existen datos de persona en la base
                {
                    var personaExistente = ContainerApp.db.Values("persona").Set(personaExistenteData!).Reset();
                    var dataDifferent = persona.Compare(personaExistente, ignoreNull:true);
                    if (!dataDifferent.IsNullOrEmpty())
                    {
                        personaExistente.Default("label");
                        var s = new CargarNuevosAlumnosViewModel()
                        {
                            row = j,
                            status = "error",
                            detail = "Los valores de persona existente son diferentes.",
                            data = "Nuevo: " + persona.Get("label").ToString() + ". Existente: " + personaExistente.Get("label").ToString()
                        };
                        statusData.Add(s);
                    }
                    persona.Set("id", personaExistenteData!["id"]);
                }
                else //no existen datos de persona en la base
                {
                    persona.Default().Reset();
                    persist.Insert(persona);
                    var s = new CargarNuevosAlumnosViewModel()
                    {
                        row = j,
                        status = "insert",
                        detail = "Persona agregada.",
                        data = persona.Get("label").ToString()!
                    };
                    statusData.Add(s);
                }
                #endregion

                #region Procesar alumno
                var alumno = ContainerApp.db.Values("alumno").Set("persona",persona.Get("id")).Reset();
                query = ContainerApp.db.Query("alumno").Unique(alumno);
                var alumnoExistenteData = ContainerApp.dbCache.Dict(query);
                if (!alumnoExistenteData.IsNullOrEmpty()) //existen datos de alumno en la base
                {
                    var alumnoExistente = alumnoExistenteData!.Obj<Data_alumno_r>();
                    alumno.Set("id", alumnoExistenteData!["id"]);
                    if (alumnoExistenteData["plan"].ToString() != comision.planificacion__plan)
                    {
                        statusData.Add(new()
                        {
                            row = j,
                            status = "warning",
                            detail = "El plan del alumno es diferente del plan de la comision.",
                            data = "Nuevo: " + comision.plan__orientacion + " " + comision.plan__resolucion + ". Existente: " + alumnoExistente.plan__orientacion + " " + alumnoExistente.plan__resolucion
                        });
                    }
                }
                else //no existen datos del alumno en la base
                {
                    alumno.Default().Set("plan", comision.planificacion__plan!).Reset().Insert(persist);
                    var s = new CargarNuevosAlumnosViewModel()
                    {
                        row = j,
                        status = "insert",
                        detail = "Alumno agregado.",
                        data = persona.Get("label").ToString()!
                    };
                    statusData.Add(s);
                }
                #endregion

                #region procesar asignacion
                var asignacion = ContainerApp.db.Values("alumno_comision").
                    Set("comision", comision.id!).
                    Set("alumno", alumno.Get("id"));
                query = ContainerApp.db.Query("alumno_comision").Unique(asignacion);
                var asignacionExistenteData = ContainerApp.dbCache.Dict(query);
                if (!asignacionExistenteData.IsNullOrEmpty()) //existen datos de alumno en la base
                {
                    asignacion.Set("id", asignacionExistenteData["id"]!);
                    if (asignacionExistenteData["estado"].ToString() != "Activo")
                    {
                        statusData.Add(new()
                        {
                            row = j,
                            status = "warning",
                            detail = "Existe asignacion no No Activa.",
                            data = persona.Get("label").ToString()
                        });
                    }
                }
                else //no existen datos de asignacion
                {
                    asignacion.Default().Reset().Insert(persist);
                    var s = new CargarNuevosAlumnosViewModel()
                    {
                        row = j,
                        status = "insert",
                        detail = "Asignacion agregada.",
                        data = persona.Get("label").ToString()!
                    };
                    statusData.Add(s);
                }
                #endregion

                //persist.Transaction();
                //ContainerApp.dbCache.Remove(persist.detail);
            }



            //EntityValues asignacion = ContainerApp.db.Values("persona").Set(data);
        }
    }

    internal class CargarNuevosAlumnosViewModel
    {
        public int row { get; set; }
        public string status { get; set; }
        public string detail { get; set; }

        public string data { get; set; }
    }
   
}
