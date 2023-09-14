using System;
using System.ComponentModel;

namespace WpfAppMy.Model.Data
{
    public class Model_persona : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string _nombres;
        public string nombres
        {
            get { return _nombres; }
            set { _nombres = value; NotifyPropertyChanged(); }
        }
        private string _apellidos;
        public string apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; NotifyPropertyChanged(); }
        }
        private DateTime _fecha_nacimiento;
        public DateTime fecha_nacimiento
        {
            get { return _fecha_nacimiento; }
            set { _fecha_nacimiento = value; NotifyPropertyChanged(); }
        }
        private string _numero_documento;
        public string numero_documento
        {
            get { return _numero_documento; }
            set { _numero_documento = value; NotifyPropertyChanged(); }
        }
        private string _cuil;
        public string cuil
        {
            get { return _cuil; }
            set { _cuil = value; NotifyPropertyChanged(); }
        }
        private string _genero;
        public string genero
        {
            get { return _genero; }
            set { _genero = value; NotifyPropertyChanged(); }
        }
        private string _apodo;
        public string apodo
        {
            get { return _apodo; }
            set { _apodo = value; NotifyPropertyChanged(); }
        }
        private string _telefono;
        public string telefono
        {
            get { return _telefono; }
            set { _telefono = value; NotifyPropertyChanged(); }
        }
        private string _email;
        public string email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged(); }
        }
        private string _email_abc;
        public string email_abc
        {
            get { return _email_abc; }
            set { _email_abc = value; NotifyPropertyChanged(); }
        }
        private DateTime _alta;
        public DateTime alta
        {
            get { return _alta; }
            set { _alta = value; NotifyPropertyChanged(); }
        }
        private string _domicilio;
        public string domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; NotifyPropertyChanged(); }
        }
        private string _lugar_nacimiento;
        public string lugar_nacimiento
        {
            get { return _lugar_nacimiento; }
            set { _lugar_nacimiento = value; NotifyPropertyChanged(); }
        }
        private bool _telefono_verificado;
        public bool telefono_verificado
        {
            get { return _telefono_verificado; }
            set { _telefono_verificado = value; NotifyPropertyChanged(); }
        }
        private bool _email_verificado;
        public bool email_verificado
        {
            get { return _email_verificado; }
            set { _email_verificado = value; NotifyPropertyChanged(); }
        }
        private bool _info_verificada;
        public bool info_verificada
        {
            get { return _info_verificada; }
            set { _info_verificada = value; NotifyPropertyChanged(); }
        }
        private string _descripcion_domicilio;
        public string descripcion_domicilio
        {
            get { return _descripcion_domicilio; }
            set { _descripcion_domicilio = value; NotifyPropertyChanged(); }
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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
