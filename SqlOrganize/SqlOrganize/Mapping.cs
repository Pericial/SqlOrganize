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
    public class Mapping : EntityOptions
    {
        public Mapping(Db _db, string _entityName, string _fieldId) : base(_db, _entityName, _fieldId)
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
        public string map(string fieldName)
        {
            //invocar metodo local, si existe
            Type thisType = this.GetType();
            MethodInfo m = thisType.GetMethod(fieldName);
            if (!m.IsNullOrEmpty())
                return (string)m!.Invoke(this, Array.Empty<object>())!;

            //invocar metodo general
            return _Map(fieldName);
        }

        public string _Id()
        {
            List<string> map_ = new();
            
            foreach (string f in db.Entity(entityName).pk)
                map_.Add(map(f));

            if (map_.Count == 1)
                return map_[0];


            return "CAST(CONCAT_WS('"+ db.config.concatString + "'," + String.Join(",", map_) + ") AS char)";
        }

        protected string _Map(string fieldName)
        {
            return Pt() + "." + fieldName;
        }

    }
}