using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using SqlOrganizeMy;
using WpfAppMy.Values;

namespace WpfAppMy
{
    internal class DbApp : DbMy
    {
        public DbApp(Config config, MemoryCache cache) : base(config, cache)
        {
        }

        public override EntityValues Values(string entityName, string? fieldId = null)
        {
            switch (entityName)
            {
                case "alumno_comision":
                    return new AlumnoComision(this, entityName, fieldId);

                case "domicilio":
                    return new Domicilio(this, entityName, fieldId);

                case "comision":
                    return new Comision(this, entityName, fieldId);

                case "persona":
                    return new Persona(this, entityName, fieldId);
            }

            return new EntityValues(this, entityName, fieldId);

        }
    }
}
