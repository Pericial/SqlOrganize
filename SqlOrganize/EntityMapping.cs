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

    -class ComisionMapping extiende Mapping
        Metodo numero()
           return '''
    CONCAT("+this.pf()+"this.numero, "+this.pt()+".division)
'''

    Las subclases deben soportar la sintaxis del motor que se encuentran utilizando.
    */
    public class EntityMapping : EntityFieldId
    {
        public EntityMapping(Db db, string entityName, string? fieldId) : base(db, entityName, fieldId)
        {
        }

        /*
        Realizar mapeo de fieldName

        Antes de mapear verifica la existencia de metodo "exclusivo".
        Map se invoca solo con fieldName, fieldId asignado en constructor.

        # ejemplo
        Db.mapping("persona").map("nombre") //correcto se mapea sin fieldId
        Db.mapping("persona", "persona).map("nombre") //correcto se mapea con fieldId
        Db.mapping("alumno").map("persona-nombre") //error, la traduccion de fieldId se hace en otro nivel

        
        */
        public string Map(string fieldName)
        {
            //invocar metodo local, si existe
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(fieldName);
            if (!m.IsNullOrEmpty())
                return (string)m!.Invoke(this, Array.Empty<object>())!;

            //invocar metodo general
            return _Map(fieldName);
        }

        /*
        Para sql server se debe aplicar trim porque agrega espacios adicionales
        cuidado de no generar strings mayores a 255 
        */
        public string _Id()
        {
            List<string> map_ = new();
            
            foreach (string f in db.Entity(entityName).pk)
                map_.Add(Map(f));

            if (map_.Count == 1)
                return "TRIM(CAST(" + map_[0] + " AS varchar(255)))";


            return "TRIM(CAST(CONCAT_WS('"+ db.config.concatString + "'," + String.Join(",", map_) + ") AS varchar(255)))";
        }

        protected string _Map(string fieldName)
        {
            return Pt() + "." + fieldName;
        }

    }
}
