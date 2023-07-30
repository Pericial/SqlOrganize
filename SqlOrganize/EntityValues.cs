
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Utils;

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
    public class EntityValues : EntityFieldId
    {

        public Logging logging { get; set; } = new Logging();

        public Dictionary<string, object> values = new Dictionary<string, object>();

        public EntityValues(Db _db, string _entity_name, string? _field_id) : base(_db, _entity_name, _field_id)
        {

        }

        public EntityValues Set(Dictionary<string, object> row)
        {
            foreach (var fieldName in db.FieldNames(entityName))
                if(row.ContainsKey(Pf()+fieldName))
                    Set(fieldName, row[fieldName]);

            return this;
        }

        public EntityValues Set(string fieldName, object value)
        {
            values[fieldName] = value;
            return this;
        }

        public object Get(string fieldName)
        {
            return values[fieldName];
        }

        public EntityValues Sset(string fieldName, object value)
        {
            var method = "Sset_" + fieldName;
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
                m!.Invoke(this, new object[] { value });

            Field field = db.Field(entityName, fieldName);
            switch (field.dataType)
            {
                case "string":
                    values[fieldName] = (string)value;
                    break;
                case "int":
                    values[fieldName] = (int)value;
                    break;
            }

            return this;
        }

        public EntityValues Reset()
        {
            foreach (var fieldName in db.FieldNames(entityName))
                if (values.ContainsKey(fieldName))
                    Reset(fieldName, values[fieldName]);

            return this;
        }


        public EntityValues Reset(string fieldName, object value)
        {
            var method = "Reset_" + fieldName;
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
                m!.Invoke(this, new object[] { value });

            Field field = db.Field(entityName, fieldName);
            switch (field.dataType)
            {
                case "string":
                    values[fieldName] = Regex.Replace((string)value, @"\s+", " ").Trim();
                break;
            }

            return this;
        }

        public EntityValues Default()
        {
            foreach (var fieldName in db.FieldNames(entityName))
                if (!values.ContainsKey(fieldName))
                    Default(fieldName);

            return this;
        }
        public EntityValues Default(string fieldName)
        {
            if (values.ContainsKey(fieldName))
                return this;

            var method = "Default_" + fieldName;
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
                m!.Invoke(this, new object[] { });

            Field field = db.Field(entityName, fieldName);
            switch (field.dataType)
            {
                case "date":
                case "datetime":
                case "year":
                case "time":
                    if (field.defaultValue.ToString().ToLower().Contains("cur"))
                        values[fieldName] = DateTime.Now;
                    break;
                default:
                    values[fieldName] = field.defaultValue;
                    break;
            }

            return this;
        }


        public EntityValues Check()
        {
            foreach (var fieldName in db.FieldNames(entityName))
                if (values.ContainsKey(fieldName))
                    Check(fieldName);

            return this;
        }

        /// <summary>
        /// Validar valor del field
        /// </summary>
        /// <param name="fieldName">Nombre del field a validar</param>
        /// <returns>Resultado de la validacion</returns>
        public bool Check(string fieldName)
        {
            logging.ResetLogs(fieldName);
            var method = "check_" + fieldName;
            Type thisType = this.GetType();
            MethodInfo? m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
                return (bool)m!.Invoke(this, null);

            Field field = db.Field(entityName, fieldName);
            Dictionary<string, object> checkMethods = new();
            checkMethods["type"] = field.dataType;
            if (field.IsRequired()) 
                checkMethods["required"] = true;

            Validation v = new(values[fieldName]);

            foreach (var (checkMethod, param) in checkMethods)
            {
                Type validationType = v.GetType();
                m = validationType.GetMethod(checkMethod);
                if (!m.IsNullOrEmpty())
                    m!.Invoke(v, new object[] { param });
                else
                    logging.AddLog(key: fieldName, msg: "No existe el metodo de validacion:  " + checkMethod, type: "check");
            }

            foreach (var error in v.errors)
                logging.AddError(key: fieldName, type: error.type, msg: error.msg);

            return v.IsSuccess();
        }
    }

}