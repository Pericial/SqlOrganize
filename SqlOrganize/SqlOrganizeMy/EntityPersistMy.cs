using MySql.Data.MySqlClient;
using SqlOrganize;
using Utils;

namespace SqlOrganizeMy
{
    public class EntityPersistMy : EntityPersist
    {

        public EntityPersistMy(Db db, string entityName) : base(db, entityName)
        {
        }

        public override void Exec()
        {
            using MySqlConnection connection = new MySqlConnection((string)db.config.connectionString);
            using MySqlCommand command = new MySqlCommand();
            SqlExecute(connection, command);
            using MySqlDataReader reader = command.ExecuteReader();

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

        protected override EntityPersist _Update(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;
            string sna = db.Entity(_entityName).schemaNameAlias;
            sql += @"
UPDATE " + sna + @" SET
";
            List<string> fieldNames = db.FieldNamesAdmin(_entityName);
            foreach (string fieldName in fieldNames)
                if (row.ContainsKey(fieldName))
                {
                    sql += fieldName + " = @" + count + ", ";
                    count++;
                    parameters.Add(row[fieldName]);
                }
            sql = sql.RemoveLastIndex(',');
            return this;
        }

    }

}