using System.Data.Common;

namespace SqlOrganize
{
    public class EntityQueryMy : EntityQuery
    {

        public EntityQueryMy(Db db, string entity_name) : base(db, entity_name)
        {
        }

        public override List<Dictionary<string, object>> All()
        {
            throw new NotImplementedException();
        }

        public override List<Dictionary<string, object>> tree()
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
                var o = db.entity(entity_name).order_default;
                order = o.IsNullOrEmpty() ? "" : string.Join(", ", o.Select(x => "$" + x));
            }

            return (order.IsNullOrEmpty()) ? "" : "ORDER BY " + traduce(order!) + @"
";
        }
    }

}