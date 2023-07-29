using SqlOrganize;

namespace SqlOrganizeMy
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
    public class ValuesMy : EntityValues
    {
        public ValuesMy(Db db, string entityName, string? fieldId = null) : base(db, entityName, fieldId)
        {
        }

        

    }
}