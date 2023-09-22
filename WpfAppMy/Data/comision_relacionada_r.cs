using System;

namespace WpfAppMy.Data
{
    public class Data_comision_relacionada_r : Data_comision_relacionada
    {
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
        private string? _relacion__id;
        public string? relacion__id
        {
            get { return _relacion__id; }
            set { _relacion__id = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__turno;
        public string? relacion__turno
        {
            get { return _relacion__turno; }
            set { _relacion__turno = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__division;
        public string? relacion__division
        {
            get { return _relacion__division; }
            set { _relacion__division = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__comentario;
        public string? relacion__comentario
        {
            get { return _relacion__comentario; }
            set { _relacion__comentario = value; NotifyPropertyChanged(); }
        }
        private bool? _relacion__autorizada;
        public bool? relacion__autorizada
        {
            get { return _relacion__autorizada; }
            set { _relacion__autorizada = value; NotifyPropertyChanged(); }
        }
        private bool? _relacion__apertura;
        public bool? relacion__apertura
        {
            get { return _relacion__apertura; }
            set { _relacion__apertura = value; NotifyPropertyChanged(); }
        }
        private bool? _relacion__publicada;
        public bool? relacion__publicada
        {
            get { return _relacion__publicada; }
            set { _relacion__publicada = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__observaciones;
        public string? relacion__observaciones
        {
            get { return _relacion__observaciones; }
            set { _relacion__observaciones = value; NotifyPropertyChanged(); }
        }
        private DateTime? _relacion__alta;
        public DateTime? relacion__alta
        {
            get { return _relacion__alta; }
            set { _relacion__alta = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__sede;
        public string? relacion__sede
        {
            get { return _relacion__sede; }
            set { _relacion__sede = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__modalidad;
        public string? relacion__modalidad
        {
            get { return _relacion__modalidad; }
            set { _relacion__modalidad = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__planificacion;
        public string? relacion__planificacion
        {
            get { return _relacion__planificacion; }
            set { _relacion__planificacion = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__comision_siguiente;
        public string? relacion__comision_siguiente
        {
            get { return _relacion__comision_siguiente; }
            set { _relacion__comision_siguiente = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__calendario;
        public string? relacion__calendario
        {
            get { return _relacion__calendario; }
            set { _relacion__calendario = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__identificacion;
        public string? relacion__identificacion
        {
            get { return _relacion__identificacion; }
            set { _relacion__identificacion = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__estado;
        public string? relacion__estado
        {
            get { return _relacion__estado; }
            set { _relacion__estado = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__configuracion;
        public string? relacion__configuracion
        {
            get { return _relacion__configuracion; }
            set { _relacion__configuracion = value; NotifyPropertyChanged(); }
        }
        private string? _relacion__pfid;
        public string? relacion__pfid
        {
            get { return _relacion__pfid; }
            set { _relacion__pfid = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__id;
        public string? sede_rel__id
        {
            get { return _sede_rel__id; }
            set { _sede_rel__id = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__numero;
        public string? sede_rel__numero
        {
            get { return _sede_rel__numero; }
            set { _sede_rel__numero = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__nombre;
        public string? sede_rel__nombre
        {
            get { return _sede_rel__nombre; }
            set { _sede_rel__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__observaciones;
        public string? sede_rel__observaciones
        {
            get { return _sede_rel__observaciones; }
            set { _sede_rel__observaciones = value; NotifyPropertyChanged(); }
        }
        private DateTime? _sede_rel__alta;
        public DateTime? sede_rel__alta
        {
            get { return _sede_rel__alta; }
            set { _sede_rel__alta = value; NotifyPropertyChanged(); }
        }
        private DateTime? _sede_rel__baja;
        public DateTime? sede_rel__baja
        {
            get { return _sede_rel__baja; }
            set { _sede_rel__baja = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__domicilio;
        public string? sede_rel__domicilio
        {
            get { return _sede_rel__domicilio; }
            set { _sede_rel__domicilio = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__tipo_sede;
        public string? sede_rel__tipo_sede
        {
            get { return _sede_rel__tipo_sede; }
            set { _sede_rel__tipo_sede = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__centro_educativo;
        public string? sede_rel__centro_educativo
        {
            get { return _sede_rel__centro_educativo; }
            set { _sede_rel__centro_educativo = value; NotifyPropertyChanged(); }
        }
        private DateTime? _sede_rel__fecha_traspaso;
        public DateTime? sede_rel__fecha_traspaso
        {
            get { return _sede_rel__fecha_traspaso; }
            set { _sede_rel__fecha_traspaso = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__organizacion;
        public string? sede_rel__organizacion
        {
            get { return _sede_rel__organizacion; }
            set { _sede_rel__organizacion = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__pfid;
        public string? sede_rel__pfid
        {
            get { return _sede_rel__pfid; }
            set { _sede_rel__pfid = value; NotifyPropertyChanged(); }
        }
        private string? _sede_rel__pfid_organizacion;
        public string? sede_rel__pfid_organizacion
        {
            get { return _sede_rel__pfid_organizacion; }
            set { _sede_rel__pfid_organizacion = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_sed__id;
        public string? domicilio_sed__id
        {
            get { return _domicilio_sed__id; }
            set { _domicilio_sed__id = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_sed__calle;
        public string? domicilio_sed__calle
        {
            get { return _domicilio_sed__calle; }
            set { _domicilio_sed__calle = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_sed__entre;
        public string? domicilio_sed__entre
        {
            get { return _domicilio_sed__entre; }
            set { _domicilio_sed__entre = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_sed__numero;
        public string? domicilio_sed__numero
        {
            get { return _domicilio_sed__numero; }
            set { _domicilio_sed__numero = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_sed__piso;
        public string? domicilio_sed__piso
        {
            get { return _domicilio_sed__piso; }
            set { _domicilio_sed__piso = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_sed__departamento;
        public string? domicilio_sed__departamento
        {
            get { return _domicilio_sed__departamento; }
            set { _domicilio_sed__departamento = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_sed__barrio;
        public string? domicilio_sed__barrio
        {
            get { return _domicilio_sed__barrio; }
            set { _domicilio_sed__barrio = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_sed__localidad;
        public string? domicilio_sed__localidad
        {
            get { return _domicilio_sed__localidad; }
            set { _domicilio_sed__localidad = value; NotifyPropertyChanged(); }
        }
        private string? _tipo_sede_sed__id;
        public string? tipo_sede_sed__id
        {
            get { return _tipo_sede_sed__id; }
            set { _tipo_sede_sed__id = value; NotifyPropertyChanged(); }
        }
        private string? _tipo_sede_sed__descripcion;
        public string? tipo_sede_sed__descripcion
        {
            get { return _tipo_sede_sed__descripcion; }
            set { _tipo_sede_sed__descripcion = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo_sed__id;
        public string? centro_educativo_sed__id
        {
            get { return _centro_educativo_sed__id; }
            set { _centro_educativo_sed__id = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo_sed__nombre;
        public string? centro_educativo_sed__nombre
        {
            get { return _centro_educativo_sed__nombre; }
            set { _centro_educativo_sed__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo_sed__cue;
        public string? centro_educativo_sed__cue
        {
            get { return _centro_educativo_sed__cue; }
            set { _centro_educativo_sed__cue = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo_sed__domicilio;
        public string? centro_educativo_sed__domicilio
        {
            get { return _centro_educativo_sed__domicilio; }
            set { _centro_educativo_sed__domicilio = value; NotifyPropertyChanged(); }
        }
        private string? _centro_educativo_sed__observaciones;
        public string? centro_educativo_sed__observaciones
        {
            get { return _centro_educativo_sed__observaciones; }
            set { _centro_educativo_sed__observaciones = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen1__id;
        public string? domicilio_cen1__id
        {
            get { return _domicilio_cen1__id; }
            set { _domicilio_cen1__id = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen1__calle;
        public string? domicilio_cen1__calle
        {
            get { return _domicilio_cen1__calle; }
            set { _domicilio_cen1__calle = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen1__entre;
        public string? domicilio_cen1__entre
        {
            get { return _domicilio_cen1__entre; }
            set { _domicilio_cen1__entre = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen1__numero;
        public string? domicilio_cen1__numero
        {
            get { return _domicilio_cen1__numero; }
            set { _domicilio_cen1__numero = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen1__piso;
        public string? domicilio_cen1__piso
        {
            get { return _domicilio_cen1__piso; }
            set { _domicilio_cen1__piso = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen1__departamento;
        public string? domicilio_cen1__departamento
        {
            get { return _domicilio_cen1__departamento; }
            set { _domicilio_cen1__departamento = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen1__barrio;
        public string? domicilio_cen1__barrio
        {
            get { return _domicilio_cen1__barrio; }
            set { _domicilio_cen1__barrio = value; NotifyPropertyChanged(); }
        }
        private string? _domicilio_cen1__localidad;
        public string? domicilio_cen1__localidad
        {
            get { return _domicilio_cen1__localidad; }
            set { _domicilio_cen1__localidad = value; NotifyPropertyChanged(); }
        }
        private string? _modalidad_rel__id;
        public string? modalidad_rel__id
        {
            get { return _modalidad_rel__id; }
            set { _modalidad_rel__id = value; NotifyPropertyChanged(); }
        }
        private string? _modalidad_rel__nombre;
        public string? modalidad_rel__nombre
        {
            get { return _modalidad_rel__nombre; }
            set { _modalidad_rel__nombre = value; NotifyPropertyChanged(); }
        }
        private string? _modalidad_rel__pfid;
        public string? modalidad_rel__pfid
        {
            get { return _modalidad_rel__pfid; }
            set { _modalidad_rel__pfid = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion_rel__id;
        public string? planificacion_rel__id
        {
            get { return _planificacion_rel__id; }
            set { _planificacion_rel__id = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion_rel__anio;
        public string? planificacion_rel__anio
        {
            get { return _planificacion_rel__anio; }
            set { _planificacion_rel__anio = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion_rel__semestre;
        public string? planificacion_rel__semestre
        {
            get { return _planificacion_rel__semestre; }
            set { _planificacion_rel__semestre = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion_rel__plan;
        public string? planificacion_rel__plan
        {
            get { return _planificacion_rel__plan; }
            set { _planificacion_rel__plan = value; NotifyPropertyChanged(); }
        }
        private string? _planificacion_rel__pfid;
        public string? planificacion_rel__pfid
        {
            get { return _planificacion_rel__pfid; }
            set { _planificacion_rel__pfid = value; NotifyPropertyChanged(); }
        }
        private string? _plan_pla__id;
        public string? plan_pla__id
        {
            get { return _plan_pla__id; }
            set { _plan_pla__id = value; NotifyPropertyChanged(); }
        }
        private string? _plan_pla__orientacion;
        public string? plan_pla__orientacion
        {
            get { return _plan_pla__orientacion; }
            set { _plan_pla__orientacion = value; NotifyPropertyChanged(); }
        }
        private string? _plan_pla__resolucion;
        public string? plan_pla__resolucion
        {
            get { return _plan_pla__resolucion; }
            set { _plan_pla__resolucion = value; NotifyPropertyChanged(); }
        }
        private string? _plan_pla__distribucion_horaria;
        public string? plan_pla__distribucion_horaria
        {
            get { return _plan_pla__distribucion_horaria; }
            set { _plan_pla__distribucion_horaria = value; NotifyPropertyChanged(); }
        }
        private string? _plan_pla__pfid;
        public string? plan_pla__pfid
        {
            get { return _plan_pla__pfid; }
            set { _plan_pla__pfid = value; NotifyPropertyChanged(); }
        }
        private string? _calendario_rel__id;
        public string? calendario_rel__id
        {
            get { return _calendario_rel__id; }
            set { _calendario_rel__id = value; NotifyPropertyChanged(); }
        }
        private DateTime? _calendario_rel__inicio;
        public DateTime? calendario_rel__inicio
        {
            get { return _calendario_rel__inicio; }
            set { _calendario_rel__inicio = value; NotifyPropertyChanged(); }
        }
        private DateTime? _calendario_rel__fin;
        public DateTime? calendario_rel__fin
        {
            get { return _calendario_rel__fin; }
            set { _calendario_rel__fin = value; NotifyPropertyChanged(); }
        }
        private short? _calendario_rel__anio;
        public short? calendario_rel__anio
        {
            get { return _calendario_rel__anio; }
            set { _calendario_rel__anio = value; NotifyPropertyChanged(); }
        }
        private short? _calendario_rel__semestre;
        public short? calendario_rel__semestre
        {
            get { return _calendario_rel__semestre; }
            set { _calendario_rel__semestre = value; NotifyPropertyChanged(); }
        }
        private DateTime? _calendario_rel__insertado;
        public DateTime? calendario_rel__insertado
        {
            get { return _calendario_rel__insertado; }
            set { _calendario_rel__insertado = value; NotifyPropertyChanged(); }
        }
        private string? _calendario_rel__descripcion;
        public string? calendario_rel__descripcion
        {
            get { return _calendario_rel__descripcion; }
            set { _calendario_rel__descripcion = value; NotifyPropertyChanged(); }
        }
    }
}
