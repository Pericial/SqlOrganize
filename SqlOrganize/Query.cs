using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace SqlOrganize
{
    /// <summary>
    /// Unificar metodos para ejecutar consultas a la base de datos
    /// </summary>    
    public abstract class Query
    {
        /// <summary>
        /// Contenedor principal del proyecto
        /// </summary>
        public Db db { get; }

        /// <summary>
        /// Parametros de las consultas
        /// </summary>
        public List<object> parameters { get; set; }  = new List<object> { };

        /// <summary>
        /// Consultas en SQL
        /// </summary>
        public string sql { get; set; } = "";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_db">Contenedor principal del proyecto</param>
        public Query(Db _db)
        {
            db = _db;
        }

        /// <summary>
        /// Ejecutar sql y devolver resultado
        /// </summary>
        /// <returns>Resultado como List -Dictionary -string, object- -</returns>
        /// <remarks>Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"</remarks>
        public abstract List<Dictionary<string, object>> ListDict();

        public abstract List<T> ListObj<T>() where T : class, new();

        public abstract Dictionary<string, object> Dict();
        public abstract T Obj<T>() where T : class, new();

        public abstract List<T> Column<T>(string columnName);

        public abstract List<T> Column<T>(int columnValue = 0);

        public abstract T Value<T>(string columnName);

        public abstract T Value<T>(int columnValue = 0);
        
        /// <summary>
        /// Ejecutar como transacción
        /// </summary>
        /// <remarks>
        /// Incorpora las sentencias BEGIN y COMMIT (O ROLLBACK en caso de falla)
        /// </remarks>
        public abstract void Transaction();

    }

}
 