using SqlOrganize;
using Utils;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace SqlOrganizeSs
{
    public class EntityPersistSs : EntityPersist
    {

        public EntityPersistSs(Db db, string entity_name) : base(db, entity_name)
        {
        }

        public override void Exec()
        {
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            using SqlCommand command = new SqlCommand();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();

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
    }

}