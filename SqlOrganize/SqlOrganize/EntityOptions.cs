using System.Reflection;

namespace SqlOrganize
{
    public class EntityOptions
    {
        public Db db { get; }
        public string entity_name { get; }

        public string field_id { get; }

        public EntityOptions(Db _db, string _entity_name, string _field_id)
        {
            db = _db;
            entity_name = _entity_name;
            field_id = _field_id;
        }

        public string pf()
        {
            return (field_id.IsNullOrEmpty()) ? field_id + "-" : "";
        }

        public string pt()
        {
            return (!field_id.IsNullOrEmpty()) ? field_id : db.entity(entity_name).alias;
        }

        /*
        Recorrer fields y ejecutar metodo indicado

        Los metodos posibles para ejecucion no deben llevar otro parametro mas que el field_name
        */
        public EntityOptions call_fields(List<string> field_names, string method)
        {
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method)!;
            
            foreach (var field_name in field_names)
            {
                m.Invoke(this, new String[1] {field_name});
            }

            return this;
        }

        /*
        Ejecutar metodo indicado para field_names de la entidad

        Los metodos posibles para ejecucion no deben llevar otro parametro mas que el field_name
        */
        public EntityOptions call(string method)
        {
            return call_fields(db.field_names(entity_name), method);
        }
        
        /*
        Ejecutar metodo y almacenar valores en un diccionario
        */
        public Dictionary<string, object> to_fields(List<string> field_names, string method)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method)!;

            foreach (var field_name in field_names)
            {
                object r = m.Invoke(this, new String[1] { field_name });
                    row[field_name] = r;
                
            }

            return row;
        }

        public Dictionary<string, object> to(string method)
        {
            return to_fields(db.field_names(entity_name), method);
        }

        public EntityOptions from_fields(Dictionary<string, object> row, List<string> field_names, string method)
        {
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method)!;

            if (!row.IsNullOrEmpty())
            {
                foreach (var field_name in field_names)
                {
                    if (row.ContainsKey(pf()+field_name))
                    {
                        object r = m.Invoke(this, new object[2] { field_name, row[pf() + field_name] });
                    }
                }
            }
            

            return this;
        }
        public EntityOptions from(Dictionary<string, object> row, string method)
        {
            return from_fields(row, db.field_names(entity_name), method);
        }

    }
}