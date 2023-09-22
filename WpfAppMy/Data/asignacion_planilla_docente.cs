using System;
using System.ComponentModel;

namespace 
{
    public class Data_asignacion_planilla_docente : INotifyPropertyChanged
    {
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _planilla_docente;
        public string? planilla_docente
        {
            get { return _planilla_docente; }
            set { _planilla_docente = value; NotifyPropertyChanged(); }
        }
        private string? _toma;
        public string? toma
        {
            get { return _toma; }
            set { _toma = value; NotifyPropertyChanged(); }
        }
        private DateTime? _insertado;
        public DateTime? insertado
        {
            get { return _insertado; }
            set { _insertado = value; NotifyPropertyChanged(); }
        }
        private string? _comentario;
        public string? comentario
        {
            get { return _comentario; }
            set { _comentario = value; NotifyPropertyChanged(); }
        }
        private bool? _reclamo;
        public bool? reclamo
        {
            get { return _reclamo; }
            set { _reclamo = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
