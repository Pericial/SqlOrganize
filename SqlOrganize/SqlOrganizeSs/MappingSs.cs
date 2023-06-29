using SqlOrganize;

namespace SqlOrganizeSs
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
    public class MappingSs : Mapping
    {
        public MappingSs(Db _db, string _entity_name, string _field_id) : base(_db, _entity_name, _field_id)
        {
        }

        public override string _Default(string field_name) {
            return Pt() + "." + field_name;
        }

        public override string _Count(string field_name)
        {
            return "COUNT(DISTINCT " +  map(field_name) + ")";
        }

    }
}