using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_calificacion_rel : INotifyPropertyChanged
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
        private string _curso__id;
        public string curso__id
        {
            get { return _curso__id; }
            set { _curso__id = value; NotifyPropertyChanged(); }
        }
        private int _curso__horas_catedra;
        public int curso__horas_catedra
        {
            get { return _curso__horas_catedra; }
            set { _curso__horas_catedra = value; NotifyPropertyChanged(); }
        }
        private string _curso__ige;
        public string curso__ige
        {
            get { return _curso__ige; }
            set { _curso__ige = value; NotifyPropertyChanged(); }
        }
        private string _curso__comision;
        public string curso__comision
        {
            get { return _curso__comision; }
            set { _curso__comision = value; NotifyPropertyChanged(); }
        }
        private string _curso__asignatura;
        public string curso__asignatura
        {
            get { return _curso__asignatura; }
            set { _curso__asignatura = value; NotifyPropertyChanged(); }
        }
        private DateTime _curso__alta;
        public DateTime curso__alta
        {
            get { return _curso__alta; }
            set { _curso__alta = value; NotifyPropertyChanged(); }
        }
        private string _curso__descripcion_horario;
        public string curso__descripcion_horario
        {
            get { return _curso__descripcion_horario; }
            set { _curso__descripcion_horario = value; NotifyPropertyChanged(); }
        }
        private string _comision__id;
        public string comision__id
        {
            get { return _comision__id; }
            set { _comision__id = value; NotifyPropertyChanged(); }
        }
        private string _comision__turno;
        public string comision__turno
        {
            get { return _comision__turno; }
            set { _comision__turno = value; NotifyPropertyChanged(); }
        }
        private string _comision__division;
        public string comision__division
        {
            get { return _comision__division; }
            set { _comision__division = value; NotifyPropertyChanged(); }
        }
        private string _comision__comentario;
        public string comision__comentario
        {
            get { return _comision__comentario; }
            set { _comision__comentario = value; NotifyPropertyChanged(); }
        }
        private bool _comision__autorizada;
        public bool comision__autorizada
        {
            get { return _comision__autorizada; }
            set { _comision__autorizada = value; NotifyPropertyChanged(); }
        }
        private bool _comision__apertura;
        public bool comision__apertura
        {
            get { return _comision__apertura; }
            set { _comision__apertura = value; NotifyPropertyChanged(); }
        }
        private bool _comision__publicada;
        public bool comision__publicada
        {
            get { return _comision__publicada; }
            set { _comision__publicada = value; NotifyPropertyChanged(); }
        }
        private string _comision__observaciones;
        public string comision__observaciones
        {
            get { return _comision__observaciones; }
            set { _comision__observaciones = value; NotifyPropertyChanged(); }
        }
        private DateTime _comision__alta;
        public DateTime comision__alta
        {
            get { return _comision__alta; }
            set { _comision__alta = value; NotifyPropertyChanged(); }
        }
        private string _comision__sede;
        public string comision__sede
        {
            get { return _comision__sede; }
            set { _comision__sede = value; NotifyPropertyChanged(); }
        }
        private string _comision__modalidad;
        public string comision__modalidad
        {
            get { return _comision__modalidad; }
            set { _comision__modalidad = value; NotifyPropertyChanged(); }
        }
        private string _comision__planificacion;
        public string comision__planificacion
        {
            get { return _comision__planificacion; }
            set { _comision__planificacion = value; NotifyPropertyChanged(); }
        }
        private string _comision__comision_siguiente;
        public string comision__comision_siguiente
        {
            get { return _comision__comision_siguiente; }
            set { _comision__comision_siguiente = value; NotifyPropertyChanged(); }
        }
        private string _comision__calendario;
        public string comision__calendario
        {
            get { return _comision__calendario; }
            set { _comision__calendario = value; NotifyPropertyChanged(); }
        }
        private string _comision__identificacion;
        public string comision__identificacion
        {
            get { return _comision__identificacion; }
            set { _comision__identificacion = value; NotifyPropertyChanged(); }
        }
        private string _comision__pfid;
        public string comision__pfid
        {
            get { return _comision__pfid; }
            set { _comision__pfid = value; NotifyPropertyChanged(); }
        }
        private string _sede__id;
        public string sede__id
        {
            get { return _sede__id; }
            set { _sede__id = value; NotifyPropertyChanged(); }
        }
        private string _sede__numero;
        public string sede__numero
        {
            get { return _sede__numero; }
            set { _sede__numero = value; NotifyPropertyChanged(); }
        }
        private string _sede__nombre;
        public string sede__nombre
        {
            get { return _sede__nombre; }
            set { _sede__nombre = value; NotifyPropertyChanged(); }
        }
        private string _sede__observaciones;
        public string sede__observaciones
        {
            get { return _sede__observaciones; }
            set { _sede__observaciones = value; NotifyPropertyChanged(); }
        }
        private DateTime _sede__alta;
        public DateTime sede__alta
        {
            get { return _sede__alta; }
            set { _sede__alta = value; NotifyPropertyChanged(); }
        }
        private DateTime _sede__baja;
        public DateTime sede__baja
        {
            get { return _sede__baja; }
            set { _sede__baja = value; NotifyPropertyChanged(); }
        }
        private string _sede__domicilio;
        public string sede__domicilio
        {
            get { return _sede__domicilio; }
            set { _sede__domicilio = value; NotifyPropertyChanged(); }
        }
        private string _sede__centro_educativo;
        public string sede__centro_educativo
        {
            get { return _sede__centro_educativo; }
            set { _sede__centro_educativo = value; NotifyPropertyChanged(); }
        }
        private DateTime _sede__fecha_traspaso;
        public DateTime sede__fecha_traspaso
        {
            get { return _sede__fecha_traspaso; }
            set { _sede__fecha_traspaso = value; NotifyPropertyChanged(); }
        }
        private string _sede__organizacion;
        public string sede__organizacion
        {
            get { return _sede__organizacion; }
            set { _sede__organizacion = value; NotifyPropertyChanged(); }
        }
        private string _sede__pfid;
        public string sede__pfid
        {
            get { return _sede__pfid; }
            set { _sede__pfid = value; NotifyPropertyChanged(); }
        }
        private string _sede__pfid_organizacion;
        public string sede__pfid_organizacion
        {
            get { return _sede__pfid_organizacion; }
            set { _sede__pfid_organizacion = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__id;
        public string domicilio__id
        {
            get { return _domicilio__id; }
            set { _domicilio__id = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__calle;
        public string domicilio__calle
        {
            get { return _domicilio__calle; }
            set { _domicilio__calle = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__entre;
        public string domicilio__entre
        {
            get { return _domicilio__entre; }
            set { _domicilio__entre = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__numero;
        public string domicilio__numero
        {
            get { return _domicilio__numero; }
            set { _domicilio__numero = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__piso;
        public string domicilio__piso
        {
            get { return _domicilio__piso; }
            set { _domicilio__piso = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__departamento;
        public string domicilio__departamento
        {
            get { return _domicilio__departamento; }
            set { _domicilio__departamento = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__barrio;
        public string domicilio__barrio
        {
            get { return _domicilio__barrio; }
            set { _domicilio__barrio = value; NotifyPropertyChanged(); }
        }
        private string _domicilio__localidad;
        public string domicilio__localidad
        {
            get { return _domicilio__localidad; }
            set { _domicilio__localidad = value; NotifyPropertyChanged(); }
        }
        private string _centro_educativo__id;
        public string centro_educativo__id
        {
            get { return _centro_educativo__id; }
            set { _centro_educativo__id = value; NotifyPropertyChanged(); }
        }
        private string _centro_educativo__nombre;
        public string centro_educativo__nombre
        {
            get { return _centro_educativo__nombre; }
            set { _centro_educativo__nombre = value; NotifyPropertyChanged(); }
        }
        private string _centro_educativo__cue;
        public string centro_educativo__cue
        {
            get { return _centro_educativo__cue; }
            set { _centro_educativo__cue = value; NotifyPropertyChanged(); }
        }
        private string _centro_educativo__domicilio;
        public string centro_educativo__domicilio
        {
            get { return _centro_educativo__domicilio; }
            set { _centro_educativo__domicilio = value; NotifyPropertyChanged(); }
        }
        private string _centro_educativo__observaciones;
        public string centro_educativo__observaciones
        {
            get { return _centro_educativo__observaciones; }
            set { _centro_educativo__observaciones = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_cen__id;
        public string domicilio_cen__id
        {
            get { return _domicilio_cen__id; }
            set { _domicilio_cen__id = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_cen__calle;
        public string domicilio_cen__calle
        {
            get { return _domicilio_cen__calle; }
            set { _domicilio_cen__calle = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_cen__entre;
        public string domicilio_cen__entre
        {
            get { return _domicilio_cen__entre; }
            set { _domicilio_cen__entre = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_cen__numero;
        public string domicilio_cen__numero
        {
            get { return _domicilio_cen__numero; }
            set { _domicilio_cen__numero = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_cen__piso;
        public string domicilio_cen__piso
        {
            get { return _domicilio_cen__piso; }
            set { _domicilio_cen__piso = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_cen__departamento;
        public string domicilio_cen__departamento
        {
            get { return _domicilio_cen__departamento; }
            set { _domicilio_cen__departamento = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_cen__barrio;
        public string domicilio_cen__barrio
        {
            get { return _domicilio_cen__barrio; }
            set { _domicilio_cen__barrio = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_cen__localidad;
        public string domicilio_cen__localidad
        {
            get { return _domicilio_cen__localidad; }
            set { _domicilio_cen__localidad = value; NotifyPropertyChanged(); }
        }
        private string _modalidad__id;
        public string modalidad__id
        {
            get { return _modalidad__id; }
            set { _modalidad__id = value; NotifyPropertyChanged(); }
        }
        private string _modalidad__nombre;
        public string modalidad__nombre
        {
            get { return _modalidad__nombre; }
            set { _modalidad__nombre = value; NotifyPropertyChanged(); }
        }
        private string _modalidad__pfid;
        public string modalidad__pfid
        {
            get { return _modalidad__pfid; }
            set { _modalidad__pfid = value; NotifyPropertyChanged(); }
        }
        private string _planificacion__id;
        public string planificacion__id
        {
            get { return _planificacion__id; }
            set { _planificacion__id = value; NotifyPropertyChanged(); }
        }
        private string _planificacion__anio;
        public string planificacion__anio
        {
            get { return _planificacion__anio; }
            set { _planificacion__anio = value; NotifyPropertyChanged(); }
        }
        private string _planificacion__semestre;
        public string planificacion__semestre
        {
            get { return _planificacion__semestre; }
            set { _planificacion__semestre = value; NotifyPropertyChanged(); }
        }
        private string _planificacion__plan;
        public string planificacion__plan
        {
            get { return _planificacion__plan; }
            set { _planificacion__plan = value; NotifyPropertyChanged(); }
        }
        private string _planificacion__pfid;
        public string planificacion__pfid
        {
            get { return _planificacion__pfid; }
            set { _planificacion__pfid = value; NotifyPropertyChanged(); }
        }
        private string _plan__id;
        public string plan__id
        {
            get { return _plan__id; }
            set { _plan__id = value; NotifyPropertyChanged(); }
        }
        private string _plan__orientacion;
        public string plan__orientacion
        {
            get { return _plan__orientacion; }
            set { _plan__orientacion = value; NotifyPropertyChanged(); }
        }
        private string _plan__resolucion;
        public string plan__resolucion
        {
            get { return _plan__resolucion; }
            set { _plan__resolucion = value; NotifyPropertyChanged(); }
        }
        private string _plan__distribucion_horaria;
        public string plan__distribucion_horaria
        {
            get { return _plan__distribucion_horaria; }
            set { _plan__distribucion_horaria = value; NotifyPropertyChanged(); }
        }
        private string _plan__pfid;
        public string plan__pfid
        {
            get { return _plan__pfid; }
            set { _plan__pfid = value; NotifyPropertyChanged(); }
        }
        private string _calendario__id;
        public string calendario__id
        {
            get { return _calendario__id; }
            set { _calendario__id = value; NotifyPropertyChanged(); }
        }
        private DateTime _calendario__inicio;
        public DateTime calendario__inicio
        {
            get { return _calendario__inicio; }
            set { _calendario__inicio = value; NotifyPropertyChanged(); }
        }
        private DateTime _calendario__fin;
        public DateTime calendario__fin
        {
            get { return _calendario__fin; }
            set { _calendario__fin = value; NotifyPropertyChanged(); }
        }
        private short _calendario__anio;
        public short calendario__anio
        {
            get { return _calendario__anio; }
            set { _calendario__anio = value; NotifyPropertyChanged(); }
        }
        private short _calendario__semestre;
        public short calendario__semestre
        {
            get { return _calendario__semestre; }
            set { _calendario__semestre = value; NotifyPropertyChanged(); }
        }
        private DateTime _calendario__insertado;
        public DateTime calendario__insertado
        {
            get { return _calendario__insertado; }
            set { _calendario__insertado = value; NotifyPropertyChanged(); }
        }
        private string _calendario__descripcion;
        public string calendario__descripcion
        {
            get { return _calendario__descripcion; }
            set { _calendario__descripcion = value; NotifyPropertyChanged(); }
        }
        private string _asignatura__id;
        public string asignatura__id
        {
            get { return _asignatura__id; }
            set { _asignatura__id = value; NotifyPropertyChanged(); }
        }
        private string _asignatura__nombre;
        public string asignatura__nombre
        {
            get { return _asignatura__nombre; }
            set { _asignatura__nombre = value; NotifyPropertyChanged(); }
        }
        private string _asignatura__formacion;
        public string asignatura__formacion
        {
            get { return _asignatura__formacion; }
            set { _asignatura__formacion = value; NotifyPropertyChanged(); }
        }
        private string _asignatura__clasificacion;
        public string asignatura__clasificacion
        {
            get { return _asignatura__clasificacion; }
            set { _asignatura__clasificacion = value; NotifyPropertyChanged(); }
        }
        private string _asignatura__codigo;
        public string asignatura__codigo
        {
            get { return _asignatura__codigo; }
            set { _asignatura__codigo = value; NotifyPropertyChanged(); }
        }
        private string _asignatura__perfil;
        public string asignatura__perfil
        {
            get { return _asignatura__perfil; }
            set { _asignatura__perfil = value; NotifyPropertyChanged(); }
        }
        private string _alumno__id;
        public string alumno__id
        {
            get { return _alumno__id; }
            set { _alumno__id = value; NotifyPropertyChanged(); }
        }
        private string _alumno__anio_ingreso;
        public string alumno__anio_ingreso
        {
            get { return _alumno__anio_ingreso; }
            set { _alumno__anio_ingreso = value; NotifyPropertyChanged(); }
        }
        private string _alumno__observaciones;
        public string alumno__observaciones
        {
            get { return _alumno__observaciones; }
            set { _alumno__observaciones = value; NotifyPropertyChanged(); }
        }
        private string _alumno__persona;
        public string alumno__persona
        {
            get { return _alumno__persona; }
            set { _alumno__persona = value; NotifyPropertyChanged(); }
        }
        private string _alumno__estado_inscripcion;
        public string alumno__estado_inscripcion
        {
            get { return _alumno__estado_inscripcion; }
            set { _alumno__estado_inscripcion = value; NotifyPropertyChanged(); }
        }
        private DateTime _alumno__fecha_titulacion;
        public DateTime alumno__fecha_titulacion
        {
            get { return _alumno__fecha_titulacion; }
            set { _alumno__fecha_titulacion = value; NotifyPropertyChanged(); }
        }
        private string _alumno__plan;
        public string alumno__plan
        {
            get { return _alumno__plan; }
            set { _alumno__plan = value; NotifyPropertyChanged(); }
        }
        private string _alumno__resolucion_inscripcion;
        public string alumno__resolucion_inscripcion
        {
            get { return _alumno__resolucion_inscripcion; }
            set { _alumno__resolucion_inscripcion = value; NotifyPropertyChanged(); }
        }
        private short _alumno__anio_inscripcion;
        public short alumno__anio_inscripcion
        {
            get { return _alumno__anio_inscripcion; }
            set { _alumno__anio_inscripcion = value; NotifyPropertyChanged(); }
        }
        private short _alumno__semestre_inscripcion;
        public short alumno__semestre_inscripcion
        {
            get { return _alumno__semestre_inscripcion; }
            set { _alumno__semestre_inscripcion = value; NotifyPropertyChanged(); }
        }
        private short _alumno__semestre_ingreso;
        public short alumno__semestre_ingreso
        {
            get { return _alumno__semestre_ingreso; }
            set { _alumno__semestre_ingreso = value; NotifyPropertyChanged(); }
        }
        private string _alumno__adeuda_legajo;
        public string alumno__adeuda_legajo
        {
            get { return _alumno__adeuda_legajo; }
            set { _alumno__adeuda_legajo = value; NotifyPropertyChanged(); }
        }
        private string _alumno__adeuda_deudores;
        public string alumno__adeuda_deudores
        {
            get { return _alumno__adeuda_deudores; }
            set { _alumno__adeuda_deudores = value; NotifyPropertyChanged(); }
        }
        private string _alumno__documentacion_inscripcion;
        public string alumno__documentacion_inscripcion
        {
            get { return _alumno__documentacion_inscripcion; }
            set { _alumno__documentacion_inscripcion = value; NotifyPropertyChanged(); }
        }
        private bool _alumno__anio_inscripcion_completo;
        public bool alumno__anio_inscripcion_completo
        {
            get { return _alumno__anio_inscripcion_completo; }
            set { _alumno__anio_inscripcion_completo = value; NotifyPropertyChanged(); }
        }
        private string _alumno__establecimiento_inscripcion;
        public string alumno__establecimiento_inscripcion
        {
            get { return _alumno__establecimiento_inscripcion; }
            set { _alumno__establecimiento_inscripcion = value; NotifyPropertyChanged(); }
        }
        private string _alumno__libro_folio;
        public string alumno__libro_folio
        {
            get { return _alumno__libro_folio; }
            set { _alumno__libro_folio = value; NotifyPropertyChanged(); }
        }
        private string _alumno__libro;
        public string alumno__libro
        {
            get { return _alumno__libro; }
            set { _alumno__libro = value; NotifyPropertyChanged(); }
        }
        private string _alumno__folio;
        public string alumno__folio
        {
            get { return _alumno__folio; }
            set { _alumno__folio = value; NotifyPropertyChanged(); }
        }
        private string _alumno__comentarios;
        public string alumno__comentarios
        {
            get { return _alumno__comentarios; }
            set { _alumno__comentarios = value; NotifyPropertyChanged(); }
        }
        private bool _alumno__tiene_dni;
        public bool alumno__tiene_dni
        {
            get { return _alumno__tiene_dni; }
            set { _alumno__tiene_dni = value; NotifyPropertyChanged(); }
        }
        private bool _alumno__tiene_constancia;
        public bool alumno__tiene_constancia
        {
            get { return _alumno__tiene_constancia; }
            set { _alumno__tiene_constancia = value; NotifyPropertyChanged(); }
        }
        private bool _alumno__tiene_certificado;
        public bool alumno__tiene_certificado
        {
            get { return _alumno__tiene_certificado; }
            set { _alumno__tiene_certificado = value; NotifyPropertyChanged(); }
        }
        private bool _alumno__previas_completas;
        public bool alumno__previas_completas
        {
            get { return _alumno__previas_completas; }
            set { _alumno__previas_completas = value; NotifyPropertyChanged(); }
        }
        private bool _alumno__tiene_partida;
        public bool alumno__tiene_partida
        {
            get { return _alumno__tiene_partida; }
            set { _alumno__tiene_partida = value; NotifyPropertyChanged(); }
        }
        private DateTime _alumno__creado;
        public DateTime alumno__creado
        {
            get { return _alumno__creado; }
            set { _alumno__creado = value; NotifyPropertyChanged(); }
        }
        private bool _alumno__confirmado_direccion;
        public bool alumno__confirmado_direccion
        {
            get { return _alumno__confirmado_direccion; }
            set { _alumno__confirmado_direccion = value; NotifyPropertyChanged(); }
        }
        private string _persona__id;
        public string persona__id
        {
            get { return _persona__id; }
            set { _persona__id = value; NotifyPropertyChanged(); }
        }
        private string _persona__nombres;
        public string persona__nombres
        {
            get { return _persona__nombres; }
            set { _persona__nombres = value; NotifyPropertyChanged(); }
        }
        private string _persona__apellidos;
        public string persona__apellidos
        {
            get { return _persona__apellidos; }
            set { _persona__apellidos = value; NotifyPropertyChanged(); }
        }
        private DateTime _persona__fecha_nacimiento;
        public DateTime persona__fecha_nacimiento
        {
            get { return _persona__fecha_nacimiento; }
            set { _persona__fecha_nacimiento = value; NotifyPropertyChanged(); }
        }
        private string _persona__numero_documento;
        public string persona__numero_documento
        {
            get { return _persona__numero_documento; }
            set { _persona__numero_documento = value; NotifyPropertyChanged(); }
        }
        private string _persona__cuil;
        public string persona__cuil
        {
            get { return _persona__cuil; }
            set { _persona__cuil = value; NotifyPropertyChanged(); }
        }
        private string _persona__genero;
        public string persona__genero
        {
            get { return _persona__genero; }
            set { _persona__genero = value; NotifyPropertyChanged(); }
        }
        private string _persona__apodo;
        public string persona__apodo
        {
            get { return _persona__apodo; }
            set { _persona__apodo = value; NotifyPropertyChanged(); }
        }
        private string _persona__telefono;
        public string persona__telefono
        {
            get { return _persona__telefono; }
            set { _persona__telefono = value; NotifyPropertyChanged(); }
        }
        private string _persona__email;
        public string persona__email
        {
            get { return _persona__email; }
            set { _persona__email = value; NotifyPropertyChanged(); }
        }
        private string _persona__email_abc;
        public string persona__email_abc
        {
            get { return _persona__email_abc; }
            set { _persona__email_abc = value; NotifyPropertyChanged(); }
        }
        private DateTime _persona__alta;
        public DateTime persona__alta
        {
            get { return _persona__alta; }
            set { _persona__alta = value; NotifyPropertyChanged(); }
        }
        private string _persona__domicilio;
        public string persona__domicilio
        {
            get { return _persona__domicilio; }
            set { _persona__domicilio = value; NotifyPropertyChanged(); }
        }
        private string _persona__lugar_nacimiento;
        public string persona__lugar_nacimiento
        {
            get { return _persona__lugar_nacimiento; }
            set { _persona__lugar_nacimiento = value; NotifyPropertyChanged(); }
        }
        private bool _persona__telefono_verificado;
        public bool persona__telefono_verificado
        {
            get { return _persona__telefono_verificado; }
            set { _persona__telefono_verificado = value; NotifyPropertyChanged(); }
        }
        private bool _persona__email_verificado;
        public bool persona__email_verificado
        {
            get { return _persona__email_verificado; }
            set { _persona__email_verificado = value; NotifyPropertyChanged(); }
        }
        private bool _persona__info_verificada;
        public bool persona__info_verificada
        {
            get { return _persona__info_verificada; }
            set { _persona__info_verificada = value; NotifyPropertyChanged(); }
        }
        private string _persona__descripcion_domicilio;
        public string persona__descripcion_domicilio
        {
            get { return _persona__descripcion_domicilio; }
            set { _persona__descripcion_domicilio = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_per__id;
        public string domicilio_per__id
        {
            get { return _domicilio_per__id; }
            set { _domicilio_per__id = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_per__calle;
        public string domicilio_per__calle
        {
            get { return _domicilio_per__calle; }
            set { _domicilio_per__calle = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_per__entre;
        public string domicilio_per__entre
        {
            get { return _domicilio_per__entre; }
            set { _domicilio_per__entre = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_per__numero;
        public string domicilio_per__numero
        {
            get { return _domicilio_per__numero; }
            set { _domicilio_per__numero = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_per__piso;
        public string domicilio_per__piso
        {
            get { return _domicilio_per__piso; }
            set { _domicilio_per__piso = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_per__departamento;
        public string domicilio_per__departamento
        {
            get { return _domicilio_per__departamento; }
            set { _domicilio_per__departamento = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_per__barrio;
        public string domicilio_per__barrio
        {
            get { return _domicilio_per__barrio; }
            set { _domicilio_per__barrio = value; NotifyPropertyChanged(); }
        }
        private string _domicilio_per__localidad;
        public string domicilio_per__localidad
        {
            get { return _domicilio_per__localidad; }
            set { _domicilio_per__localidad = value; NotifyPropertyChanged(); }
        }
        private string _plan_alu__id;
        public string plan_alu__id
        {
            get { return _plan_alu__id; }
            set { _plan_alu__id = value; NotifyPropertyChanged(); }
        }
        private string _plan_alu__orientacion;
        public string plan_alu__orientacion
        {
            get { return _plan_alu__orientacion; }
            set { _plan_alu__orientacion = value; NotifyPropertyChanged(); }
        }
        private string _plan_alu__resolucion;
        public string plan_alu__resolucion
        {
            get { return _plan_alu__resolucion; }
            set { _plan_alu__resolucion = value; NotifyPropertyChanged(); }
        }
        private string _plan_alu__distribucion_horaria;
        public string plan_alu__distribucion_horaria
        {
            get { return _plan_alu__distribucion_horaria; }
            set { _plan_alu__distribucion_horaria = value; NotifyPropertyChanged(); }
        }
        private string _plan_alu__pfid;
        public string plan_alu__pfid
        {
            get { return _plan_alu__pfid; }
            set { _plan_alu__pfid = value; NotifyPropertyChanged(); }
        }
        private string _resolucion_inscripcion__id;
        public string resolucion_inscripcion__id
        {
            get { return _resolucion_inscripcion__id; }
            set { _resolucion_inscripcion__id = value; NotifyPropertyChanged(); }
        }
        private string _resolucion_inscripcion__numero;
        public string resolucion_inscripcion__numero
        {
            get { return _resolucion_inscripcion__numero; }
            set { _resolucion_inscripcion__numero = value; NotifyPropertyChanged(); }
        }
        private short _resolucion_inscripcion__anio;
        public short resolucion_inscripcion__anio
        {
            get { return _resolucion_inscripcion__anio; }
            set { _resolucion_inscripcion__anio = value; NotifyPropertyChanged(); }
        }
        private string _resolucion_inscripcion__tipo;
        public string resolucion_inscripcion__tipo
        {
            get { return _resolucion_inscripcion__tipo; }
            set { _resolucion_inscripcion__tipo = value; NotifyPropertyChanged(); }
        }
        private string _disposicion__id;
        public string disposicion__id
        {
            get { return _disposicion__id; }
            set { _disposicion__id = value; NotifyPropertyChanged(); }
        }
        private string _disposicion__asignatura;
        public string disposicion__asignatura
        {
            get { return _disposicion__asignatura; }
            set { _disposicion__asignatura = value; NotifyPropertyChanged(); }
        }
        private string _disposicion__planificacion;
        public string disposicion__planificacion
        {
            get { return _disposicion__planificacion; }
            set { _disposicion__planificacion = value; NotifyPropertyChanged(); }
        }
        private int _disposicion__orden_informe_coordinacion_distrital;
        public int disposicion__orden_informe_coordinacion_distrital
        {
            get { return _disposicion__orden_informe_coordinacion_distrital; }
            set { _disposicion__orden_informe_coordinacion_distrital = value; NotifyPropertyChanged(); }
        }
        private string _asignatura_dis__id;
        public string asignatura_dis__id
        {
            get { return _asignatura_dis__id; }
            set { _asignatura_dis__id = value; NotifyPropertyChanged(); }
        }
        private string _asignatura_dis__nombre;
        public string asignatura_dis__nombre
        {
            get { return _asignatura_dis__nombre; }
            set { _asignatura_dis__nombre = value; NotifyPropertyChanged(); }
        }
        private string _asignatura_dis__formacion;
        public string asignatura_dis__formacion
        {
            get { return _asignatura_dis__formacion; }
            set { _asignatura_dis__formacion = value; NotifyPropertyChanged(); }
        }
        private string _asignatura_dis__clasificacion;
        public string asignatura_dis__clasificacion
        {
            get { return _asignatura_dis__clasificacion; }
            set { _asignatura_dis__clasificacion = value; NotifyPropertyChanged(); }
        }
        private string _asignatura_dis__codigo;
        public string asignatura_dis__codigo
        {
            get { return _asignatura_dis__codigo; }
            set { _asignatura_dis__codigo = value; NotifyPropertyChanged(); }
        }
        private string _asignatura_dis__perfil;
        public string asignatura_dis__perfil
        {
            get { return _asignatura_dis__perfil; }
            set { _asignatura_dis__perfil = value; NotifyPropertyChanged(); }
        }
        private string _planificacion_dis__id;
        public string planificacion_dis__id
        {
            get { return _planificacion_dis__id; }
            set { _planificacion_dis__id = value; NotifyPropertyChanged(); }
        }
        private string _planificacion_dis__anio;
        public string planificacion_dis__anio
        {
            get { return _planificacion_dis__anio; }
            set { _planificacion_dis__anio = value; NotifyPropertyChanged(); }
        }
        private string _planificacion_dis__semestre;
        public string planificacion_dis__semestre
        {
            get { return _planificacion_dis__semestre; }
            set { _planificacion_dis__semestre = value; NotifyPropertyChanged(); }
        }
        private string _planificacion_dis__plan;
        public string planificacion_dis__plan
        {
            get { return _planificacion_dis__plan; }
            set { _planificacion_dis__plan = value; NotifyPropertyChanged(); }
        }
        private string _planificacion_dis__pfid;
        public string planificacion_dis__pfid
        {
            get { return _planificacion_dis__pfid; }
            set { _planificacion_dis__pfid = value; NotifyPropertyChanged(); }
        }
        private string _plan_pla__id;
        public string plan_pla__id
        {
            get { return _plan_pla__id; }
            set { _plan_pla__id = value; NotifyPropertyChanged(); }
        }
        private string _plan_pla__orientacion;
        public string plan_pla__orientacion
        {
            get { return _plan_pla__orientacion; }
            set { _plan_pla__orientacion = value; NotifyPropertyChanged(); }
        }
        private string _plan_pla__resolucion;
        public string plan_pla__resolucion
        {
            get { return _plan_pla__resolucion; }
            set { _plan_pla__resolucion = value; NotifyPropertyChanged(); }
        }
        private string _plan_pla__distribucion_horaria;
        public string plan_pla__distribucion_horaria
        {
            get { return _plan_pla__distribucion_horaria; }
            set { _plan_pla__distribucion_horaria = value; NotifyPropertyChanged(); }
        }
        private string _plan_pla__pfid;
        public string plan_pla__pfid
        {
            get { return _plan_pla__pfid; }
            set { _plan_pla__pfid = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
