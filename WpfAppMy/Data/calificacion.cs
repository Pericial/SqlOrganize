using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_calificacion : INotifyPropertyChanged
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private decimal _nota1;
        public decimal nota1
        {
            get { return _nota1; }
            set { _nota1 = value; NotifyPropertyChanged(); }
        }
        private decimal _nota2;
        public decimal nota2
        {
            get { return _nota2; }
            set { _nota2 = value; NotifyPropertyChanged(); }
        }
        private decimal _nota3;
        public decimal nota3
        {
            get { return _nota3; }
            set { _nota3 = value; NotifyPropertyChanged(); }
        }
        private decimal _nota_final;
        public decimal nota_final
        {
            get { return _nota_final; }
            set { _nota_final = value; NotifyPropertyChanged(); }
        }
        private decimal _crec;
        public decimal crec
        {
            get { return _crec; }
            set { _crec = value; NotifyPropertyChanged(); }
        }
        private string _curso;
        public string curso
        {
            get { return _curso; }
            set { _curso = value; NotifyPropertyChanged(); }
        }
        private int _porcentaje_asistencia;
        public int porcentaje_asistencia
        {
            get { return _porcentaje_asistencia; }
            set { _porcentaje_asistencia = value; NotifyPropertyChanged(); }
        }
        private string _observaciones;
        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; NotifyPropertyChanged(); }
        }
        private string _division;
        public string division
        {
            get { return _division; }
            set { _division = value; NotifyPropertyChanged(); }
        }
        private string _alumno;
        public string alumno
        {
            get { return _alumno; }
            set { _alumno = value; NotifyPropertyChanged(); }
        }
        private string _disposicion;
        public string disposicion
        {
            get { return _disposicion; }
            set { _disposicion = value; NotifyPropertyChanged(); }
        }
        private DateTime _fecha;
        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; NotifyPropertyChanged(); }
        }
        private bool _archivado;
        public bool archivado
        {
            get { return _archivado; }
            set { _archivado = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
