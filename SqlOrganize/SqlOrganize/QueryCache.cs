using Microsoft.Extensions.Caching.Memory;

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
            EntityName = query.entity_name;
        }

        public QueryCache(string entityName)
        {
            EntityName = entityName;
        }

        public object Exec()
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

        public List<Dictionary<string, object>> All()
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
