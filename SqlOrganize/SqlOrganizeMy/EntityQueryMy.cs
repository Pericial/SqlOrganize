using SqlOrganize;
using System.Data.Common;
using Utils;

namespace SqlOrganizeMy
{
    public class EntityQueryMy : EntityQuery
    {

        public EntityQueryMy(Db db, string entityName) : base(db, entityName)
        {
        }

        public override List<Dictionary<string, object>> All()
        {
            throw new NotImplementedException();
        }

        public override List<Dictionary<string, object>> Tree()
        {
            throw new NotImplementedException();
        }

        protected override string sql_limit()
        {
            if (size.IsNullOrEmpty()) return "";
            page = page.IsNullOrEmpty() ? 1 : page;
            return "LIMIT " + size + " OFFSET " + ((page - 1) * size) + @"
";
        }

        protected override string sql_order()
        {
            if (order.IsNullOrEmpty())
            {
                var o = db.Entity(entityName).orderDefault;
                order = o.IsNullOrEmpty() ? "" : string.Join(", ", o.Select(x => "$" + x));
            }

            return (order.IsNullOrEmpty()) ? "" : "ORDER BY " + traduce(order!) + @"
";
        }
    }

}