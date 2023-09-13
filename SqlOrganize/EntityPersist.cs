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
    public abstract class EntityPersist
    {
        public Db db { get; }

        public string? entityName { get; }

        public List<object> parameters { get; set; } = new List<object> { };

        public int count = 0;

        public string sql { get; set; } = "";

        public List<(string entityName, object id)> detail = new();

        public EntityPersist(Db _db, string? _entityName = null)
        {
            db = _db;
            entityName = _entityName;
        }

        public EntityPersist Parameters(params object[] parameters)
        {
            this.parameters.AddRange(parameters.ToList());
            return this;
        }

        abstract protected EntityPersist _Update(Dictionary<string, object> row, string? _entityName = null);

        public EntityPersist Update(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;

            _Update(row, _entityName);
            string id = db.Mapping(_entityName!).Map(db.config.id);
            sql += @"
WHERE " + id + " = @" + count + @";
";
            count++;
            parameters.Add(row[db.config.id]);
            detail.Add((_entityName!, (string)row[db.config.id]));
            return this;
        }

        public EntityPersist UpdateIds(Dictionary<string, object> row, List<object> ids, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;

            _Update(row, _entityName);

            string idMap = db.Mapping(_entityName!).Map(db.config.id);

            if ((ids.Count + count) > 2100) //SQL Server no admite mas de 2100 parametros, se define consulta alternativa para estos casos
            {
                List<object> ids_ = new();
                var v = db.Values(_entityName!);
                foreach (var id in ids)
                {
                    v.Set(db.config.id, id);
                    var id_ = v.Sql(db.config.id);
                    ids_.Add(id_);
                    detail.Add((_entityName!, id));

                }
                sql += @"WHERE " + idMap + " IN (" + String.Join(",", ids_) + @");
";
            } else
            {
                sql += @"WHERE " + idMap + " IN (@" + count + @");
";
                count++;
                parameters.Add(ids);

                foreach (var id in ids)
                    detail.Add((_entityName!, id));
            }
            
            return this;
        }

        /// <summary>
        /// Actualizar valores de todas las entradas de una tabla
        /// </summary>
        /// <param name="row"></param>
        /// <param name="_entityName"></param>
        /// <remarks>USAR CON PRECAUCIÓN!!!</remarks>
        /// <returns></returns>
        public EntityPersist UpdateAll(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;
            var ids = db.Query(_entityName).Fields(db.config.id).Size(0).Column<object>();
            return (ids.Count > 0) ? UpdateIds(row, ids, _entityName) : this;
        }

        /// <summary>
        /// Actualizar un unico campo
        /// </summary>
        /// <param name="key">Nombre del campo a actualizar</param>
        /// <param name="value">Valor del campo a actualizar</param>
        /// <param name="id">Identificacion de la fila a actualizar</param>
        /// <param name="_entityName">Nombre de la entidad, si no se especifica se toma el atributo</param>
        /// <returns>Mismo objeto</returns>
        public EntityPersist UpdateValue(string key, object value, List<object> ids, string? _entityName = null)
        {
            Dictionary<string, object> row = new Dictionary<string, object>()
            {
                { key, value }
            };
            return UpdateIds(row, ids, _entityName);
        }


        /// <summary>
        /// Actualizar un unico campo
        /// </summary>
        /// <param name="key">Nombre del campo a actualizar</param>
        /// <param name="value">Valor del campo a actualizar</param>
        /// <param name="id">Identificacion de la fila a actualizar</param>
        /// <param name="_entityName">Nombre de la entidad, si no se especifica se toma el atributo</param>
        /// <returns>Mismo objeto</returns>
        public EntityPersist UpdateValueAll(string key, object value, string? _entityName = null)
        {
            Dictionary<string, object> row = new Dictionary<string, object>() { { key, value } };
            return UpdateAll(row, _entityName);
        }




        /// <summary>
        /// Actualiza valor local o de relacion
        /// </summary>
        /// <param name="key">Nombre del campo a actualizar</param>
        /// <param name="value">Nuevo valor del campo a actualizar</param>
        /// <param name="source">Fuente con todos los valores sin actualizar</param>
        /// <param name="_entityName">Opcional nombre de la entidad, si no existe toma el atributo</param>
        /// <returns>Mismo objeto</returns>
        public EntityPersist UpdateValueRel(string key, object value, Dictionary<string, object> source, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;
            string idKey = db.config.id;
            if (key.Contains(db.config.idAttrSeparatorString))
            {
                int indexSeparator = key.IndexOf(db.config.idAttrSeparatorString);
                string fieldId = key.Substring(0, indexSeparator);
                _entityName = db.Entity(_entityName!).relations[fieldId].refEntityName;
                idKey = fieldId + db.config.idAttrSeparatorString + db.config.id;
                key = key.Substring(indexSeparator + db.config.idAttrSeparatorString.Length); //se suma la cantidad de caracteres del separador
            }

            List<object> ids = new() { source[idKey] };
            return UpdateValue(key, value, ids, _entityName);
        }

        public EntityPersist Insert(Dictionary<string, object> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;

            List<string> fieldNames = db.FieldNamesAdmin(_entityName!);
            Dictionary<string, object> row_ = new();
            foreach (string key in row.Keys)
                if (fieldNames.Contains(key))
                    row_.Add(key, row[key]);

            string sn = db.Entity(_entityName!).schemaName;
            sql += "INSERT INTO " + sn + @" (" + String.Join(", ", row_.Keys) + @") 
VALUES (";


            foreach (object value in row_.Values)
            {
                sql += "@" + count + ", ";
                parameters.Add(value);
                count++;
            }

            sql = sql.RemoveLastChar(',');
            sql += @");
";
            EntityValues v = db.Values(_entityName).Set(row_);
            if (!v.values.ContainsKey(db.config.id))
                v.Set(db.config.id, null).Reset(db.config.id);
            row[db.config.id] = v.Get(db.config.id);
            detail.Add((_entityName!, row[db.config.id] as string));

            return this;
        }

        public string Sql()
        {
            return sql;
        }

        /// <summary>
        /// Verifica existencia de valor unico en base a la configuracion de la entidad
        /// Si encuentra resultado, actualiza
        /// Si no encuentra resultado, inserta
        /// </summary>
        /// <param name="row">Conjunto de valores a persistir</param>
        /// <param name="_entityName">Nombre de la entidad, si no existe toma el atributo</param>
        /// <returns>El mismo objeto</returns>
        /// <exception cref="Exception">Si encuentra mas de un conjunto de valores a partir de los campos unicos</exception>
        /// <exception cref="Exception">Si encuentra errores de configuracion en los campos a actualizar</exception>
        /// <exception cref="Exception">Si encuentra errores de configuracion en los campos a insertar</exception>
        public EntityPersist Persist(IDictionary<string, object?> row, string? _entityName = null)
        {
            _entityName = _entityName ?? entityName;

            EntityValues v = db.Values(_entityName!).Set(row);
            return PersistValues(v);
        }
    
        /// <summary>
        /// Persistencia de una instancia de EntityValues
        /// </summary>
        /// <remarks>Se define el comportamiento básico de persistencia</remarks>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public EntityPersist PersistValues(EntityValues v)
        {
            var rows = db.Query(v.entityName!).Unique(v.values).ListDict();

            if (rows.Count > 1)
                throw new Exception("La consulta por campos unicos retorno mas de un resultado");

            if (rows.Count == 1)
            {
                if(v.values.ContainsKey(db.config.id) && v.Get(db.config.id) != rows[0][db.config.id])
                    throw new Exception("Los id son diferentes");

                v.Set(db.config.id, rows[0][db.config.id]).Reset().Check();
                if (v.logging.HasErrors())
                    throw new Exception("Los campos a actualizar poseen errores: " + v.logging.ToString());

                return Update(v.values, v.entityName);
            }

            v.Default().Reset().Check();

            if (v.logging.HasErrors())
                throw new Exception("Los campos a insertar poseen errores: " + v.logging.ToString());

            return Insert(v.values, v.entityName);
        }

        public EntityPersist Exec()
        {
            var q = db.Query();
            q.sql = sql;
            q.parameters = parameters;
            q.Exec();
            return this;
        }

        public EntityPersist Transaction()
        {
            var q = db.Query();
            q.sql = sql;
            q.parameters = parameters;
            q.Transaction();
            return this;
        }
    }

}
 
