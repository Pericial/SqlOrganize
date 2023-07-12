
using MySql.Data.MySqlClient;
using SqlOrganize;

namespace SqlOrganizeMy
{

    public class DbMy : Db
    {

        MySqlConnection _conn;

        /*
         * config["connection_string"] = "server=127.0.0.1;uid=root;pwd=12345;database=test"
         */
        public DbMy(Config config): base(config)
        {
            _conn = new MySqlConnection();
            _conn.ConnectionString = (string)config.connectionString;
            _conn.Open();
        }

        public MySqlConnection conn() => _conn;

        public override EntityPersist Persist(string entityName)
        {
            return new EntityPersistMy(this, entityName);
        }

        public override EntityQuery Query(string entity_name)
        {
            return new EntityQueryMy(this, entity_name);
        }

        public override EntityValues Values(string entityName, string? fieldId = null)
        {
            return new ValuesMy(this, entityName, fieldId);
        }
    }

}