using System.Reflection;
using Utils;

namespace SqlOrganize
{
    public class EntityOptions
    {
        public Db db { get; }
        public string entityName { get; }

        public string? fieldId { get; }

        public EntityOptions(Db _db, string _entityName, string? _fieldId)
        {
            db = _db;
            entityName = _entityName;
            fieldId = _fieldId;
        }

        public string Pf()
        {
            return (!fieldId.IsNullOrEmpty()) ? fieldId! + "-" : "";
        }

        public string Pt()
        {
            return (!fieldId.IsNullOrEmpty()) ? fieldId! : db.Entity(entityName).alias;
        }

        /*
        Recorrer fields y ejecutar metodo indicado

        Los metodos posibles para ejecucion no deben llevar otro parametro mas que el fieldName
        */
        public EntityOptions CallFields(List<string> fieldNames, string method)
        {
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method)!;
            
            foreach (var fieldName in fieldNames)
            {
                m.Invoke(this, new String[1] { fieldName });
            }

            return this;
        }

        /*
        Ejecutar metodo indicado para fieldNames de la entidad

        Los metodos posibles para ejecucion no deben llevar otro parametro mas que el fieldName
        */
        public EntityOptions Call(string method)
        {
            return CallFields(db.FieldNames(entityName), method);
        }
        
        /*
        Ejecutar metodo y almacenar valores en un diccionario
        */
        public Dictionary<string, object> to_fields(List<string> fieldNames, string method)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method)!;

            foreach (var fieldName in fieldNames)
            {
                object r = m.Invoke(this, new String[1] { fieldName });
                    row[fieldName] = r;
                
            }

            return row;
        }

        public Dictionary<string, object> to(string method)
        {
            return to_fields(db.FieldNames(entityName), method);
        }

        public EntityOptions FromFields(List<string> fieldNames, Dictionary<string, object> row, string method)
        {
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method)!;

            if (!row.IsNullOrEmpty())
                foreach (var fieldName in fieldNames)
                    if (row.ContainsKey(Pf()+fieldName))
                    {
                        object r = m.Invoke(this, new object[2] { fieldName, row[Pf() + fieldName] });
                    }

            return this;
        }
        public EntityOptions From(Dictionary<string, object> row, string method)
        {
            return FromFields(db.FieldNames(entityName), row, method);
        }

    }
}