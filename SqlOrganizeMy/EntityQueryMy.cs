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

        public override List<Dictionary<string, T>> Tree<T>()
        {
            throw new NotImplementedException();
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
                var o = db.Entity(entityName).orderDefault;
                order = o.IsNullOrEmpty() ? "" : string.Join(", ", o.Select(x => "$" + x));
            }

            return (order.IsNullOrEmpty()) ? "" : "ORDER BY " + Traduce(order!) + @"
";
        }

        public override List<Dictionary<string, object>> ListDict()
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.ListDict();
        }

        public override List<T> ListObject<T>()
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.ListObj<T>();
        }

        public override Dictionary<string, object> Dict()
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Dict();
        }

        public override T Object<T>()
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Obj<T>();

        }

        public override List<T> Column<T>(string columnName)
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Column<T>(columnName);
        }

        public override List<T> Column<T>(int columnValue = 0)
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Column<T>(columnValue);
        }
        public override T Value<T>(string columnName)
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Value<T>(columnName);
        }

        public override T Value<T>(int columnValue = 0)
        {
            var q = db.Query();
            q.sql = Sql();
            q.parameters.AddRange(parameters);
            return q.Value<T>(columnValue);
        }

        public override EntityQuery Clone()
        {
            var eq = new EntityQueryMy(db, entityName);
            return _Clone(eq);
        }
    }

}