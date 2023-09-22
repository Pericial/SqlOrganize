using System;
using System.ComponentModel;

namespace 
{
    public class Data_domicilio : INotifyPropertyChanged
    {
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _calle;
        public string? calle
        {
            get { return _calle; }
            set { _calle = value; NotifyPropertyChanged(); }
        }
        private string? _entre;
        public string? entre
        {
            get { return _entre; }
            set { _entre = value; NotifyPropertyChanged(); }
        }
        private string? _numero;
        public string? numero
        {
            get { return _numero; }
            set { _numero = value; NotifyPropertyChanged(); }
        }
        private string? _piso;
        public string? piso
        {
            get { return _piso; }
            set { _piso = value; NotifyPropertyChanged(); }
        }
        private string? _departamento;
        public string? departamento
        {
            get { return _departamento; }
            set { _departamento = value; NotifyPropertyChanged(); }
        }
        private string? _barrio;
        public string? barrio
        {
            get { return _barrio; }
            set { _barrio = value; NotifyPropertyChanged(); }
        }
        private string? _localidad;
        public string? localidad
        {
            get { return _localidad; }
            set { _localidad = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
