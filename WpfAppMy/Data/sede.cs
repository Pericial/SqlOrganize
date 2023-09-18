using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_sede : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string _numero;
        public string numero
        {
            get { return _numero; }
            set { _numero = value; NotifyPropertyChanged(); }
        }
        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; NotifyPropertyChanged(); }
        }
        private string _observaciones;
        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        private DateTime _alta;
        public DateTime alta
        {
            get { return _alta; }
            set { _alta = value; NotifyPropertyChanged(); }
        }
        private DateTime _baja;
        public DateTime baja
        {
            get { return _baja; }
            set { _baja = value; NotifyPropertyChanged(); }
        }
        private string _domicilio;
        public string domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; NotifyPropertyChanged(); }
        }
        private string _tipo_sede;
        public string tipo_sede
        {
            get { return _tipo_sede; }
            set { _tipo_sede = value; NotifyPropertyChanged(); }
        }
        private string _centro_educativo;
        public string centro_educativo
        {
            get { return _centro_educativo; }
            set { _centro_educativo = value; NotifyPropertyChanged(); }
        }
        private DateTime _fecha_traspaso;
        public DateTime fecha_traspaso
        {
            get { return _fecha_traspaso; }
            set { _fecha_traspaso = value; NotifyPropertyChanged(); }
        }
        private string _organizacion;
        public string organizacion
        {
            get { return _organizacion; }
            set { _organizacion = value; NotifyPropertyChanged(); }
        }
        private string _pfid;
        public string pfid
        {
            get { return _pfid; }
            set { _pfid = value; NotifyPropertyChanged(); }
        }
        private string _pfid_organizacion;
        public string pfid_organizacion
        {
            get { return _pfid_organizacion; }
            set { _pfid_organizacion = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
