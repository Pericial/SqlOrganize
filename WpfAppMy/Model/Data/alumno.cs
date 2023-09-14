using System;
using System.ComponentModel;

namespace WpfAppMy.Model.Data
{
    public class Model_alumno : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string _anio_ingreso;
        public string anio_ingreso
        {
            get { return _anio_ingreso; }
            set { _anio_ingreso = value; NotifyPropertyChanged(); }
        }
        private string _observaciones;
        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        private string _persona;
        public string persona
        {
            get { return _persona; }
            set { _persona = value; NotifyPropertyChanged(); }
        }
        private string _estado_inscripcion;
        public string estado_inscripcion
        {
            get { return _estado_inscripcion; }
            set { _estado_inscripcion = value; NotifyPropertyChanged(); }
        }
        private DateTime _fecha_titulacion;
        public DateTime fecha_titulacion
        {
            get { return _fecha_titulacion; }
            set { _fecha_titulacion = value; NotifyPropertyChanged(); }
        }
        private string _plan;
        public string plan
        {
            get { return _plan; }
            set { _plan = value; NotifyPropertyChanged(); }
        }
        private string _resolucion_inscripcion;
        public string resolucion_inscripcion
        {
            get { return _resolucion_inscripcion; }
            set { _resolucion_inscripcion = value; NotifyPropertyChanged(); }
        }
        private short _anio_inscripcion;
        public short anio_inscripcion
        {
            get { return _anio_inscripcion; }
            set { _anio_inscripcion = value; NotifyPropertyChanged(); }
        }
        private short _semestre_inscripcion;
        public short semestre_inscripcion
        {
            get { return _semestre_inscripcion; }
            set { _semestre_inscripcion = value; NotifyPropertyChanged(); }
        }
        private short _semestre_ingreso;
        public short semestre_ingreso
        {
            get { return _semestre_ingreso; }
            set { _semestre_ingreso = value; NotifyPropertyChanged(); }
        }
        private string _adeuda_legajo;
        public string adeuda_legajo
        {
            get { return _adeuda_legajo; }
            set { _adeuda_legajo = value; NotifyPropertyChanged(); }
        }
        private string _adeuda_deudores;
        public string adeuda_deudores
        {
            get { return _adeuda_deudores; }
            set { _adeuda_deudores = value; NotifyPropertyChanged(); }
        }
        private string _documentacion_inscripcion;
        public string documentacion_inscripcion
        {
            get { return _documentacion_inscripcion; }
            set { _documentacion_inscripcion = value; NotifyPropertyChanged(); }
        }
        private bool _anio_inscripcion_completo;
        public bool anio_inscripcion_completo
        {
            get { return _anio_inscripcion_completo; }
            set { _anio_inscripcion_completo = value; NotifyPropertyChanged(); }
        }
        private string _establecimiento_inscripcion;
        public string establecimiento_inscripcion
        {
            get { return _establecimiento_inscripcion; }
            set { _establecimiento_inscripcion = value; NotifyPropertyChanged(); }
        }
        private string _libro_folio;
        public string libro_folio
        {
            get { return _libro_folio; }
            set { _libro_folio = value; NotifyPropertyChanged(); }
        }
        private string _libro;
        public string libro
        {
            get { return _libro; }
            set { _libro = value; NotifyPropertyChanged(); }
        }
        private string _folio;
        public string folio
        {
            get { return _folio; }
            set { _folio = value; NotifyPropertyChanged(); }
        }
        private string _comentarios;
        public string comentarios
        {
            get { return _comentarios; }
            set { _comentarios = value; NotifyPropertyChanged(); }
        }
        private bool _tiene_dni;
        public bool tiene_dni
        {
            get { return _tiene_dni; }
            set { _tiene_dni = value; NotifyPropertyChanged(); }
        }
        private bool _tiene_constancia;
        public bool tiene_constancia
        {
            get { return _tiene_constancia; }
            set { _tiene_constancia = value; NotifyPropertyChanged(); }
        }
        private bool _tiene_certificado;
        public bool tiene_certificado
        {
            get { return _tiene_certificado; }
            set { _tiene_certificado = value; NotifyPropertyChanged(); }
        }
        private bool _previas_completas;
        public bool previas_completas
        {
            get { return _previas_completas; }
            set { _previas_completas = value; NotifyPropertyChanged(); }
        }
        private bool _tiene_partida;
        public bool tiene_partida
        {
            get { return _tiene_partida; }
            set { _tiene_partida = value; NotifyPropertyChanged(); }
        }
        private DateTime _creado;
        public DateTime creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        private bool _confirmado_direccion;
        public bool confirmado_direccion
        {
            get { return _confirmado_direccion; }
            set { _confirmado_direccion = value; NotifyPropertyChanged(); }
        }
        private string _persona__id;
        public string persona__id
        {
            get { return _persona__id; }
            set { _persona__id = value; NotifyPropertyChanged(); }
        }
        private string _persona__nombres;
        public string persona__nombres
        {
            get { return _persona__nombres; }
            set { _persona__nombres = value; NotifyPropertyChanged(); }
        }
        private string _persona__apellidos;
        public string persona__apellidos
        {
            get { return _persona__apellidos; }
            set { _persona__apellidos = value; NotifyPropertyChanged(); }
        }
        private DateTime _persona__fecha_nacimiento;
        public DateTime persona__fecha_nacimiento
        {
            get { return _persona__fecha_nacimiento; }
            set { _persona__fecha_nacimiento = value; NotifyPropertyChanged(); }
        }
        private string _persona__numero_documento;
        public string persona__numero_documento
        {
            get { return _persona__numero_documento; }
            set { _persona__numero_documento = value; NotifyPropertyChanged(); }
        }
        private string _persona__cuil;
        public string persona__cuil
        {
            get { return _persona__cuil; }
            set { _persona__cuil = value; NotifyPropertyChanged(); }
        }
        private string _persona__genero;
        public string persona__genero
        {
            get { return _persona__genero; }
            set { _persona__genero = value; NotifyPropertyChanged(); }
        }
        private string _persona__apodo;
        public string persona__apodo
        {
            get { return _persona__apodo; }
            set { _persona__apodo = value; NotifyPropertyChanged(); }
        }
        private string _persona__telefono;
        public string persona__telefono
        {
            get { return _persona__telefono; }
            set { _persona__telefono = value; NotifyPropertyChanged(); }
        }
        private string _persona__email;
        public string persona__email
        {
            get { return _persona__email; }
            set { _persona__email = value; NotifyPropertyChanged(); }
        }
        private string _persona__email_abc;
        public string persona__email_abc
        {
            get { return _persona__email_abc; }
            set { _persona__email_abc = value; NotifyPropertyChanged(); }
        }
        private DateTime _persona__alta;
        public DateTime persona__alta
        {
            get { return _persona__alta; }
            set { _persona__alta = value; NotifyPropertyChanged(); }
        }
        private string _persona__domicilio;
        public string persona__domicilio
        {
            get { return _persona__domicilio; }
            set { _persona__domicilio = value; NotifyPropertyChanged(); }
        }
        private string _persona__lugar_nacimiento;
        public string persona__lugar_nacimiento
        {
            get { return _persona__lugar_nacimiento; }
            set { _persona__lugar_nacimiento = value; NotifyPropertyChanged(); }
        }
        private bool _persona__telefono_verificado;
        public bool persona__telefono_verificado
        {
            get { return _persona__telefono_verificado; }
            set { _persona__telefono_verificado = value; NotifyPropertyChanged(); }
        }
        private bool _persona__email_verificado;
        public bool persona__email_verificado
        {
            get { return _persona__email_verificado; }
            set { _persona__email_verificado = value; NotifyPropertyChanged(); }
        }
        private bool _persona__info_verificada;
        public bool persona__info_verificada
        {
            get { return _persona__info_verificada; }
            set { _persona__info_verificada = value; NotifyPropertyChanged(); }
        }
        private string _persona__descripcion_domicilio;
        public string persona__descripcion_domicilio
        {
            get { return _persona__descripcion_domicilio; }
            set { _persona__descripcion_domicilio = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__id;
        public string domicilio__id
        {
            get { return _domicilio__id; }
            set { _domicilio__id = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__calle;
        public string domicilio__calle
        {
            get { return _domicilio__calle; }
            set { _domicilio__calle = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__entre;
        public string domicilio__entre
        {
            get { return _domicilio__entre; }
            set { _domicilio__entre = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__numero;
        public string domicilio__numero
        {
            get { return _domicilio__numero; }
            set { _domicilio__numero = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__piso;
        public string domicilio__piso
        {
            get { return _domicilio__piso; }
            set { _domicilio__piso = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__departamento;
        public string domicilio__departamento
        {
            get { return _domicilio__departamento; }
            set { _domicilio__departamento = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__barrio;
        public string domicilio__barrio
        {
            get { return _domicilio__barrio; }
            set { _domicilio__barrio = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__localidad;
        public string domicilio__localidad
        {
            get { return _domicilio__localidad; }
            set { _domicilio__localidad = value; NotifyPropertyChanged(); }
        }
        private string _plan__id;
        public string plan__id
        {
            get { return _plan__id; }
            set { _plan__id = value; NotifyPropertyChanged(); }
        }
        private string _plan__orientacion;
        public string plan__orientacion
        {
            get { return _plan__orientacion; }
            set { _plan__orientacion = value; NotifyPropertyChanged(); }
        }
        private string _plan__resolucion;
        public string plan__resolucion
        {
            get { return _plan__resolucion; }
            set { _plan__resolucion = value; NotifyPropertyChanged(); }
        }
        private string _plan__distribucion_horaria;
        public string plan__distribucion_horaria
        {
            get { return _plan__distribucion_horaria; }
            set { _plan__distribucion_horaria = value; NotifyPropertyChanged(); }
        }
        private string _plan__pfid;
        public string plan__pfid
        {
            get { return _plan__pfid; }
            set { _plan__pfid = value; NotifyPropertyChanged(); }
        }
        private string _resolucion_inscripcion__id;
        public string resolucion_inscripcion__id
        {
            get { return _resolucion_inscripcion__id; }
            set { _resolucion_inscripcion__id = value; NotifyPropertyChanged(); }
        }
        private string _resolucion_inscripcion__numero;
        public string resolucion_inscripcion__numero
        {
            get { return _resolucion_inscripcion__numero; }
            set { _resolucion_inscripcion__numero = value; NotifyPropertyChanged(); }
        }
        private DateTime _resolucion_inscripcion__anio;
        public DateTime resolucion_inscripcion__anio
        {
            get { return _resolucion_inscripcion__anio; }
            set { _resolucion_inscripcion__anio = value; NotifyPropertyChanged(); }
        }
        private string _resolucion_inscripcion__tipo;
        public string resolucion_inscripcion__tipo
        {
            get { return _resolucion_inscripcion__tipo; }
            set { _resolucion_inscripcion__tipo = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
