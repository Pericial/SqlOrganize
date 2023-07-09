using Microsoft.Data.SqlClient;
using SqlOrganize;

namespace SqlOrganizeSs
{
    /*
    Comentarios del DBMS
    - Sql Server agrega espacios en blanco adicionales cuando se utiliza CONCAT y CONCAT_WS.
    - CONCAT_WS agrega 3 espacios en blanco al final.
    - CONCAT agrega espacios en blanco por cada concatenacion.
    */
    public class DbSs : Db
    {
        SqlConnection _conn;

        /*
         * config["connection_string"] = "server=127.0.0.1;uid=root;pwd=12345;database=test"
         */
        public DbSs(Config config) : base(config)
        {
        }

        public SqlConnection conn() => _conn;

        public override EntityPersist Persist(string entityName)
        {
            return new EntityPersistSs(this, entityName);
        }

        public override EntityQuery Query(string entity_name)
        {
            return new EntityQuerySs(this, entity_name);
        }

        public override EntityValues Values(string entity_name, string field_id)
        {
            return new ValuesSs(this, entity_name, field_id);
        }
    }
}