using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_asignatura : INotifyPropertyChanged
    {
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _nombre;
        public string? nombre
        {
            get { return _nombre; }
            set { _nombre = value; NotifyPropertyChanged(); }
        }
        private string? _formacion;
        public string? formacion
        {
            get { return _formacion; }
            set { _formacion = value; NotifyPropertyChanged(); }
        }
        private string? _clasificacion;
        public string? clasificacion
        {
            get { return _clasificacion; }
            set { _clasificacion = value; NotifyPropertyChanged(); }
        }
        private string? _codigo;
        public string? codigo
        {
            get { return _codigo; }
            set { _codigo = value; NotifyPropertyChanged(); }
        }
        private string? _perfil;
        public string? perfil
        {
            get { return _perfil; }
            set { _perfil = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
