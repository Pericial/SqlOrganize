using SqlOrganize;
using Utils;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace SqlOrganizeSs
{
    public class EntityValuesSs : EntityValues
    {
        public EntityValuesSs(Db db, string entityName, string fieldId) : base(db, entityName, fieldId)
        {
        }

        public ulong GetNextValue(Field field)
        {
            throw new NotImplementedException();
        }
    }

}
