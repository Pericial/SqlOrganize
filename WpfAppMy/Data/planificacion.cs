using System;
using System.ComponentModel;

namespace 
{
    public class Data_planificacion : INotifyPropertyChanged
    {
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _anio;
        public string? anio
        {
            get { return _anio; }
            set { _anio = value; NotifyPropertyChanged(); }
        }
        private string? _semestre;
        public string? semestre
        {
            get { return _semestre; }
            set { _semestre = value; NotifyPropertyChanged(); }
        }
        private string? _plan;
        public string? plan
        {
            get { return _plan; }
            set { _plan = value; NotifyPropertyChanged(); }
        }
        private string? _pfid;
        public string? pfid
        {
            get { return _pfid; }
            set { _pfid = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
