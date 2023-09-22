using System;
using System.ComponentModel;

namespace 
{
    public class Data_planilla_docente : INotifyPropertyChanged
    {
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _numero;
        public string? numero
        {
            get { return _numero; }
            set { _numero = value; NotifyPropertyChanged(); }
        }
        private DateTime? _insertado;
        public DateTime? insertado
        {
            get { return _insertado; }
            set { _insertado = value; NotifyPropertyChanged(); }
        }
        private DateTime? _fecha_contralor;
        public DateTime? fecha_contralor
        {
            get { return _fecha_contralor; }
            set { _fecha_contralor = value; NotifyPropertyChanged(); }
        }
        private DateTime? _fecha_consejo;
        public DateTime? fecha_consejo
        {
            get { return _fecha_consejo; }
            set { _fecha_consejo = value; NotifyPropertyChanged(); }
        }
        private string? _observaciones;
        public string? observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
