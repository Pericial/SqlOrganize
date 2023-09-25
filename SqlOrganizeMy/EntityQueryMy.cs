using MySql.Data.MySqlClient;
using SqlOrganize;
using System.Data.Common;
using System.Net.NetworkInformation;
using Utils;

namespace SqlOrganizeMy
{
    public class EntityQueryMy : EntityQuery
    {

        public EntityQueryMy(Db db, string entityName) : base(db, entityName)
        {
        }

        protected override string SqlLimit()
        {
            if (size == 0) return "";
            page = page == 0 ? 1 : page;
            return "LIMIT " + size + " OFFSET " + ((page - 1) * size) + @"
";
        }

        protected override string SqlOrder()
        {
            if (order.IsNullOrEmpty())
            {
                var o = Db.Entity(entityName).orderDefault;
                order = o.IsNullOrEmpty() ? "" : string.Join(", ", o.Select(x => "$" + x));
            }

            return (order.IsNullOrEmpty()) ? "" : "ORDER BY " + Traduce(order!) + @"
";
        }
        

        public override EntityQuery Clone()
        {
            var eq = new EntityQueryMy(Db, entityName);
            return _Clone(eq);
        }
    }

}