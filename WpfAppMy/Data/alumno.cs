using System;
using System.ComponentModel;

namespace 
{
    public class Data_alumno : INotifyPropertyChanged
    {
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _anio_ingreso;
        public string? anio_ingreso
        {
            get { return _anio_ingreso; }
            set { _anio_ingreso = value; NotifyPropertyChanged(); }
        }
        private string? _observaciones;
        public string? observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        private string? _persona;
        public string? persona
        {
            get { return _persona; }
            set { _persona = value; NotifyPropertyChanged(); }
        }
        private string? _estado_inscripcion;
        public string? estado_inscripcion
        {
            get { return _estado_inscripcion; }
            set { _estado_inscripcion = value; NotifyPropertyChanged(); }
        }
        private DateTime? _fecha_titulacion;
        public DateTime? fecha_titulacion
        {
            get { return _fecha_titulacion; }
            set { _fecha_titulacion = value; NotifyPropertyChanged(); }
        }
        private string? _plan;
        public string? plan
        {
            get { return _plan; }
            set { _plan = value; NotifyPropertyChanged(); }
        }
        private string? _resolucion_inscripcion;
        public string? resolucion_inscripcion
        {
            get { return _resolucion_inscripcion; }
            set { _resolucion_inscripcion = value; NotifyPropertyChanged(); }
        }
        private short? _anio_inscripcion;
        public short? anio_inscripcion
        {
            get { return _anio_inscripcion; }
            set { _anio_inscripcion = value; NotifyPropertyChanged(); }
        }
        private short? _semestre_inscripcion;
        public short? semestre_inscripcion
        {
            get { return _semestre_inscripcion; }
            set { _semestre_inscripcion = value; NotifyPropertyChanged(); }
        }
        private short? _semestre_ingreso;
        public short? semestre_ingreso
        {
            get { return _semestre_ingreso; }
            set { _semestre_ingreso = value; NotifyPropertyChanged(); }
        }
        private string? _adeuda_legajo;
        public string? adeuda_legajo
        {
            get { return _adeuda_legajo; }
            set { _adeuda_legajo = value; NotifyPropertyChanged(); }
        }
        private string? _adeuda_deudores;
        public string? adeuda_deudores
        {
            get { return _adeuda_deudores; }
            set { _adeuda_deudores = value; NotifyPropertyChanged(); }
        }
        private string? _documentacion_inscripcion;
        public string? documentacion_inscripcion
        {
            get { return _documentacion_inscripcion; }
            set { _documentacion_inscripcion = value; NotifyPropertyChanged(); }
        }
        private bool? _anio_inscripcion_completo;
        public bool? anio_inscripcion_completo
        {
            get { return _anio_inscripcion_completo; }
            set { _anio_inscripcion_completo = value; NotifyPropertyChanged(); }
        }
        private string? _establecimiento_inscripcion;
        public string? establecimiento_inscripcion
        {
            get { return _establecimiento_inscripcion; }
            set { _establecimiento_inscripcion = value; NotifyPropertyChanged(); }
        }
        private string? _libro_folio;
        public string? libro_folio
        {
            get { return _libro_folio; }
            set { _libro_folio = value; NotifyPropertyChanged(); }
        }
        private string? _libro;
        public string? libro
        {
            get { return _libro; }
            set { _libro = value; NotifyPropertyChanged(); }
        }
        private string? _folio;
        public string? folio
        {
            get { return _folio; }
            set { _folio = value; NotifyPropertyChanged(); }
        }
        private string? _comentarios;
        public string? comentarios
        {
            get { return _comentarios; }
            set { _comentarios = value; NotifyPropertyChanged(); }
        }
        private bool? _tiene_dni;
        public bool? tiene_dni
        {
            get { return _tiene_dni; }
            set { _tiene_dni = value; NotifyPropertyChanged(); }
        }
        private bool? _tiene_constancia;
        public bool? tiene_constancia
        {
            get { return _tiene_constancia; }
            set { _tiene_constancia = value; NotifyPropertyChanged(); }
        }
        private bool? _tiene_certificado;
        public bool? tiene_certificado
        {
            get { return _tiene_certificado; }
            set { _tiene_certificado = value; NotifyPropertyChanged(); }
        }
        private bool? _previas_completas;
        public bool? previas_completas
        {
            get { return _previas_completas; }
            set { _previas_completas = value; NotifyPropertyChanged(); }
        }
        private bool? _tiene_partida;
        public bool? tiene_partida
        {
            get { return _tiene_partida; }
            set { _tiene_partida = value; NotifyPropertyChanged(); }
        }
        private DateTime? _creado;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        private bool? _confirmado_direccion;
        public bool? confirmado_direccion
        {
            get { return _confirmado_direccion; }
            set { _confirmado_direccion = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
