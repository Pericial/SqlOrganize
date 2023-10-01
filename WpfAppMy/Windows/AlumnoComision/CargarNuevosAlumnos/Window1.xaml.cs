using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
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
using WpfAppMy.ViewModels;

namespace WpfAppMy.Windows.AlumnoComision.CargarNuevosAlumnos
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string? IdComision;
        private Comision comision;
        private ObservableCollection<ViewModel> statusData = new();
        private DAO.AlumnoComision alumnoComisionDAO = new();
        private EntityPersist persist = ContainerApp.db.Persist();
        private SqlOrganize.DAO dao = new(ContainerApp.db);


        public Window1(string? idComision = null)
        {

            InitializeComponent();
            this.IdComision = idComision;
            labelTextBox.IsReadOnly = true;
            Loaded += CargarNuevosAlumnos_Loaded;
            headers.Text = "persona-nombres, persona-apellidos, persona-numero_documento, persona-fecha_nacimiento";
            statusGrid.ItemsSource = statusData;
        }

        private void CargarNuevosAlumnos_Loaded(object sender, RoutedEventArgs e)
        {
            var data = dao.Get("comision", IdComision);
            comision = data.Obj<Comision>();
            comision.label = ((Values.Comision)ContainerApp.db.Values("comision").Values(data)).ToStringNombreSede();
            DataContext = comision;
        }

        


        private void ProcesarButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> _headers = headers.Text.Split(",").Select(s => s.Trim());
            var _data = data.Text.Split("\r\n");
            statusData.Clear();
            for (var j = 0; j < _data.Length; j++)
            {
                if (_data[j].IsNullOrEmpty())
                    continue;

                var values = _data[j].Split("\t");

                var personaData = new Dictionary<string, object>();
                for (var i = 0; i < _headers.Count(); i++)
                {
                    if (values.ElementAt(i).IsNullOrEmpty()) continue;
                    personaData.Add(_headers.ElementAt(i), values.ElementAt(i));
                }

                #region Procesar persona
                var persona = (Values.Persona)ContainerApp.db.Values("persona", "persona").Set(personaData).Reset();
                var personaExistenteData = ContainerApp.db.Query("persona").Unique(persona).DictCache();

                if (!personaExistenteData.IsNullOrEmpty()) //existen datos de persona en la base
                {
                    var personaExistente = ContainerApp.db.Values("persona").Values(personaExistenteData);

                    var dataDifferent = persona.Compare(personaExistente!, ignoreNull: true);
                    if (!dataDifferent.IsNullOrEmpty())
                    {
                        statusData.Add(new ViewModel()
                        {
                            row = j,
                            status = "error",
                            detail = "Los valores de persona existente son diferentes no se realizara ningún registro",
                            data = "Nuevo: " + persona.ToString() + ". Existente: " + (personaExistente as Values.Persona)!.ToString()
                        });
                        continue;
                    }
                    persona.Set("id", personaExistente!.Get("id"));
                }
                else //no existen datos de persona en la base
                {
                    persona.Default().Reset().Insert(persist);
                    statusData.Add(new ViewModel()
                    {
                        row = j,
                        status = "insert",
                        detail = "Persona agregada.",
                        data = persona.ToString()
                    });
                }
                #endregion

                #region Procesar alumno
                var alumno = ContainerApp.db.Values("alumno").Set("persona", persona.Get("id"));
                var alumnoExistenteData = ContainerApp.db.Query("alumno").Unique(alumno).DictCache();

                if (!alumnoExistenteData.IsNullOrEmpty()) //existen datos de alumno en la base
                {
                    var alumnoExistente = ContainerApp.db.Values("alumno").Values(alumnoExistenteData);

                    statusData.Add(new()
                    {
                        row = j,
                        status = "info",
                        detail = "El alumno ya existe.",
                        data = persona.ToString()
                    });
                    alumno.Set("id", alumnoExistente!.Get("id"));
                    if (alumnoExistente!.Get("plan").IsNullOrEmptyOrDbNull())
                    {
                        statusData.Add(new()
                        {
                            row = j,
                            status = "update",
                            detail = "Se actualizo el plan del alumno que estaba vacio.",
                            data = persona.ToString()
                        });
                    }
                    else if (alumnoExistente!.Get("plan")!.ToString() != comision.planificacion__plan)
                    {

                        statusData.Add(new()
                        {
                            row = j,
                            status = "warning",
                            detail = "El plan del alumno es diferente del plan de la comision.",
                            data = "Nuevo: " + comision.plan__orientacion + " " + comision.plan__resolucion + ". Existente: " + alumnoExistente.ValuesTree("plan")?.ToString()
                        });
                    }
                }
                else //no existen datos del alumno en la base
                {
                    alumno.Default().Set("plan", comision.planificacion__plan!).Reset().Insert(persist);
                    statusData.Add( new ViewModel()
                    {
                        row = j,
                        status = "insert",
                        detail = "Alumno agregado.",
                        data = persona.ToString()
                    });
                }
                #endregion

                #region procesar asignacion
                var asignacion = ContainerApp.db.Values("alumno_comision").
                    Set("comision", comision.id!).
                    Set("alumno", alumno.Get("id"));
                var asignacionExistenteData = ContainerApp.db.Query("alumno_comision").Unique(asignacion).DictCache();
                if (!asignacionExistenteData.IsNullOrEmpty()) //existen datos de alumno en la base
                {
                    statusData.Add(new()
                    {
                        row = j,
                        status = "warning",
                        detail = "Ya existe asignacion en la comisión actual con estado " + asignacionExistenteData["estado"].ToString(),
                        data = persona.ToString()
                    });
                }
                else //no existen datos de asignacion
                {
                    asignacion.Default().Reset().Insert(persist);
                    statusData.Add(
                        new ViewModel()
                    {
                        row = j,
                        status = "insert",
                        detail = "Asignacion agregada.",
                        data = persona.ToString()!
                    });
                }
                #endregion

                #region controlar asignaciones activas del semestre
                var otrasAsignacionesDelSemestre =  alumnoComisionDAO.AsignacionesDelAlumnoEnOtrasComisionesAutorizadasDelSemestre(comision.calendario__anio!, comision.calendario__semestre!, comision.id!, alumno.Get("id"));
                foreach(var a in otrasAsignacionesDelSemestre)
                {
                    var comD = ContainerApp.db.Query("comision").CacheById(a["comision"]);
                    var comV = (Values.Comision)ContainerApp.db.Values("comision").Values(comD!);

                    statusData.Add(new ViewModel()
                    {
                        row = j,
                        status = "warning",
                        detail = "Se encuentra en otra comision del mismo semestre",
                        data = persona.ToString() + " en " + comV.ToStringNombreSede() + " con estado " + a["estado"].ToString()
                    });
                }
                #endregion

                #region informar otras asignaciones activas del alumno
                var otrasAsignaciones = alumnoComisionDAO.AsignacionesDelAlumnoEnOtrasComisionesAutorizadas(comision.id!, alumno.Get("id"));
                foreach (var a in otrasAsignaciones)
                {
                    var comD = ContainerApp.db.Query("comision").CacheById(a["comision-id"]);
                    var comV = (Values.Comision)ContainerApp.db.Values("comision").Values(comD!);

                    statusData.Add(new ViewModel()
                    {
                        row = j,
                        status = "info",
                        detail = "Asignacion en otra comisión",
                        data = persona.ToString() + " en " + comV.ToStringNombreSede() + " con estado " + a["estado"]
                    });
                }
                #endregion

                
            }



        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea guardar los alumnos?",
                    "Guardar",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                persist.Transaction().RemoveCache();
            }
        }
    }
}
