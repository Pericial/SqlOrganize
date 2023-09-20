using SqlOrganize;
using SqlOrganizeMy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMy.Values;

namespace WpfAppMy
{
    internal class DbApp : DbMy
    {
        public DbApp(Config config) : base(config)
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
            }

            return new EntityValues(this, entityName, fieldId);

        }
    }
}
