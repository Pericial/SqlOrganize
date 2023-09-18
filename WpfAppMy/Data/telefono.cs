using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_telefono : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string _tipo;
        public string tipo
        {
            get { return _tipo; }
            set { _tipo = value; NotifyPropertyChanged(); }
        }
        private string _prefijo;
        public string prefijo
        {
            get { return _prefijo; }
            set { _prefijo = value; NotifyPropertyChanged(); }
        }
        private string _numero;
        public string numero
        {
            get { return _numero; }
            set { _numero = value; NotifyPropertyChanged(); }
        }
        private DateTime _insertado;
        public DateTime insertado
        {
            get { return _insertado; }
            set { _insertado = value; NotifyPropertyChanged(); }
        }
        private DateTime _eliminado;
        public DateTime eliminado
        {
            get { return _eliminado; }
            set { _eliminado = value; NotifyPropertyChanged(); }
        }
        private string _persona;
        public string persona
        {
            get { return _persona; }
            set { _persona = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
