using SqlOrganize;
using Utils;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;

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
                var o = Db.Entity(entityName).orderDefault;
                order = o.IsNullOrEmpty() ? "" : string.Join(", ", o.Select(x => "$" + x));
            }

            return ((order.IsNullOrEmpty()) ? "ORDER BY 1" : "ORDER BY " + Traduce(order!)) + @"
";
        }

        /// <summary>
        /// Definir campos a consultar
        /// </summary>
        /// <remarks>En SQL SERVER a diferencia de otros motores, los campos de ordenamiento deben incluirse en los campos a consultar</remarks>
        /// <returns></returns>
        protected override string SqlFields()
        {
            if (this.fields.IsNullOrEmpty() && this.select.IsNullOrEmpty() && this.group.IsNullOrEmpty())
                this.Fields();

            string f = TraduceFields(this.fields);

            f += Concat(Traduce(this.select), @",
", "", !f.IsNullOrEmpty());

            f += Concat(Traduce(this.group, true), @",
", "", !f.IsNullOrEmpty());


            string o = order.Replace("ASC", "").Replace("asc", "").Replace("DESC", "").Replace("desc", "").Trim();
            f += Concat(Traduce(o, true), @",
", "", !f.IsNullOrEmpty());
            var f_aux = f.Split(',').ToList();

            var f_aux_duplicates = f_aux.GroupBy(x => x.Trim().Replace("\n",""))
                        .Where(group => group.Count() > 1)
                        .Select(group => group.Key);


            foreach (var fad in f_aux_duplicates)
                for (var i = 0; i < f_aux.Count; i++)
                    if (fad.Trim().Replace("\n", "").Equals(f_aux[i].Trim().Replace("\n", "")))
                    {
                        f_aux.RemoveAt(i);
                        break;
                    }
            return String.Join(',', f_aux) + @"
";
        }

        public override EntityQuery Clone()
        {
            var eq = new EntityQuerySs(Db, entityName);
            return _Clone(eq);
        }
    }
}
