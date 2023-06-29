using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Utils;

namespace SqlOrganize
{
    /*
    Mapear campos para que sean entendidos por el motor de base de datos.

    Define SQL, cada motor debe tener su propia clase mapping de forma tal que
    sea traducido de forma correcta.

    Ejemplo de subclase opcional:

    -class ComisionMapping: Mapping:
        def numero(self):
           return '''
    CONCAT("+self.pf()+"sed.numero, "+self.pt()+".division)
'''

    Las subclases deben soportar la sintaxis del motor que se encuentran utilizando.
    */
    public abstract class Mapping : EntityOptions
    {
        public Mapping(Db _db, string _entityName, string _fieldId) : base(_db, _entityName, _fieldId)
        {
        }

        /*
        Realizar mapeo de field_name

        Verifica la existencia de metodo eclusivo, si no exite, buscar metodo
        predefinido.

        # ejemplo
        mapping.map("nombre")
        mapping.map("fecha_alta.max.y"); //aplicar y, luego max CAST_TO_DATE(MAX(fecha_alta));
        mapping.map("fecha_alta.y.count"); //aplicar count, luego y COUNT(CAST_TO_DATE(fecha_alta));
        mapping.map("edad.avg.max")
        */
        public string map(string fieldName)
        {
            //invocar metodo local, si existe
            string method = fieldName.Replace(".", "_");
            method = char.ToUpper(method[0]) + method.Substring(1).ToLower();
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(method);
            if (!m.IsNullOrEmpty())
                return (string)m!.Invoke(this, Array.Empty<object>())!;

            //invocar metodo general
            List<string> p = fieldName.Split('.').ToList();

            if(p.Count == 1)
                return _Default(fieldName);

            method = p[p.Count-1];
            p.RemoveAt(p.Count-1);

            switch (method)
            {
                case "count":
                    return _Count(String.Join(".", p.ToArray()));

                default: //por defecto se ignora el metodo y se invoca nuevamente
                    return this.map(String.Join(".", p.ToArray()));
            }
        }

        public string Id()
        {
            List<string> map_ = new();
            foreach(string f in db.Entity(entityName).pk)
                map_.Add(map(f));
           
            if (map_.Count == 1)
            {
                return map_[0]; 
            }

            return "CAST(CONCAT_WS('"+ db.config.concatString + "'," + String.Join(",", map_) + ") AS char)";
        }

        /*
        mapeo por defecto
        */
        public abstract string _Default(string field_name);

        public abstract string _Count(string field_name);


    }
}