using System.Reflection;
using Utils;

namespace SqlOrganize
{
    public class EntityOptions
    {
        public Db db { get; }
        public string entityName { get; }

        public string fieldId { get; }

        public EntityOptions(Db _db, string _entityName, string _fieldId)
        {
            db = _db;
            entityName = _entityName;
            fieldId = _fieldId;
        }

        public string Pf()
        {
            return (fieldId.IsNullOrEmpty()) ? fieldId + "-" : "";
        }

        public string Pt()
        {
            return (!fieldId.IsNullOrEmpty()) ? fieldId : db.Entity(entityName).alias;
        }

        /*
        Recorrer fields y ejecutar metodo indicado

        Los metodos posibles para ejecucion no deben llevar otro parametro mas que el field_name
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
        Ejecutar metodo indicado para field_names de la entidad

        Los metodos posibles para ejecucion no deben llevar otro parametro mas que el field_name
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

        public EntityOptions FromFields(Dictionary<string, object> row, List<string> fieldNames, string method)
        {
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method)!;

            if (!row.IsNullOrEmpty())
                foreach (var field_name in fieldNames)
                    if (row.ContainsKey(Pf()+field_name))
                    {
                        object r = m.Invoke(this, new object[2] { field_name, row[Pf() + field_name] });
                    }

            return this;
        }
        public EntityOptions from(Dictionary<string, object> row, string method)
        {
            return FromFields(row, db.FieldNames(entityName), method);
        }

    }
}