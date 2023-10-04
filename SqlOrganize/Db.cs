using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;
using Utils;

namespace SqlOrganize
{
    
    /// <summary>
    /// Contenedor principal de SqlOrganize
    /// </summary>
    /// <remarks>
    /// Db utiliza y es utilizado como herramienta en varios patrones de diseño: AbstractFactory, AbstractCreator, AbstractBuilder, Singleton.<br/>
    /// Una implementación de Db para un determinado motor de base de datos, sera el ConcreteFactory (Ej DbMy extends Db).<br/>
    /// Una implementación de Db para una determinada App sera el ConcreteCreator (Ej DbApp extends DbMy).<br/>
    /// En una determinada App existira una clase Container que sera el director (Builder) y utilizara clases estaticas de Db (Singleton).
    /// </remarks>
    public abstract class Db
    {
        public Config config { get; }

        //public Dictionary<string, Dictionary<string, EntityTree>> tree { get; set; } = new();

        //public Dictionary<string, Dictionary<string, EntityRel>> relations { get; set; } = new();

        public Dictionary<string, Entity> entities { get; set; }

        public Dictionary<string, Dictionary<string, Field>> fields { get; set; }

        public MemoryCache? Cache { get; set; } = null;

        public Db(Config _config, Model model, MemoryCache? Cache = null)
        {
            config = _config;
            this.Cache = Cache;
            entities = model.Entities();
            foreach (Entity e in entities.Values)
                e.db = this;

            fields = model.Fields();
            foreach (Dictionary<string, Field> df in fields.Values)
                foreach (Field f in df.Values)
                    f.db = this;
        }


        public Dictionary<string, Field> FieldsEntity(string entityName)
        {
            return fields[entityName];
        }

        /* 
        configuracion de field

        Si no existe el field consultado se devuelve una configuracion vacia
        No es obligatorio que exista el field en la configuracion, se cargaran los parametros por defecto.
        */
        public Field Field(string entityName, string fieldName)
        {
            Dictionary<string, Field> fe = FieldsEntity(entityName);
            return (fe.ContainsKey(fieldName)) ? fe[fieldName] : new Field();
        }

        public List<string> EntityNames() => entities.Select(o => o.Key).ToList();

        /// <summary>
        /// Nombres de campos de la entidad
        /// </summary>
        /// <remarks>Importante, por cada entidad y por cada relacion, debe incluirse el campo derivado db.config.id. Varios metodos definidos asumen que el valor de _Id esta incluido (EntityValues, DbCache, EntityQuery, etc)<br/>
        /// Utilizar FieldNamesRel, para devolver los nombres de campos junto el nombre de campos de relaciones</remarks>
        /// <param name="entityName"></param>
        /// <returns>Nombres de campos de la entidad</returns>
        public List<string> FieldNames(string entityName) {
            var l = FieldsEntity(entityName).Keys.ToList();
            if(!l.Contains(config.id))
                l.Insert(0, config.id); //Importante!! id debe ser incluido,
            return l;
        }


        /// <summary>
        /// Lista de campos de la entidad y sus relaciones
        /// </summary>
        /// <param name="entityName">Nombre de la entidad de la cual se retornaran el campo principal y sus relaciones</param>
        /// <returns></returns>
        public List<string> FieldNamesRel(string entityName)
        {
            List<string> fieldNamesR = new();

            if (!Entity(entityName).relations.IsNullOrEmpty())
                foreach ((string fieldId, EntityRelation er) in Entity(entityName).relations)
                    foreach (string fieldName in FieldNames(er.refEntityName))
                        fieldNamesR.Add(fieldId + "-" + fieldName);

            return FieldNames(entityName).Concat(fieldNamesR).ToList();
        }

        public List<string> FieldNamesAdmin(string entityName)
        {
            var e = Entity(entityName);
            return e.fields.Except(e.noAdmin).ToList();
        }

        public Entity Entity(string entity_name)
        {
            return entities[entity_name];
        }

        /// <summary>
        /// Instancia de Query para simplificar la ejecucion de consultas a la base de datos
        /// </summary>
        /// <returns>Instancia de Query</returns>
        public abstract Query Query();

        public abstract EntityQuery Query(string entity_name);

        public abstract EntityPersist Persist(string? entityName = null);

        public virtual EntityMapping Mapping(string entityName, string? fieldId = null)
        {
            return new(this, entityName, fieldId);
        }

        public virtual EntityValues Values(string entityName, string? fieldId = null)
        {
            return new(this, entityName, fieldId);
        }

        /// <summary>
        /// Extrae los elementos de una key
        /// </summary>
        /// <param name="entityName">Nombre de la entidad</param>
        /// <param name="key">fieldId-fieldName</param>
        /// <returns>Elementos de la relación</returns>
        /// <remarks>Asegurar existencia de caracter de separación.<br/>
        /// Se puede controlar por ej.: if (key.Contains(ContainerApp.db.config.idAttrSeparatorString)) </remarks>
        public (string fieldId, string fieldName, string refEntityName) KeyDeconstruction(string entityName, string key) {
            int i = key.IndexOf(config.idAttrSeparatorString);
            string fieldId = key.Substring(0, i);
            string refEntityName = Entity(entityName!).relations[fieldId].refEntityName;
            string fieldName = key.Substring(i + config.idAttrSeparatorString.Length);
            return (fieldId, fieldName, refEntityName);
        }
    }

}
