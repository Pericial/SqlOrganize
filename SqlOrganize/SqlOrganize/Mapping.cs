using Microsoft.VisualBasic;
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
        mapping.map("fecha_alta.max.y"); //aplicar max, luego y
        mapping.map("edad.avg")
        */
        public string map(string fieldName)
        {
            List<string> p = fieldName.Split('.').ToList();

            if(p.Count == 1) {
                return _default(fieldName);
            }

            string method = p[p.Count - 1]; //se traduce el metodo ubicado mas a la derecha (el primero en traducirse se ejecutara al final)
            p.RemoveAt(p.Count - 1);
            switch (method)
            {
                case "count":
                    return _count(String.Join(".", p.ToArray()));

                default:
                    return this.map(String.Join(".", p.ToArray()));
            }
        }

        public string id()
        {
            List<string> map_ = new();
            foreach(string f in db.Entity(entityName).pk)
                map_.Add(map(f));
           
            if (map_.Count == 1)
            {
                return map_[0]; 
            }

            return "CONCAT_WS(\"-\"," + String.Join(",", map_) + ") ";
        }

        /*
        mapeo por defecto
        */
        public abstract string _default(string field_name);

        public abstract string _count(string field_name);


    }
}