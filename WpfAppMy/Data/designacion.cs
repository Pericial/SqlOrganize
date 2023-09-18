using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_designacion : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private DateTime _desde;
        public DateTime desde
        {
            get { return _desde; }
            set { _desde = value; NotifyPropertyChanged(); }
        }
        private DateTime _hasta;
        public DateTime hasta
        {
            get { return _hasta; }
            set { _hasta = value; NotifyPropertyChanged(); }
        }
        private string _cargo;
        public string cargo
        {
            get { return _cargo; }
            set { _cargo = value; NotifyPropertyChanged(); }
        }
        private string _sede;
        public string sede
        {
            get { return _sede; }
            set { _sede = value; NotifyPropertyChanged(); }
        }
        private string _persona;
        public string persona
        {
            get { return _persona; }
            set { _persona = value; NotifyPropertyChanged(); }
        }
        private DateTime _alta;
        public DateTime alta
        {
            get { return _alta; }
            set { _alta = value; NotifyPropertyChanged(); }
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
