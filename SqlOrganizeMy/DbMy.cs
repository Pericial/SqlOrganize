
using Microsoft.Extensions.Caching.Memory;
using MySql.Data.MySqlClient;
using SqlOrganize;

namespace SqlOrganizeMy
{
    /// <summary>
    /// Contenedor principal para mysql
    /// </summary>
    public class DbMy : Db
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config">
        /// Configuracion
        /// </param>
        /// <example>
        ///   connectionString = "server=127.0.0.1;uid=root;pwd=12345;database=test"
        /// </example>
        public DbMy(Config config, Model model, MemoryCache? cache = null) : base(config, model, cache)
        {
            /*
            prueba de conexion
            Las conexiones se realizan directamente cuando se requiere la eje-
            cucion de una consulta a la base de datos.
            Este codigo esta como referencia, se deja como ejemplo por si es
            necesario verificar la conexion                       
            var conn = new MySqlConnection();
            conn.ConnectionString = (string)config.connectionString;
            conn.Open();
            conn.Close();
            */
        }

        public override EntityPersist Persist(string entityName)
        {
            return new EntityPersistMy(this, entityName);
        }

        public override EntityQuery Query(string entity_name)
        {
            return new EntityQueryMy(this, entity_name);
        }

        public override QueryMy Query()
        {
            return new QueryMy(this);
        }
    }
}
