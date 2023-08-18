
using System.Reflection;
using System.Text.RegularExpressions;
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

        public EntityValues(Db _db, string _entityName, string? _fieldId = null) : base(_db, _entityName, _fieldId)
        {
        }

        public EntityValues Set(IDictionary<string, object> row)
        {
            foreach (var fieldName in db.FieldNames(entityName))
                if(row.ContainsKey(Pf() + fieldName))
                    Set(fieldName, row[Pf() + fieldName]);

            return this;
        }

        public EntityValues Set(string fieldName, object value)
        {
            string fn = fieldName;
            if (!Pf().IsNullOrEmpty() && fieldName.Contains(Pf()))
                fn = fieldName.Replace(Pf(), "");
            values[fn] = value;
            return this;
        }

        public EntityValues Remove(string fieldName)
        {
            values.Remove(fieldName);
            return this;
        }

        public object Get(string fieldName)
        {
            return values[fieldName];
        }

        public Dictionary<string, object> Get()
        {
            Dictionary<string, object> response = new();
            foreach (var fieldName in db.FieldNames(entityName))
                if (values.ContainsKey(fieldName))
                    response[Pf()+fieldName] = values[fieldName];

            return response;
        }


        public EntityValues Sset(string fieldName, object value)
        {
            var method = "Sset_" + fieldName;
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
                m!.Invoke(this, new object[] { value });

            Field field = db.Field(entityName, fieldName);
            if (value == null)
            {
                values[fieldName] = null;
                return this;
            }

            switch (field.dataType)
            {
                case "string":
                    values[fieldName] = (string)value;
                    break;
                case "int":
                    values[fieldName] = Int32.Parse(value.ToString());
                    break;
                case "bool":
                    values[fieldName] = (value as string).ToBool();
                    break;

            }

            return this;
        }

        /// <summary>
        /// Resetear valores definidos
        /// </summary>
        /// <returns></returns>
        public EntityValues Reset()
        {
            List<string> fieldNames = new List<string>(db.FieldNames(entityName));
            fieldNames.Remove(db.config.id); //id debe dejarse para el final porque puede depender de otros valores

            foreach (var fieldName in fieldNames)
                if (values.ContainsKey(fieldName))
                    Reset(fieldName);

            Reset(db.config.id);

            return this;
        }

        public EntityValues Reset(string fieldName)
        {
            var method = "Reset_" + fieldName;
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
                m!.Invoke(this, new object[] { });

            Field field = db.Field(entityName, fieldName);
            foreach (var (resetKey, resetValue) in field.resets)
            {
                switch (resetKey)
                {
                    case "trim":
                        if (!values[fieldName].IsNullOrEmpty())
                            values[fieldName] = ((string)values[fieldName]).Trim(((string)resetValue).ToChar());
                        break;
                    case "removeMultipleSpaces":
                        if (!values[fieldName].IsNullOrEmpty())
                            values[fieldName] = Regex.Replace((string)values[fieldName], @"\s+", " ");
                        break;

                }
            }

            return this;
        }

        /// <summary>
        /// Asignar valor por defecto para aquellos valores no definidos
        /// </summary>
        /// <returns></returns>
        public EntityValues Default()
        {
            foreach (var fieldName in db.FieldNames(entityName))
                if (!values.ContainsKey(fieldName))
                    Default(fieldName);

            return this;
        }

        /// <summary>
        /// Reset _Id
        /// </summary>
        /// <remarks>_Id depende de otros valores de la misma entidad, se reasigna luego de definir el resto de los valores</remarks>
        /// <example>db.Values("entityName").Set(source).Set("_Id", null).Reset("_Id"); //inicializa y reasigna _Id individualmente //<br/>
        /// db.Values("entityName").Set(source).Default().Reset() //inicializa y reasigna _Id conjuntamente</example>
        /// <returns></returns>
        public EntityValues Reset__Id()
        {
             List<string> fieldsId = db.Entity(entityName).id;
             foreach(string fieldName in fieldsId)
                if (!values.ContainsKey(fieldName) || values[fieldName].IsNullOrEmpty())
                    return this; //no se reasigna si no esta definido o si es distinto de null

            if (fieldsId.Count == 1)
            {
                values["_Id"] = values[fieldsId[0]].ToString();
                return this;
            }

            List<string> valuesId = new();
            foreach (string fieldName in fieldsId)
                valuesId.Add(values[fieldName].ToString()!);

            values["_Id"] = String.Join(db.config.concatString, valuesId);
            return this;
        }

        /// <summary>
        /// Definir valor por defecto
        /// </summary>
        /// <param name="fieldName">Nombre del field al cual se va a definir valor por defecto</param>
        /// <remarks>Solo se define valor por defecto si el field no se encuentra en atributo values</remarks>
        /// <returns>Mismo objeto</returns>
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

            if (field.defaultValue is null) { 
                values[fieldName] = null;
                return this;
            }

            switch (field.dataType)
            {
                case "string":
                    if (field.defaultValue.ToString().ToLower().Contains("guid"))
                        values[fieldName] = (Guid.NewGuid()).ToString();
                    else
                        values[fieldName] = field.defaultValue;
                    break;
                case "date":
                case "datetime":
                case "year":
                case "time":
                    if (!field.defaultValue.ToString().ToLower().Contains("cur"))
                        values[fieldName] = DateTime.Now;
                    else
                        values[fieldName] = field.defaultValue;
                    break;
                case "int":
                    if (field.defaultValue.ToString().ToLower().Contains("max")) { 
                        int max = db.Query(entityName).Select("MAX($"+fieldName+")").Value <int> ();
                        values[fieldName] = max+1;
                    }
                    else if (field.defaultValue.ToString().ToLower().Contains("next"))
                    {
                        throw new Exception("Not implemented"); //siguiente valor de la secuencia, cada motor debe tener su propia implementacion, definir subclase
                    }
                    else
                    {
                        values[fieldName] = field.defaultValue;
                    }
                    break;

                default:
                    values[fieldName] = field.defaultValue;
                    break;
            }

            return this;
        }


        public bool Check()
        {
            logging.Clear();
            foreach (var fieldName in db.FieldNames(entityName))
                if (values.ContainsKey(fieldName))
                    Check(fieldName);

            return !logging.HasErrors();
        }

        /// <summary>
        /// Validar valor del field
        /// </summary>
        /// <param name="fieldName">Nombre del field a validar</param>
        /// <returns>Resultado de la validacion</returns>
        public bool Check(string fieldName)
        {
            logging.ClearByKey(fieldName);
            var method = "Check_" + fieldName;
            Type thisType = this.GetType();
            MethodInfo? m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
                return (bool)m!.Invoke(this, null);

            Field field = db.Field(entityName, fieldName);
            Validation v = new(Get(fieldName));
            v.Clear();
            foreach (var (checkMethod, param) in field.checks)
            {
                switch (checkMethod)
                {
                    case "type":
                        v.Type((string)param);                
                    break;
                    case "required":
                        if((bool)param)
                            v.Required();
                    break;

                }
            }

            foreach (var error in v.errors)
                logging.AddErrorLog(key: fieldName, type: error.type, msg: error.msg);

            return !v.HasErrors();
        }
    }

}
