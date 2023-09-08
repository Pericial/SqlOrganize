using SqlOrganize;
using Utils;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace SqlOrganizeSs
{
    public class EntityQuerySs : EntityQuery
    {

        public EntityQuerySs(Db db, string entity_name) : base(db, entity_name)
        {
        }

        protected override string SqlLimit()
        {
            if (size.IsNullOrEmpty() || size == 0) return "";
            page = page.IsNullOrEmpty() ? 1 : page;
            return "OFFSET " + ((page - 1) * size) + @" ROWS
FETCH FIRST " + size + " ROWS ONLY";
        }

        protected override string SqlOrder()
        {
            if (order.IsNullOrEmpty())
            {
                var o = db.Entity(entityName).orderDefault;
                order = o.IsNullOrEmpty() ? "" : string.Join(", ", o.Select(x => "$" + x));
            }

            return ((order.IsNullOrEmpty()) ? "ORDER BY 1" : "ORDER BY " + Traduce(order!)) + @"
";
        }

        protected override string SqlFields()
        {
            if (this.fields.IsNullOrEmpty() && this.select.IsNullOrEmpty() && this.group.IsNullOrEmpty())
                this.Fields();

            string f = TraduceFields(this.fields);

            f += Concat(Traduce(this.select), @",
", "", !f.IsNullOrEmpty());

            f += Concat(Traduce(this.group, true), @",
", "", !f.IsNullOrEmpty());


            string o = order.Replace("ASC", "").Replace("asc", "").Replace("DESC", "").Replace("desc", "");
            f += Concat(Traduce(o, true), @",
", "", !f.IsNullOrEmpty());


            return f + @"
";
        }

        public override EntityQuery Clone()
        {
            var eq = new EntityQuerySs(db, entityName);
            return _Clone(eq);
        }
    }
}
