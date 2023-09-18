using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_toma : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private DateTime _fecha_toma;
        public DateTime fecha_toma
        {
            get { return _fecha_toma; }
            set { _fecha_toma = value; NotifyPropertyChanged(); }
        }
        private string _estado;
        public string estado
        {
            get { return _estado; }
            set { _estado = value; NotifyPropertyChanged(); }
        }
        private string _observaciones;
        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        private string _comentario;
        public string comentario
        {
            get { return _comentario; }
            set { _comentario = value; NotifyPropertyChanged(); }
        }
        private string _tipo_movimiento;
        public string tipo_movimiento
        {
            get { return _tipo_movimiento; }
            set { _tipo_movimiento = value; NotifyPropertyChanged(); }
        }
        private string _estado_contralor;
        public string estado_contralor
        {
            get { return _estado_contralor; }
            set { _estado_contralor = value; NotifyPropertyChanged(); }
        }
        private DateTime _alta;
        public DateTime alta
        {
            get { return _alta; }
            set { _alta = value; NotifyPropertyChanged(); }
        }
        private string _curso;
        public string curso
        {
            get { return _curso; }
            set { _curso = value; NotifyPropertyChanged(); }
        }
        private string _docente;
        public string docente
        {
            get { return _docente; }
            set { _docente = value; NotifyPropertyChanged(); }
        }
        private string _reemplazo;
        public string reemplazo
        {
            get { return _reemplazo; }
            set { _reemplazo = value; NotifyPropertyChanged(); }
        }
        private string _planilla_docente;
        public string planilla_docente
        {
            get { return _planilla_docente; }
            set { _planilla_docente = value; NotifyPropertyChanged(); }
        }
        private bool _calificacion;
        public bool calificacion
        {
            get { return _calificacion; }
            set { _calificacion = value; NotifyPropertyChanged(); }
        }
        private bool _temas_tratados;
        public bool temas_tratados
        {
            get { return _temas_tratados; }
            set { _temas_tratados = value; NotifyPropertyChanged(); }
        }
        private bool _asistencia;
        public bool asistencia
        {
            get { return _asistencia; }
            set { _asistencia = value; NotifyPropertyChanged(); }
        }
        private bool _sin_planillas;
        public bool sin_planillas
        {
            get { return _sin_planillas; }
            set { _sin_planillas = value; NotifyPropertyChanged(); }
        }
        private bool _confirmada;
        public bool confirmada
        {
            get { return _confirmada; }
            set { _confirmada = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
