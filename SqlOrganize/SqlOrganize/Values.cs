
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SqlOrganize
{
    /*
    Values of entity

    Define metodos basicos para administrar valores:
    
    -sset: Seteo con cast y formateo
    -set: Seteo directo
    -check: Validar valor
    -default: Asignar valor por defecto
    -get: Retorno directo
    -json: Transformar a json
    -sql: Transformar a sql
    */
    public abstract class Values : EntityOptions
    {

        public Logging logging { get; set; } = new Logging();

        public Dictionary <string, object>  values = new Dictionary<string, object> ();

        public Values(Db _db, string _entity_name, string _field_id) : base(_db, _entity_name, _field_id)
        {

        }

        public void set(string field_name, object value)
        {
            values[field_name] = value;
        }

        public object get(string field_name)
        {
            return values[field_name];
        }

        public void sset(string field_name, object value)
        {
            var method = "sset_" + field_name.Replace(".", "_");
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
            {
                m.Invoke(this, new object[] { value });
            }

            method = _define_sset_method(field_name);
            m = thisType.GetMethod(method);
            m.Invoke(this, new object[] { field_name, value });
        }

        /*
        Si la funcion sset de field_name no se encuentra definida por el usuario,        
        se define en funcion de type
        */
        public string _define_sset_method(string field_name)
        {
            List<string> p = field_name.Split(".").ToList();

            if (p.Count == 1)
            {
                Field field = db.field(entity_name, field_name);
                switch (field.type)
                {
                    
                    default:
                        return "_sset_string";

                }
            }

            string method = "_" + p[p.Count - 1]; //se traduce el metodo ubicado mas a la derecha (el primero en traducirse se ejecutara al final)
            p.RemoveAt(p.Count - 1);

            switch (method)
            {
                case "count":
                case "avg":
                case "sum":
                    return "_sset_int";

                default:
                    return _define_sset_method(String.Join(".", p.ToArray())); //si no resuelve, intenta nuevamente (ejemplo field.count.max, intentara nuevamente con field.count)
            }
        }

        public void _sset_string(string field_name, object value)
        {
            values[field_name] = Regex.Replace((string)value, @"\s+", " ").Trim();
        }

        public void _sset_int(string field_name, object value)
        {
            values[field_name] = (int)value;
        }

        /*
        Validacion
        */
        public bool check(string field_name)
        {
            logging.reset_logs(field_name);

            var method = "check_" + field_name.Replace(".", "_");
            Type thisType = this.GetType();
            MethodInfo? m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
            {
                return (bool)m!.Invoke(this, null);
            }

            Dictionary<string, object?> check_methods = _define_check_methods(field_name);

            Validation v = new(values[field_name]);

            foreach (var (check_method, param) in check_methods)
            {
                Type validationType = v.GetType();
                m = validationType.GetMethod(check_method);
                if (!m.IsNullOrEmpty())
                {
                    m!.Invoke(v, new object[] { param });
                }
                else
                {
                    logging.add_log(key: field_name, msg: "No existe el metodo de validacion", type: "check");
                }
            }

            foreach (var error in v.errors)
            {
                logging.add_error(key: field_name, type: error.type, msg: error.msg);
            }

            return v.is_success();
        }

        public Dictionary<string, object?> _define_check_methods(string field_name)
        {
            List<string> p = field_name.Split(".").ToList();
            Dictionary<string, object> r = new();

            if (p.Count == 1) //traducir field_name sin funcion (por el momento solo se validan los fields sin funcion
            {
                Field field = db.field(entity_name, field_name);

                r["type"] = field.type;
                if(field.required) r["required"] = null;

            }

            return new();

        }

        /*
        Convertir valor para que sea entendido por el motor de base de datos
        */
        /*
        @example 
        _sql("nombre")
        _sql("nombre.max");
        */
        public object sql(string field_name)
        {
            if (!values.ContainsKey(field_name))
            {
                throw new Exception(field_name + " no tiene valor definido");
            }

            var method = "sql_" + field_name.Replace(".", "_");
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
            {
                return m!.Invoke(this, null);
            }

            List<string> p = field_name.Split('.').ToList();

            if (p.Count == 1)
            {
                Field field = db.field(entity_name, p[0]);
                switch (field.type)
                {
                    default:
                        return _sql_default(field_name);
                }
            }

            return _sql_aux(field_name);

        }

        public object _sql_default(string field_name) {
            return values[field_name];
        }

        public object _sql_aux(string field_name)
        {
            List<string> p = field_name.Split(".").ToList();
            if (p.Count == 1)
            {
                Field field = db.field(entity_name, field_name);
                switch (field.type)
                {
                    default:
                        return _sql_default(field_name);
                }
            }

            string f = "_" + p[p.Count - 1]; //se traduce el metodo ubicado mas a la derecha (el primero en traducirse se ejecutara al final)
            p.RemoveAt(p.Count - 1);

            switch (f)
            {
                case "count":
                case "avg":
                case "sum":
                    return _sql_default(field_name);

                default:
                    return _sql_aux(String.Join(".", p.ToArray())); //si no resuelve, intenta nuevamente (ejemplo field.count.max, intentara nuevamente con field.count)
            }
        }

    }

}