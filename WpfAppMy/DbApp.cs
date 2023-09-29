using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using SqlOrganizeMy;
using WpfAppMy.Values;

namespace WpfAppMy
{
    internal class DbApp : DbMy
    {
        public DbApp(Config config, Model model, MemoryCache cache) : base(config, model, cache)
        {
        }

        public override EntityValues Values(string entityName, string? fieldId = null)
        {
            switch (entityName)
            {
                case "alumno":
                    return new Alumno(this, entityName, fieldId);

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
