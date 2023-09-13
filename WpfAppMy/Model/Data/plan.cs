using System;
using System.ComponentModel;

namespace WpfAppMy.Model.Data
{
    public class Model_plan : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string _orientacion;
        public string orientacion
        {
            get { return _orientacion; }
            set { _orientacion = value; NotifyPropertyChanged(); }
        }
        private string _resolucion;
        public string resolucion
        {
            get { return _resolucion; }
            set { _resolucion = value; NotifyPropertyChanged(); }
        }
        private string _distribucion_horaria;
        public string distribucion_horaria
        {
            get { return _distribucion_horaria; }
            set { _distribucion_horaria = value; NotifyPropertyChanged(); }
        }
        private string _pfid;
        public string pfid
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
