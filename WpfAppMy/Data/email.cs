using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_email : INotifyPropertyChanged
    {

        public string? label { get; set; }
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _email;
        public string? email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged(); }
        }
        private bool? _verificado;
        public bool? verificado
        {
            get { return _verificado; }
            set { _verificado = value; NotifyPropertyChanged(); }
        }
        private DateTime? _insertado;
        public DateTime? insertado
        {
            get { return _insertado; }
            set { _insertado = value; NotifyPropertyChanged(); }
        }
        private DateTime? _eliminado;
        public DateTime? eliminado
        {
            get { return _eliminado; }
            set { _eliminado = value; NotifyPropertyChanged(); }
        }
        private string? _persona;
        public string? persona
        {
            get { return _persona; }
            set { _persona = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
