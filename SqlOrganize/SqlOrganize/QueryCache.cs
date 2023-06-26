using Microsoft.Extensions.Caching.Memory;
using Utils;

namespace SqlOrganize
{

    /*
    Uso de cache para resutado de consulta
    */
    public class QueryCache
    {
        public Db? Db { get; }
        public string EntityName { get; }
        public object? Ids { get; }
        public static MemoryCache Cache { get; set; } = new MemoryCache(new MemoryCacheOptions());

        public QueryCache (Db db)
        {
            Db = db;
        }

        /*
        Ejecuta la consulta y la almacena en Cache
        */
        public object Exec(EntityQuery Query)
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
                result = Query.Exec<object>();
                Cache.Set(queryKey, result);
                queries.Add(queryKey);
                Cache.Set<List<string>>("queries", queries);
            }
            return result;
        }

        public List<T> ListObject<T>(EntityQuery query) where T : class, new()
        {
            return query!.ListObject<T>();
            //Todo verificar si esta en cache, verificar si query posee campos unicos
        }


    }
}
