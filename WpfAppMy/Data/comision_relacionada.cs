using System;
using System.ComponentModel;

namespace 
{
    public class Data_comision_relacionada : INotifyPropertyChanged
    {
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _comision;
        public string? comision
        {
            get { return _comision; }
            set { _comision = value; NotifyPropertyChanged(); }
        }
        private string? _relacion;
        public string? relacion
        {
            get { return _relacion; }
            set { _relacion = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
