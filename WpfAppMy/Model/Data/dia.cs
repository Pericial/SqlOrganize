using System;
using System.ComponentModel;

namespace WpfAppMy.Model.Data
{
    public class Model_dia : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private short _numero;
        public short numero
        {
            get { return _numero; }
            set { _numero = value; NotifyPropertyChanged(); }
        }
        private string _dia;
        public string dia
        {
            get { return _dia; }
            set { _dia = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
