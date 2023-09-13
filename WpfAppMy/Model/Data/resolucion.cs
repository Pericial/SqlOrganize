using System;
using System.ComponentModel;

namespace WpfAppMy.Model.Data
{
    public class Model_resolucion : INotifyPropertyChanged
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
        private DateTime _anio;
        public DateTime anio
        {
            get { return _anio; }
            set { _anio = value; NotifyPropertyChanged(); }
        }
        private string _tipo;
        public string tipo
        {
            get { return _tipo; }
            set { _tipo = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
