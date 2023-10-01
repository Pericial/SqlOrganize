
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

        public IDictionary<string, object?> values = new Dictionary<string, object?>();

        public EntityValues(Db _db, string _entityName, string? _fieldId = null) : base(_db, _entityName, _fieldId)
        {
        }

        public EntityValues Values(IDictionary<string, object> row)
        {
            values = row;
            return this;
        }

        public EntityValues Set(IDictionary<string, object> row)
        {
            foreach (var fieldName in db.FieldNames(entityName))
                if (row.ContainsKey(Pf() + fieldName))
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

        public object? GetOrNull(string fieldName)
        {
            return (values.ContainsKey(fieldName) && !values[fieldName].IsNullOrEmptyOrDbNull()) ?
                 values[fieldName] : null;

        }



        public IDictionary<string, object> Get()
        {
            Dictionary<string, object> response = new();
            foreach (var fieldName in db.FieldNames(entityName))
                if (values.ContainsKey(fieldName))
                    response[Pf() + fieldName] = values[fieldName];

            return response;
        }

        /// <summary>
        /// Retornar formato SQL
        /// </summary>
        /// <param name="fieldName">n</param>
        /// <returns>Formato SQL para el fieldName</returns>
        /// <remarks>La conversion de formato es realizada directamente por la libreria SQL, pero para ciertos casos puede ser necesario <br/></remarks>
        public object Sql(string fieldName)
        {
            if (!values.ContainsKey(fieldName))
                throw new Exception("Se esta intentando obtener valor de un campo no definido");

            var value = values[fieldName];

            if (value == null)
                return "null";
            
            Field field = db.Field(entityName, fieldName);

            switch (field.dataType) //solo funciona para tipos especificos, para mapear correctamente deberia almacenarse en field, el tipo original sql.
            {
                case "string":
                    return "'" + (string)value + "'";

                case "DateTime": //puede que no funcione correctamente, es necesario almacenar el tipo original sql
                    return "'" + ((DateTime)value).ToString("u");

                default:
                    return value;

            }

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
                    if(value is bool)
                        values[fieldName] = (bool)value;
                    else 
                        values[fieldName] = (value as string).ToBool();
                    break;
                case "date":
                    throw new NotImplementedException();
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

            if (values.ContainsKey(db.config.id))
                Reset(db.config.id);

            return this;
        }

        /// <summary>
        /// Reasigna fieldName
        /// </summary>
        /// <param name="fieldName"></param>
        /// <remarks>fieldName debe estar definido obligatoriamente</remarks>
        /// <returns></returns>
        public EntityValues Reset(string fieldName)
        {
            var method = "Reset_" + fieldName;
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
            {
                m!.Invoke(this, new object[] { });
                return this;
            }
            Field field = db.Field(entityName, fieldName);

            foreach (var (resetKey, resetValue) in field.resets)
            {
                switch (resetKey)
                {
                    case "trim":
                        if (!values[fieldName].IsNullOrEmpty() && !values[fieldName].IsDbNull())
                            values[fieldName] = ((string)values[fieldName]).Trim(((string)resetValue).ToChar());
                        break;
                    case "removeMultipleSpaces":
                        if (!values[fieldName].IsNullOrEmpty() && !values[fieldName].IsDbNull())
                            values[fieldName] = Regex.Replace((string)values[fieldName], @"\s+", " ");
                        break;
                    case "nullIfEmpty":
                        if (values[fieldName].IsNullOrEmpty())
                            values[fieldName] = null;
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
        /// Fuerza la asignacion de valor por defecto
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public EntityValues SetDefault(string fieldName)
        {
            if (values.ContainsKey(fieldName))
                Remove(fieldName);
            return Default(fieldName);
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
            foreach (string fieldName in fieldsId)
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
            if (!m.IsNullOrEmpty()) {
                m!.Invoke(this, new object[] { });
                return this;
            }

            Field field = db.Field(entityName, fieldName);

            if (field.defaultValue is null)
            {
                values[fieldName] = null;
                return this;
            }

            switch (field.dataType)
            {
                case "string":
                    if (field.defaultValue.ToString().ToLower().Contains("guid"))
                        values[fieldName] = (Guid.NewGuid()).ToString();

                    //generate random strings
                    else if (field.defaultValue.ToString()!.ToLower().Contains("random"))
                    {
                        string param = field.defaultValue.ToString()!.SubstringBetween("(", ")");
                        values[fieldName] = ValueTypesUtils.RandomString(Int32.Parse(param));
                    }
                    else
                        values[fieldName] = field.defaultValue;
                    break;
                case "DateTime":
                    if (field.defaultValue.ToString().ToLower().Contains("cur") ||
                        field.defaultValue.ToString().ToLower().Contains("getdate")
                        )
                        values[fieldName] = DateTime.Now;
                    else
                        values[fieldName] = field.defaultValue;
                    break;
                
                case "sbyte":
                case "byte":
                case "short":
                case "ushort":
                case "int":
                case "uint":
                case "long":
                case "ulong":
                case "nint":
                case "nuint":
                    if (field.defaultValue.ToString().ToLower().Contains("next"))
                    {
                        ulong next = GetNextValue(field);
                      
                        values[fieldName] = next;
                    }
                    else if (field.defaultValue.ToString().ToLower().Contains("max"))
                    {
                        long max = db.Query(entityName).Select("MAX($" + fieldName + ")").Value<long>();
                        values[fieldName] = max + 1;
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
                        if ((bool)param)
                            v.Required();
                        break;

                }
            }

            foreach (var error in v.errors)
                logging.AddErrorLog(key: fieldName, type: error.type, msg: error.msg);

            return !v.HasErrors();
        }

        public EntityValues SetNotNull(IDictionary<string, object> row)
        {
            foreach (var fieldName in db.FieldNames(entityName))
                if (row.ContainsKey(Pf() + fieldName))
                    if(row[Pf() + fieldName] != null && !row[Pf() + fieldName].IsDbNull())
                        Set(fieldName, row[Pf() + fieldName]);

            return this;
        }

        public IDictionary<string, object> Compare(EntityValues val, IEnumerable<string>? ignoreFields = null, bool ignoreNull = true, bool ignoreNonExistent = true)
        {
            return Compare(val.values!, ignoreFields, ignoreNull, ignoreNonExistent);
        }


        /// <summary>
        /// Comparar valores con los indicados en parametro
        /// </summary>
        /// <param name="values">Valores externos a persistir<</param>
        /// <returns>Valores del parametro que son diferentes o que no estan definidos localmente</returns>
        /// <remarks>Solo compara fieldNames</remarks>
        public virtual IDictionary<string, object> Compare(IDictionary<string, object> val, IEnumerable<string>? ignoreFields = null, bool ignoreNull = true, bool ignoreNonExistent = true)
        {
            Dictionary<string, object> dict1_ = new(this.values);
            Dictionary<string, object> dict2_ = new(val);
            Dictionary<string, object> response = new();


            if (!ignoreFields.IsNullOrEmpty())
                foreach (var key in ignoreFields)
                {
                    dict1_.Remove(key);
                    dict2_.Remove(key);
                }

            foreach (var fieldName in db.FieldNames(entityName)) { 
                if (ignoreNonExistent && !dict1_.ContainsKey(fieldName))
                    continue;

                if (dict2_.ContainsKey(fieldName) && (ignoreNull && dict2_[fieldName] != null && !dict2_[fieldName].IsDbNull()))
                    if (
                        !dict1_.ContainsKey(fieldName) 
                        || !dict1_[fieldName].ToString().Equals(dict2_[fieldName].ToString())
                    )
                        response[fieldName] = dict2_[fieldName];
            }
            return response;
        }


        /// <summary>
        /// Obtener siguiente valor de la secuencia para mysql
        /// </summary>
        /// <remarks>Esta implementación funciona en mysql, llevar a subclase</br>
        /// Siempre devuelve el siguiente valor de la secuencia sin incrementar, si se utiliza en multiples transacciones de inserción consultar una sola vez e incrementar valor</remarks>
        /// <param name="field"></param>
        /// <returns></returns>
        public ulong GetNextValue(Field field)
        {
            var q = db.Query();
            q.sql = @"
                            SELECT auto_increment 
                            FROM INFORMATION_SCHEMA.TABLES 
                            WHERE TABLE_NAME = @0";
            q.parameters.Add(field.entityName);
            return q.Value<ulong>();
        }

        public EntityValues Update()
        {
            db.Persist(entityName).Update(values!).Exec();
            return this;
        }

        public EntityValues Update(EntityPersist persist)
        {
            persist.Update(this);
            return this;
        }

        public EntityValues Insert()
        {
            db.Persist(entityName).Insert(values!).Exec();
            return this;
        }

        public EntityValues Insert(EntityPersist persist)
        {
            persist.Insert(this);
            return this;
        }

    
        public EntityValues? ValuesTree(string fieldId)
        {
            Entity entity = db.Entity(entityName);
            EntityTree tree = entity.tree[fieldId];
            object? val = GetOrNull(tree.fieldName);
            if (!val.IsNullOrEmpty())
            {
                var data = db.Query(tree.refEntityName)._CacheById(val);
                return db.Values(tree.refEntityName).Set(data);
            }
            return null;
        }

        public override string ToString()
        {
            List<string> fieldNames = ToStringFields();

            var label = "";
            foreach(string fieldName in fieldNames)
                label += GetOrNull(fieldName)?.ToString() ?? " ";

            return label.RemoveMultipleSpaces().Trim();
        }

        protected List<string> ToStringFields()
        {
            var entity = db.Entity(entityName);
            List<string> fields = new();
            foreach (string f in entity.unique)
                if (entity.notNull.Contains(f))
                    fields.Add(f);

            bool uniqueMultipleFlag = true;
            foreach (List<string> um in entity.uniqueMultiple)
            {
                foreach (string f in um)
                    if (!entity.notNull.Contains(f))
                    {
                        uniqueMultipleFlag = false;
                        break;
                    }

                if (uniqueMultipleFlag)
                    foreach (var f in um)
                        fields.Add(f);

                uniqueMultipleFlag = true;
            }

            if (fields.IsNullOrEmpty())
                fields = entity.notNull;

            if (fields.IsNullOrEmpty())
                fields = entity.fields;

            return fields;
        }


    }

   
}
