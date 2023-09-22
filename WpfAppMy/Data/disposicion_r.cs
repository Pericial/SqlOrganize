using System;

namespace 
{
    public class Data_disposicion_r : Data_disposicion
    {
        private string? _asignatura__id;
        public string? asignatura__id
        {
            get { return _asignatura__id; }
            set { _asignatura__id = value; NotifyPropertyChanged(); }
        }
        private string? _asignatura__nombre;
        public string? asignatura__nombre
        {
            get { return _asignatura__nombre; }
            set { _asignatura__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _asignatura__formacion;
        public string? asignatura__formacion
        {
            get { return _asignatura__formacion; }
            set { _asignatura__formacion = value; NotifyPropertyChanged(); }
        }
        private string? _asignatura__clasificacion;
        public string? asignatura__clasificacion
        {
            get { return _asignatura__clasificacion; }
            set { _asignatura__clasificacion = value; NotifyPropertyChanged(); }
        }
        private string? _asignatura__codigo;
        public string? asignatura__codigo
        {
            get { return _asignatura__codigo; }
            set { _asignatura__codigo = value; NotifyPropertyChanged(); }
        }
        private string? _asignatura__perfil;
        public string? asignatura__perfil
        {
            get { return _asignatura__perfil; }
            set { _asignatura__perfil = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion__id;
        public string? planificacion__id
        {
            get { return _planificacion__id; }
            set { _planificacion__id = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion__anio;
        public string? planificacion__anio
        {
            get { return _planificacion__anio; }
            set { _planificacion__anio = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion__semestre;
        public string? planificacion__semestre
        {
            get { return _planificacion__semestre; }
            set { _planificacion__semestre = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion__plan;
        public string? planificacion__plan
        {
            get { return _planificacion__plan; }
            set { _planificacion__plan = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion__pfid;
        public string? planificacion__pfid
        {
            get { return _planificacion__pfid; }
            set { _planificacion__pfid = value; NotifyPropertyChanged(); }
        }
        private string? _plan__id;
        public string? plan__id
        {
            get { return _plan__id; }
            set { _plan__id = value; NotifyPropertyChanged(); }
        }
        private string? _plan__orientacion;
        public string? plan__orientacion
        {
            get { return _plan__orientacion; }
            set { _plan__orientacion = value; NotifyPropertyChanged(); }
        }
        private string? _plan__resolucion;
        public string? plan__resolucion
        {
            get { return _plan__resolucion; }
            set { _plan__resolucion = value; NotifyPropertyChanged(); }
        }
        private string? _plan__distribucion_horaria;
        public string? plan__distribucion_horaria
        {
            get { return _plan__distribucion_horaria; }
            set { _plan__distribucion_horaria = value; NotifyPropertyChanged(); }
        }
        private string? _plan__pfid;
        public string? plan__pfid
        {
            get { return _plan__pfid; }
            set { _plan__pfid = value; NotifyPropertyChanged(); }
        }
    }
}
