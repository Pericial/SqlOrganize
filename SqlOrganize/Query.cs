using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
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
        public List<object> parameters { get; set; }  = new List<object>();

        /// <summary>
        /// Parametros de las consultas
        /// </summary>
        public Dictionary<string, object> parametersDict { get; set; } = new ();

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
        public abstract IEnumerable<Dictionary<string, object>> ColOfDict();

        public abstract IEnumerable<T> ColOfObj<T>() where T : class, new();

        public abstract IDictionary<string, object> Dict();
        public abstract T Obj<T>() where T : class, new();

        public abstract IEnumerable<T> Column<T>(string columnName);

        public abstract IEnumerable<T> Column<T>(int columnValue = 0);

        public abstract T Value<T>(string columnName);

        public abstract T Value<T>(int columnValue = 0);
        
        /// <summary>
        /// Ejecutar como transacción
        /// </summary>
        /// <remarks>
        /// Incorpora las sentencias BEGIN y COMMIT (O ROLLBACK en caso de falla)
        /// </remarks>
        public abstract void Transaction();

        public abstract void Exec();

        protected abstract void AddWithValue(DbCommand command, string columnName, object value);

        protected void SqlExecute(DbConnection connection, DbCommand command)
        {
            connection.Open();
            command.Connection = connection;

            if (parametersDict.Keys.Count > 0)
            {
                //debe recorrerse de forma ordenada por longitud, si un campo se llama "persona" y otro "persona_adicional"  y no se recorre ordenado descendiente, el resultado es erroneo.
                var keys = parametersDict.Keys.SortByLength("DESC");

                var j = parameters.Count;

                foreach (string key in keys)
                    while (sql.Contains("@" + key))
                    {
                        sql = sql.ReplaceFirst("@" + key, "@" + j.ToString());
                        parameters.Add(parametersDict[key]);
                        j++;
                    }
            }

            for (var i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].IsList())
                {
                    //cuidado con el tipo de entrada, no se puede hacer cast de List<string> a List<object> por ejemplo
                    var _parameters = (parameters[i] as List<object>).Select((x, j) => Tuple.Create($"@{i}_{j}", x));
                    sql = sql.ReplaceFirst("@" + i.ToString(), string.Join(",", _parameters.Select(x => x.Item1)));
                    foreach (var parameter in _parameters)
                        AddWithValue(command, parameter.Item1, parameter.Item2);
                }
                else
                {
                    var p = (parameters[i] == null) ? DBNull.Value : parameters[i];
                    AddWithValue(command, i.ToString(), p);
                }
            }

            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
    }

}
 
