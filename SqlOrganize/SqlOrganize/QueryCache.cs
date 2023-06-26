using Microsoft.Extensions.Caching.Memory;
using Utils;

namespace SqlOrganize
{

    /*
    Uso de cache para resutado de consulta
    */
    public class QueryCache
    {
        public Db Db { get; }
        public static MemoryCache Cache { get; set; } = new MemoryCache(new MemoryCacheOptions());

        public QueryCache (Db db, MemoryCache cache)
        {
            Db = db;           
            Cache = cache;
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

        public List<Dictionary<string,object>> ListDict(string entityName, params object[] ids)
        {
            ids = ids.Distinct().ToArray();

            List<Dictionary<string, object>> response = new(ids.Length); //respuesta que sera devuelta

            List<object> searchIds = new(); //ids que no se encuentran en cache y deben ser buscados

            for(var i = 0; i < ids.Length; i++)
            {
                object? data = null;
                if (Cache.TryGetValue(entityName + ids[i], out data))
                {
                    response.Insert(0, (Dictionary<string, object>)data!);
                }else
                {
                    response.Insert(0, null);
                    searchIds.Insert(0, ids[i]);
                }
            }

            if (searchIds.Count == 0) return response;

            List<Dictionary<string, object>> rows = Db.Query(entityName).Where("$id = @0").Parameters(searchIds).ListDict<object>();

            foreach(Dictionary<string, object> row in rows)
            {

            }







        }

        public List


    }
}
