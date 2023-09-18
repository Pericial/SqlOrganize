using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_distribucion_horaria : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private int _horas_catedra;
        public int horas_catedra
        {
            get { return _horas_catedra; }
            set { _horas_catedra = value; NotifyPropertyChanged(); }
        }
        private int _dia;
        public int dia
        {
            get { return _dia; }
            set { _dia = value; NotifyPropertyChanged(); }
        }
        private string _disposicion;
        public string disposicion
        {
            get { return _disposicion; }
            set { _disposicion = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
