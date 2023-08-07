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

        public List<(string entityName, string _Id)> detail = new();

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
            string _Id = db.Mapping(_entityName!).Map("_Id");
            sql += @"
WHERE " + _Id + " = @" + count + @";
";
            count++;
            parameters.Add(row["_Id"]);
            detail.Add((_entityName!, (string)row["_Id"]));
            return this;
        }


        /// <summary>
        /// Actualizar un unico campo
        /// </summary>
        /// <param name="key">Nombre del campo a actualizar</param>
        /// <param name="value">Valor del campo a actualizar</param>
        /// <param name="_Id">Identificacion de la fila a actualizar</param>
        /// <param name="_entityName">Nombre de la entidad, si no se especifica se toma el atributo</param>
        /// <returns>Mismo objeto</returns>
        public EntityPersist UpdateValue(string key, object value, string _Id, string? _entityName = null)
        {
            Dictionary<string, object> row = new Dictionary<string, object>()
            {
                { "_Id", _Id },
                { key, value }
            };
            return Update(row, _entityName);
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
            string _IdKey = "_Id";
            if (key.Contains(db.config.idAttrSeparatorString))
            {
                int indexSeparator = key.IndexOf(db.config.idAttrSeparatorString);
                string fieldId = key.Substring(0, indexSeparator);
                _entityName = db.Entity(_entityName!).relations[fieldId].refEntityName;
                _IdKey = fieldId + db.config.idAttrSeparatorString + "_Id";
                key = key.Substring(indexSeparator + db.config.idAttrSeparatorString.Length); //se suma la cantidad de caracteres del separador
            }

            string _Id = (string)source[_IdKey];
            return UpdateValue(key, value, _Id, _entityName);
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
            sql = "INSERT INTO " + sn + @" (" + String.Join(", ", row_.Keys) + @") 
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
            EntityValues v = db.Values(_entityName).Set(row_).Reset("_Id");
            row["_Id"] = v.Get("_Id");
            detail.Add((_entityName!, (string)row["_Id"]));

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
        public EntityPersist Persist(Dictionary<string, object> row, string? _entityName = null)
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
                if(v.values.ContainsKey("_Id") && v.Get("_Id") != rows[0]["_Id"])
                    throw new Exception("Los _Id son diferentes");

                v.Set("_Id", rows[0]["_Id"]).Reset().Check();
                if (v.logging.HasErrors())
                    throw new Exception("Los campos a actualizar poseen errores: " + v.logging.ToString());

                return Update(v.values, v.entityName);
            }

            v.Default().Reset().Check();

            if (v.logging.HasErrors())
                throw new Exception("Los campos a insertar poseen errores: " + v.logging.ToString());

            return Insert(v.values, v.entityName);
        }

        public abstract EntityPersist Exec();
    }

}
 