using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_disposicion : INotifyPropertyChanged
    {

        public string? label { get; set; }
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _asignatura;
        public string? asignatura
        {
            get { return _asignatura; }
            set { _asignatura = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion;
        public string? planificacion
        {
            get { return _planificacion; }
            set { _planificacion = value; NotifyPropertyChanged(); }
        }
        private int? _orden_informe_coordinacion_distrital;
        public int? orden_informe_coordinacion_distrital
        {
            get { return _orden_informe_coordinacion_distrital; }
            set { _orden_informe_coordinacion_distrital = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
