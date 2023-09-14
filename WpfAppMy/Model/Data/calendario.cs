using System;
using System.ComponentModel;

namespace WpfAppMy.Model.Data
{
    public class Model_calendario : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private DateTime _inicio;
        public DateTime inicio
        {
            get { return _inicio; }
            set { _inicio = value; NotifyPropertyChanged(); }
        }
        private DateTime _fin;
        public DateTime fin
        {
            get { return _fin; }
            set { _fin = value; NotifyPropertyChanged(); }
        }
        private short _anio;
        public short anio
        {
            get { return _anio; }
            set { _anio = value; NotifyPropertyChanged(); }
        }
        private short _semestre;
        public short semestre
        {
            get { return _semestre; }
            set { _semestre = value; NotifyPropertyChanged(); }
        }
        private DateTime _insertado;
        public DateTime insertado
        {
            get { return _insertado; }
            set { _insertado = value; NotifyPropertyChanged(); }
        }
        private string _descripcion;
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
