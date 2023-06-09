
using MySql.Data.MySqlClient;

namespace SqlOrganize
{

    public class DbMy : Db
    {

        MySqlConnection _conn;

        /*
         * config["connection_string"] = "server=127.0.0.1;uid=root;pwd=12345;database=test"
         */
        public DbMy(Dictionary<string, object> config): base(config)
        {
            _conn = new MySqlConnection();
            _conn.ConnectionString = (string)config["connection_string"];
            _conn.Open();
        }

        public MySqlConnection conn() => _conn;

        public override EntityQuery query(string entity_name)
        {
            return new EntityQueryMy(this, entity_name);
        }


        public override Mapping mapping(string entity_name, string field_id)
        {
            return new MappingMy(this, entity_name, field_id);
        }

        public override Condition condition(string entity_name, string field_id)
        {
            return new ConditionMy(this, entity_name, field_id);
        }

        public override Values values(string entity_name, string field_id)
        {
            return new ValuesMy(this, entity_name, field_id);
        }
    }

}