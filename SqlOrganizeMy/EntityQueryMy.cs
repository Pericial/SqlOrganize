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

        protected void SqlExecute(MySqlConnection connection, MySqlCommand command)
        {
            connection.Open();
            string sql = Sql();
            command.Connection = connection;
            for (var i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].IsList())
                {
                    var _parameters = (parameters[i] as List<object>).Select((x, j) => Tuple.Create($"@{i}_{j}", x));
                    sql = sql.ReplaceFirst("@" + i.ToString(), string.Join(",", _parameters.Select(x => x.Item1)));
                    foreach (var parameter in _parameters)
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                }
                else
                {
                    command.Parameters.AddWithValue(i.ToString(), parameters[i]);
                }
            }

            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public override List<Dictionary<string, object>> ListDict()
        {
            return db.Query().Sql(Sql()).Parameters(Parameters()).ListDict();
        }

        public override List<T> ListObject<T>()
        {
            return db.Query().Sql(Sql()).Parameters(Parameters()).ListObject<T>();
        }

        public override Dictionary<string, object> Dict()
        {
            return db.Query().Sql(Sql()).Parameters(Parameters()).Dict();
        }

        public override T Object<T>()
        {
            return db.Query().Sql(Sql()).Parameters(Parameters()).Object<T>();

        }

        public override List<T> Column<T>(string columnName)
        {
            return db.Query().Sql(Sql()).Parameters(Parameters()).Column<T>(columnName);
        }

        public override List<T> Column<T>(int columnValue = 0)
        {
            return db.Query().Sql(Sql()).Parameters(Parameters()).Column<T>(columnValue);
        }
        public override T Value<T>(string columnName)
        {
            return db.Query().Sql(Sql()).Parameters(Parameters()).Value<T>(columnName);
        }

        public override T Value<T>(int columnValue = 0)
        {
            return db.Query().Sql(Sql()).Parameters(parameters).Value<T>(columnValue);
        }

        public override EntityQuery Clone()
        {
            var eq = new EntityQueryMy(db, entityName);
            return _Clone(eq);
        }
    }

}