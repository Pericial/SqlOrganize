using System;

namespace WpfAppMy.Data
{
    public class Data_horario_r : Data_horario
    {
        private string? _curso__id;
        public string? curso__id
        {
            get { return _curso__id; }
            set { _curso__id = value; NotifyPropertyChanged(); }
        }
        private int? _curso__horas_catedra;
        public int? curso__horas_catedra
        {
            get { return _curso__horas_catedra; }
            set { _curso__horas_catedra = value; NotifyPropertyChanged(); }
        }
        private string? _curso__ige;
        public string? curso__ige
        {
            get { return _curso__ige; }
            set { _curso__ige = value; NotifyPropertyChanged(); }
        }
        private string? _curso__comision;
        public string? curso__comision
        {
            get { return _curso__comision; }
            set { _curso__comision = value; NotifyPropertyChanged(); }
        }
        private string? _curso__asignatura;
        public string? curso__asignatura
        {
            get { return _curso__asignatura; }
            set { _curso__asignatura = value; NotifyPropertyChanged(); }
        }
        private DateTime? _curso__alta;
        public DateTime? curso__alta
        {
            get { return _curso__alta; }
            set { _curso__alta = value; NotifyPropertyChanged(); }
        }
        private string? _curso__descripcion_horario;
        public string? curso__descripcion_horario
        {
            get { return _curso__descripcion_horario; }
            set { _curso__descripcion_horario = value; NotifyPropertyChanged(); }
        }
        private string? _comision__id;
        public string? comision__id
        {
            get { return _comision__id; }
            set { _comision__id = value; NotifyPropertyChanged(); }
        }
        private string? _comision__turno;
        public string? comision__turno
        {
            get { return _comision__turno; }
            set { _comision__turno = value; NotifyPropertyChanged(); }
        }
        private string? _comision__division;
        public string? comision__division
        {
            get { return _comision__division; }
            set { _comision__division = value; NotifyPropertyChanged(); }
        }
        private string? _comision__comentario;
        public string? comision__comentario
        {
            get { return _comision__comentario; }
            set { _comision__comentario = value; NotifyPropertyChanged(); }
        }
        private bool? _comision__autorizada;
        public bool? comision__autorizada
        {
            get { return _comision__autorizada; }
            set { _comision__autorizada = value; NotifyPropertyChanged(); }
        }
        private bool? _comision__apertura;
        public bool? comision__apertura
        {
            get { return _comision__apertura; }
            set { _comision__apertura = value; NotifyPropertyChanged(); }
        }
        private bool? _comision__publicada;
        public bool? comision__publicada
        {
            get { return _comision__publicada; }
            set { _comision__publicada = value; NotifyPropertyChanged(); }
        }
        private string? _comision__observaciones;
        public string? comision__observaciones
        {
            get { return _comision__observaciones; }
            set { _comision__observaciones = value; NotifyPropertyChanged(); }
        }
        private DateTime? _comision__alta;
        public DateTime? comision__alta
        {
            get { return _comision__alta; }
            set { _comision__alta = value; NotifyPropertyChanged(); }
        }
        private string? _comision__sede;
        public string? comision__sede
        {
            get { return _comision__sede; }
            set { _comision__sede = value; NotifyPropertyChanged(); }
        }
        private string? _comision__modalidad;
        public string? comision__modalidad
        {
            get { return _comision__modalidad; }
            set { _comision__modalidad = value; NotifyPropertyChanged(); }
        }
        private string? _comision__planificacion;
        public string? comision__planificacion
        {
            get { return _comision__planificacion; }
            set { _comision__planificacion = value; NotifyPropertyChanged(); }
        }
        private string? _comision__comision_siguiente;
        public string? comision__comision_siguiente
        {
            get { return _comision__comision_siguiente; }
            set { _comision__comision_siguiente = value; NotifyPropertyChanged(); }
        }
        private string? _comision__calendario;
        public string? comision__calendario
        {
            get { return _comision__calendario; }
            set { _comision__calendario = value; NotifyPropertyChanged(); }
        }
        private string? _comision__identificacion;
        public string? comision__identificacion
        {
            get { return _comision__identificacion; }
            set { _comision__identificacion = value; NotifyPropertyChanged(); }
        }
        private string? _comision__estado;
        public string? comision__estado
        {
            get { return _comision__estado; }
            set { _comision__estado = value; NotifyPropertyChanged(); }
        }
        private string? _comision__configuracion;
        public string? comision__configuracion
        {
            get { return _comision__configuracion; }
            set { _comision__configuracion = value; NotifyPropertyChanged(); }
        }
        private string? _comision__pfid;
        public string? comision__pfid
        {
            get { return _comision__pfid; }
            set { _comision__pfid = value; NotifyPropertyChanged(); }
        }
        private string? _sede__id;
        public string? sede__id
        {
            get { return _sede__id; }
            set { _sede__id = value; NotifyPropertyChanged(); }
        }
        private string? _sede__numero;
        public string? sede__numero
        {
            get { return _sede__numero; }
            set { _sede__numero = value; NotifyPropertyChanged(); }
        }
        private string? _sede__nombre;
        public string? sede__nombre
        {
            get { return _sede__nombre; }
            set { _sede__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _sede__observaciones;
        public string? sede__observaciones
        {
            get { return _sede__observaciones; }
            set { _sede__observaciones = value; NotifyPropertyChanged(); }
        }
        private DateTime? _sede__alta;
        public DateTime? sede__alta
        {
            get { return _sede__alta; }
            set { _sede__alta = value; NotifyPropertyChanged(); }
        }
        private DateTime? _sede__baja;
        public DateTime? sede__baja
        {
            get { return _sede__baja; }
            set { _sede__baja = value; NotifyPropertyChanged(); }
        }
        private string? _sede__domicilio;
        public string? sede__domicilio
        {
            get { return _sede__domicilio; }
            set { _sede__domicilio = value; NotifyPropertyChanged(); }
        }
        private string? _sede__tipo_sede;
        public string? sede__tipo_sede
        {
            get { return _sede__tipo_sede; }
            set { _sede__tipo_sede = value; NotifyPropertyChanged(); }
        }
        private string? _sede__centro_educativo;
        public string? sede__centro_educativo
        {
            get { return _sede__centro_educativo; }
            set { _sede__centro_educativo = value; NotifyPropertyChanged(); }
        }
        private DateTime? _sede__fecha_traspaso;
        public DateTime? sede__fecha_traspaso
        {
            get { return _sede__fecha_traspaso; }
            set { _sede__fecha_traspaso = value; NotifyPropertyChanged(); }
        }
        private string? _sede__organizacion;
        public string? sede__organizacion
        {
            get { return _sede__organizacion; }
            set { _sede__organizacion = value; NotifyPropertyChanged(); }
        }
        private string? _sede__pfid;
        public string? sede__pfid
        {
            get { return _sede__pfid; }
            set { _sede__pfid = value; NotifyPropertyChanged(); }
        }
        private string? _sede__pfid_organizacion;
        public string? sede__pfid_organizacion
        {
            get { return _sede__pfid_organizacion; }
            set { _sede__pfid_organizacion = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__id;
        public string? domicilio__id
        {
            get { return _domicilio__id; }
            set { _domicilio__id = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__calle;
        public string? domicilio__calle
        {
            get { return _domicilio__calle; }
            set { _domicilio__calle = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__entre;
        public string? domicilio__entre
        {
            get { return _domicilio__entre; }
            set { _domicilio__entre = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__numero;
        public string? domicilio__numero
        {
            get { return _domicilio__numero; }
            set { _domicilio__numero = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__piso;
        public string? domicilio__piso
        {
            get { return _domicilio__piso; }
            set { _domicilio__piso = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__departamento;
        public string? domicilio__departamento
        {
            get { return _domicilio__departamento; }
            set { _domicilio__departamento = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__barrio;
        public string? domicilio__barrio
        {
            get { return _domicilio__barrio; }
            set { _domicilio__barrio = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio__localidad;
        public string? domicilio__localidad
        {
            get { return _domicilio__localidad; }
            set { _domicilio__localidad = value; NotifyPropertyChanged(); }
        }
        private string? _tipo_sede__id;
        public string? tipo_sede__id
        {
            get { return _tipo_sede__id; }
            set { _tipo_sede__id = value; NotifyPropertyChanged(); }
        }
        private string? _tipo_sede__descripcion;
        public string? tipo_sede__descripcion
        {
            get { return _tipo_sede__descripcion; }
            set { _tipo_sede__descripcion = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo__id;
        public string? centro_educativo__id
        {
            get { return _centro_educativo__id; }
            set { _centro_educativo__id = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo__nombre;
        public string? centro_educativo__nombre
        {
            get { return _centro_educativo__nombre; }
            set { _centro_educativo__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo__cue;
        public string? centro_educativo__cue
        {
            get { return _centro_educativo__cue; }
            set { _centro_educativo__cue = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo__domicilio;
        public string? centro_educativo__domicilio
        {
            get { return _centro_educativo__domicilio; }
            set { _centro_educativo__domicilio = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo__observaciones;
        public string? centro_educativo__observaciones
        {
            get { return _centro_educativo__observaciones; }
            set { _centro_educativo__observaciones = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen__id;
        public string? domicilio_cen__id
        {
            get { return _domicilio_cen__id; }
            set { _domicilio_cen__id = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen__calle;
        public string? domicilio_cen__calle
        {
            get { return _domicilio_cen__calle; }
            set { _domicilio_cen__calle = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen__entre;
        public string? domicilio_cen__entre
        {
            get { return _domicilio_cen__entre; }
            set { _domicilio_cen__entre = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen__numero;
        public string? domicilio_cen__numero
        {
            get { return _domicilio_cen__numero; }
            set { _domicilio_cen__numero = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen__piso;
        public string? domicilio_cen__piso
        {
            get { return _domicilio_cen__piso; }
            set { _domicilio_cen__piso = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen__departamento;
        public string? domicilio_cen__departamento
        {
            get { return _domicilio_cen__departamento; }
            set { _domicilio_cen__departamento = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen__barrio;
        public string? domicilio_cen__barrio
        {
            get { return _domicilio_cen__barrio; }
            set { _domicilio_cen__barrio = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen__localidad;
        public string? domicilio_cen__localidad
        {
            get { return _domicilio_cen__localidad; }
            set { _domicilio_cen__localidad = value; NotifyPropertyChanged(); }
        }
        private string? _modalidad__id;
        public string? modalidad__id
        {
            get { return _modalidad__id; }
            set { _modalidad__id = value; NotifyPropertyChanged(); }
        }
        private string? _modalidad__nombre;
        public string? modalidad__nombre
        {
            get { return _modalidad__nombre; }
            set { _modalidad__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _modalidad__pfid;
        public string? modalidad__pfid
        {
            get { return _modalidad__pfid; }
            set { _modalidad__pfid = value; NotifyPropertyChanged(); }
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
        private string? _calendario__id;
        public string? calendario__id
        {
            get { return _calendario__id; }
            set { _calendario__id = value; NotifyPropertyChanged(); }
        }
        private DateTime? _calendario__inicio;
        public DateTime? calendario__inicio
        {
            get { return _calendario__inicio; }
            set { _calendario__inicio = value; NotifyPropertyChanged(); }
        }
        private DateTime? _calendario__fin;
        public DateTime? calendario__fin
        {
            get { return _calendario__fin; }
            set { _calendario__fin = value; NotifyPropertyChanged(); }
        }
        private short? _calendario__anio;
        public short? calendario__anio
        {
            get { return _calendario__anio; }
            set { _calendario__anio = value; NotifyPropertyChanged(); }
        }
        private short? _calendario__semestre;
        public short? calendario__semestre
        {
            get { return _calendario__semestre; }
            set { _calendario__semestre = value; NotifyPropertyChanged(); }
        }
        private DateTime? _calendario__insertado;
        public DateTime? calendario__insertado
        {
            get { return _calendario__insertado; }
            set { _calendario__insertado = value; NotifyPropertyChanged(); }
        }
        private string? _calendario__descripcion;
        public string? calendario__descripcion
        {
            get { return _calendario__descripcion; }
            set { _calendario__descripcion = value; NotifyPropertyChanged(); }
        }
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
        private string? _dia__id;
        public string? dia__id
        {
            get { return _dia__id; }
            set { _dia__id = value; NotifyPropertyChanged(); }
        }
        private short? _dia__numero;
        public short? dia__numero
        {
            get { return _dia__numero; }
            set { _dia__numero = value; NotifyPropertyChanged(); }
        }
        private string? _dia__dia;
        public string? dia__dia
        {
            get { return _dia__dia; }
            set { _dia__dia = value; NotifyPropertyChanged(); }
        }
    }
}