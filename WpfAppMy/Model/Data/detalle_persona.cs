using System;
using System.ComponentModel;

namespace WpfAppMy.Model.Data
{
    public class Model_detalle_persona : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string _descripcion;
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; NotifyPropertyChanged(); }
        }
        private string _archivo;
        public string archivo
        {
            get { return _archivo; }
            set { _archivo = value; NotifyPropertyChanged(); }
        }
        private DateTime _creado;
        public DateTime creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        private string _persona;
        public string persona
        {
            get { return _persona; }
            set { _persona = value; NotifyPropertyChanged(); }
        }
        private DateTime _fecha;
        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; NotifyPropertyChanged(); }
        }
        private string _tipo;
        public string tipo
        {
            get { return _tipo; }
            set { _tipo = value; NotifyPropertyChanged(); }
        }
        private string _asunto;
        public string asunto
        {
            get { return _asunto; }
            set { _asunto = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
