using System;
using System.ComponentModel;

namespace WpfAppMy.Model.Data
{
    public class Model_contralor : INotifyPropertyChanged
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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
