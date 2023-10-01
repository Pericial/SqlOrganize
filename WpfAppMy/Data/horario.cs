using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_horario : INotifyPropertyChanged
    {

        public string? label { get; set; }
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private DateTime? _hora_inicio;
        public DateTime? hora_inicio
        {
            get { return _hora_inicio; }
            set { _hora_inicio = value; NotifyPropertyChanged(); }
        }
        private DateTime? _hora_fin;
        public DateTime? hora_fin
        {
            get { return _hora_fin; }
            set { _hora_fin = value; NotifyPropertyChanged(); }
        }
        private string? _curso;
        public string? curso
        {
            get { return _curso; }
            set { _curso = value; NotifyPropertyChanged(); }
        }
        private string? _dia;
        public string? dia
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
