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
    /// Facilitar la ejecucion de consultas a la base de datos.
    /// </summary>
    /// <remarks>
    /// Esta pensada para unificar el codigo de ejecucion de consultas en una sola clase.
    /// </remarks>
    public abstract class Query
    {
        public Db db { get; }


        public List<object> parameters { get; set; }  = new List<object> { };

        /// <summary>
        /// Contador de parametros (uso opcional)
        /// </summary>
        public int count = 0;

        public string sql { get; set; } = "";
        public Query(Db _db)
        {
            db = _db;
        }

        public Query Parameters(params object[] parameters)
        {
            this.parameters.AddRange(parameters.ToList());
            return this;
        }

        public Query Sql(string _sql)
        {
            sql += _sql;
            return this;

        }
        public string Sql()
        {
            return sql;
        }

        /*
       Obtener todas las filas

       Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
       */
        public abstract List<Dictionary<string, object>> ListDict();

        public abstract List<T> ListObject<T>() where T : class, new();

        public abstract Dictionary<string, object> Dict();
        public abstract T Object<T>() where T : class, new();

        public abstract List<T> Column<T>(string columnName);

        public abstract List<T> Column<T>(int columnValue = 0);

        public abstract T Value<T>(string columnName);

        public abstract T Value<T>(int columnValue = 0);
        /*
        Obtener arbol

        Convert the result to json with "JsonConvert.SerializeObject(data, Formatting.Indented)"
        */
        public abstract void Transaction();

    }

}
 