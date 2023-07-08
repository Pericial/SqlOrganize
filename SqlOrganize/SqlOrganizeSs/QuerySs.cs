using SqlOrganize;
using Utils;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace SqlOrganizeSs
{
    public class QuerySs : Query
    {

        public QuerySs(Db db, string entity_name) : base(db, entity_name)
        {
        }

        public override List<Dictionary<string, T>> Tree<T>()
        {
            throw new NotImplementedException();
        }

        protected override string SqlLimit()
        {
            if (size.IsNullOrEmpty()) return "";
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

            return ((order.IsNullOrEmpty()) ? "ORDER BY (SELECT NULL)" : "ORDER BY " + Traduce(order!)) + @"
";
        }

        protected void SqlExecute(SqlConnection connection, SqlCommand command)
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
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            using SqlCommand command = new SqlCommand();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();
            return reader.Serialize();
        }

        public override List<T> ListObject<T>()
        {
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            using SqlCommand command = new SqlCommand();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();
            return reader.ConvertToListOfObject<T>();
        }

        public override Dictionary<string, object> Dict()
        {
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            using SqlCommand command = new SqlCommand();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);

            return reader.SerializeRowCols(reader.ColumnNames());
        }

        public override T Object<T>()
        {
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            using SqlCommand command = new SqlCommand();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult);

            return reader.ConvertToObject<T>();
        }

        public override List<T> Column<T>(string columnName)
        {
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            using SqlCommand command = new SqlCommand();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();

            return reader.ColumnValues<T>(columnName);
        }

        public override List<T> Column<T>(int columnValue = 0)
        {
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            using SqlCommand command = new SqlCommand();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();

            return reader.ColumnValues<T>(columnValue);
        }

        public override T Value<T>(string columnName)
        {
            throw new NotImplementedException();
        }

        public override T Value<T>(int columnValue = 0)
        {
            throw new NotImplementedException();
        }

        public override Query Clone()
        {
            var eq = new QuerySs(db, entityName);
            eq.size = size;
            eq.where = where;
            eq.page = page;
            eq.parameters = parameters;
            eq.group = group;
            eq.having = having;
            eq.fields = fields;
            eq.select = select;
            eq.order = order;
            return eq;
        }
    }
}