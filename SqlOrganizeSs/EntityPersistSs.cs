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

        public override EntityPersist Exec()
        {
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            using SqlCommand command = new SqlCommand();
            SqlExecute(connection, command);
            using SqlDataReader reader = command.ExecuteReader();
            return this;

        }

        protected void SqlExecute(SqlConnection connection, SqlCommand command)
        {
            connection.Open();
            string sql = @"BEGIN TRAN; 
" + Sql() + @"
COMMIT TRAN;";
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


        protected override EntityPersist _Update(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;
            Entity e = db.Entity(_entityName);
            sql += @"
UPDATE " + e.alias + @" SET
";
            List<string> fieldNames = db.FieldNamesAdmin(_entityName);
            foreach (string fieldName in fieldNames)
                if (row.ContainsKey(fieldName))
                {
                    sql += fieldName + " = @" + count + ", ";
                    count++;
                    parameters.Add(row[fieldName]);
                }
            sql = sql.RemoveLastChar(',');
            sql += " FROM " + e.schemaNameAlias + @"
";
            return this;
        }

    }

}
