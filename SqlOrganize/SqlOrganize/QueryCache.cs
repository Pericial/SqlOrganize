﻿using Microsoft.Extensions.Caching.Memory;

namespace SqlOrganize
{

    /*
    Uso de cache para resutado de consulta
    */
    public class QueryCache
    {
        public EntityQuery? Query { get; }
        public string EntityName { get; }
        public static MemoryCache Cache { get; set; } = new MemoryCache(new MemoryCacheOptions());

        public QueryCache (EntityQuery query)
        {
            Query = query;
            EntityName = query.entityName;
        }

        public QueryCache(string entityName)
        {
            EntityName = entityName;
        }

        /*
        Ejecuta la consulta y la almacena en Cache
        */
        public object Execute()
        {
            List<string> queries = null;
            if (!Cache.TryGetValue("queries", out queries))
            {
                queries = new();
                Cache.Set<List<string>>("queries", queries);
            }

            object result = null;
            string queryKey = Query!.ToString();
            if (!Cache.TryGetValue(queryKey, out result))
            {
                result = Query.Exec();
                Cache.Set(queryKey, result);
                queries.Add(queryKey);
                Cache.Set<List<string>>("queries", queries);
            }
            return result;
        }

        /*
        Ejecuta la consulta almacena resultado en cache
        Solo funciona con campos de configuracion.
        */
        public List<Dictionary<string, object>> ExecuteR()
        {
            return Query!.All();
            //Cache.Clear();
        }

        public List<Dictionary<string, object>> GetAll(List<object> ids)
        {
            return Query!.All();
        }
    }
}