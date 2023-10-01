using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_alumno_comision : INotifyPropertyChanged
    {

        public string? label { get; set; }
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private DateTime? _creado;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        private string? _observaciones;
        public string? observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        private string? _comision;
        public string? comision
        {
            get { return _comision; }
            set { _comision = value; NotifyPropertyChanged(); }
        }
        private string? _alumno;
        public string? alumno
        {
            get { return _alumno; }
            set { _alumno = value; NotifyPropertyChanged(); }
        }
        private string? _estado;
        public string? estado
        {
            get { return _estado; }
            set { _estado = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
