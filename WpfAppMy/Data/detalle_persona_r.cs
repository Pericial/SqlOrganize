using System;

namespace WpfAppMy.Data
{
    public class Data_detalle_persona_r : Data_detalle_persona
    {
        private string? _archivo__id;
        public string? archivo__id
        {
            get { return _archivo__id; }
            set { _archivo__id = value; NotifyPropertyChanged(); }
        }
        private string? _archivo__name;
        public string? archivo__name
        {
            get { return _archivo__name; }
            set { _archivo__name = value; NotifyPropertyChanged(); }
        }
        private string? _archivo__type;
        public string? archivo__type
        {
            get { return _archivo__type; }
            set { _archivo__type = value; NotifyPropertyChanged(); }
        }
        private string? _archivo__content;
        public string? archivo__content
        {
            get { return _archivo__content; }
            set { _archivo__content = value; NotifyPropertyChanged(); }
        }
        private uint? _archivo__size;
        public uint? archivo__size
        {
            get { return _archivo__size; }
            set { _archivo__size = value; NotifyPropertyChanged(); }
        }
        private DateTime? _archivo__created;
        public DateTime? archivo__created
        {
            get { return _archivo__created; }
            set { _archivo__created = value; NotifyPropertyChanged(); }
        }
        private string? _persona__id;
        public string? persona__id
        {
            get { return _persona__id; }
            set { _persona__id = value; NotifyPropertyChanged(); }
        }
        private string? _persona__nombres;
        public string? persona__nombres
        {
            get { return _persona__nombres; }
            set { _persona__nombres = value; NotifyPropertyChanged(); }
        }
        private string? _persona__apellidos;
        public string? persona__apellidos
        {
            get { return _persona__apellidos; }
            set { _persona__apellidos = value; NotifyPropertyChanged(); }
        }
        private DateTime? _persona__fecha_nacimiento;
        public DateTime? persona__fecha_nacimiento
        {
            get { return _persona__fecha_nacimiento; }
            set { _persona__fecha_nacimiento = value; NotifyPropertyChanged(); }
        }
        private string? _persona__numero_documento;
        public string? persona__numero_documento
        {
            get { return _persona__numero_documento; }
            set { _persona__numero_documento = value; NotifyPropertyChanged(); }
        }
        private string? _persona__cuil;
        public string? persona__cuil
        {
            get { return _persona__cuil; }
            set { _persona__cuil = value; NotifyPropertyChanged(); }
        }
        private string? _persona__genero;
        public string? persona__genero
        {
            get { return _persona__genero; }
            set { _persona__genero = value; NotifyPropertyChanged(); }
        }
        private string? _persona__apodo;
        public string? persona__apodo
        {
            get { return _persona__apodo; }
            set { _persona__apodo = value; NotifyPropertyChanged(); }
        }
        private string? _persona__telefono;
        public string? persona__telefono
        {
            get { return _persona__telefono; }
            set { _persona__telefono = value; NotifyPropertyChanged(); }
        }
        private string? _persona__email;
        public string? persona__email
        {
            get { return _persona__email; }
            set { _persona__email = value; NotifyPropertyChanged(); }
        }
        private string? _persona__email_abc;
        public string? persona__email_abc
        {
            get { return _persona__email_abc; }
            set { _persona__email_abc = value; NotifyPropertyChanged(); }
        }
        private DateTime? _persona__alta;
        public DateTime? persona__alta
        {
            get { return _persona__alta; }
            set { _persona__alta = value; NotifyPropertyChanged(); }
        }
        private string? _persona__domicilio;
        public string? persona__domicilio
        {
            get { return _persona__domicilio; }
            set { _persona__domicilio = value; NotifyPropertyChanged(); }
        }
        private string? _persona__lugar_nacimiento;
        public string? persona__lugar_nacimiento
        {
            get { return _persona__lugar_nacimiento; }
            set { _persona__lugar_nacimiento = value; NotifyPropertyChanged(); }
        }
        private bool? _persona__telefono_verificado;
        public bool? persona__telefono_verificado
        {
            get { return _persona__telefono_verificado; }
            set { _persona__telefono_verificado = value; NotifyPropertyChanged(); }
        }
        private bool? _persona__email_verificado;
        public bool? persona__email_verificado
        {
            get { return _persona__email_verificado; }
            set { _persona__email_verificado = value; NotifyPropertyChanged(); }
        }
        private bool? _persona__info_verificada;
        public bool? persona__info_verificada
        {
            get { return _persona__info_verificada; }
            set { _persona__info_verificada = value; NotifyPropertyChanged(); }
        }
        private string? _persona__descripcion_domicilio;
        public string? persona__descripcion_domicilio
        {
            get { return _persona__descripcion_domicilio; }
            set { _persona__descripcion_domicilio = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__id;
        public string? domicilio__id
        {
            get { return _domicilio__id; }
            set { _domicilio__id = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__calle;
        public string? domicilio__calle
        {
            get { return _domicilio__calle; }
            set { _domicilio__calle = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__entre;
        public string? domicilio__entre
        {
            get { return _domicilio__entre; }
            set { _domicilio__entre = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__numero;
        public string? domicilio__numero
        {
            get { return _domicilio__numero; }
            set { _domicilio__numero = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__piso;
        public string? domicilio__piso
        {
            get { return _domicilio__piso; }
            set { _domicilio__piso = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__departamento;
        public string? domicilio__departamento
        {
            get { return _domicilio__departamento; }
            set { _domicilio__departamento = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__barrio;
        public string? domicilio__barrio
        {
            get { return _domicilio__barrio; }
            set { _domicilio__barrio = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__localidad;
        public string? domicilio__localidad
        {
            get { return _domicilio__localidad; }
            set { _domicilio__localidad = value; NotifyPropertyChanged(); }
        }
    }
}