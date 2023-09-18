using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_contralor_rel : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private DateTime _fecha_contralor;
        public DateTime fecha_contralor
        {
            get { return _fecha_contralor; }
            set { _fecha_contralor = value; NotifyPropertyChanged(); }
        }
        private DateTime _fecha_consejo;
        public DateTime fecha_consejo
        {
            get { return _fecha_consejo; }
            set { _fecha_consejo = value; NotifyPropertyChanged(); }
        }
        private DateTime _insertado;
        public DateTime insertado
        {
            get { return _insertado; }
            set { _insertado = value; NotifyPropertyChanged(); }
        }
        private string _planilla_docente;
        public string planilla_docente
        {
            get { return _planilla_docente; }
            set { _planilla_docente = value; NotifyPropertyChanged(); }
        }
        private string _planilla_docente__id;
        public string planilla_docente__id
        {
            get { return _planilla_docente__id; }
            set { _planilla_docente__id = value; NotifyPropertyChanged(); }
        }
        private string _planilla_docente__numero;
        public string planilla_docente__numero
        {
            get { return _planilla_docente__numero; }
            set { _planilla_docente__numero = value; NotifyPropertyChanged(); }
        }
        private DateTime _planilla_docente__insertado;
        public DateTime planilla_docente__insertado
        {
            get { return _planilla_docente__insertado; }
            set { _planilla_docente__insertado = value; NotifyPropertyChanged(); }
        }
        private DateTime _planilla_docente__fecha_contralor;
        public DateTime planilla_docente__fecha_contralor
        {
            get { return _planilla_docente__fecha_contralor; }
            set { _planilla_docente__fecha_contralor = value; NotifyPropertyChanged(); }
        }
        private DateTime _planilla_docente__fecha_consejo;
        public DateTime planilla_docente__fecha_consejo
        {
            get { return _planilla_docente__fecha_consejo; }
            set { _planilla_docente__fecha_consejo = value; NotifyPropertyChanged(); }
        }
        private string _planilla_docente__observaciones;
        public string planilla_docente__observaciones
        {
            get { return _planilla_docente__observaciones; }
            set { _planilla_docente__observaciones = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
