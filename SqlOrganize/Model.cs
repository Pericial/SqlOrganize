using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{
    public abstract class Model
    {
        /// <summary>
        /// JSON con entidades del modelo
        /// </summary>
        /// <remarks>
        /// Se genera a traves de proyecto ModelOrganize
        /// </remarks>
        protected abstract string entities { get; }

        /// <summary>
        /// JSON con fields del modelo
        /// </summary>
        /// <remarks>
        /// Se genera a traves de proyecto ModelOrganize
        /// </remarks>
        protected abstract Dictionary<string, string> fields { get; }
        
        /// <summary>
        /// Procesa atributo entities para generar el diccionario de entidades que seran utilizadas durante todo el proyecto
        /// </summary>
        public Dictionary<string, Entity> Entities()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Entity>>(entities)!;
        }

        /// <summary>
        /// Procesa atributo fields para generar el diccionario de fields de entidades que seran utilizadas durante todo el proyecto
        /// </summary>
        public Dictionary<string, Dictionary<string, Field>> Fields()
        {
            Dictionary<string, Dictionary<string, Field>> response = new();
            foreach (var (entityName, field) in fields)
                response[entityName] = JsonConvert.DeserializeObject<Dictionary<string, Field>>(field)!;

            return response;
        }

    }
}
