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
        
        public MemoryCache Cache { get; set; }

        public QueryCache (Db db, MemoryCache cache)
        {
            Db = db;           
            Cache = cache;
        }

        /*
        Ejecuta la consulta y la almacena en Cache
        *
        public object Exec(EntityQuery Query)
        {
            List<string> queries;
            if (!Cache.TryGetValue("queries", out queries))
                queries = new();

            object result;
            string queryKey = Query!.ToString();
            if (!Cache.TryGetValue(queryKey, out result))
            {
                result = Query.Exec<object>();
                Cache.Set(queryKey, result);
                queries.Add(queryKey);
                Cache.Set<List<string>>("queries", queries);
            }
            return result;
        }*/

        /*
        Obtener campos de una entidad (sin relaciones)
        Si no encuentra valores en el Cache, realiza una consulta a la base de datos
        y lo almacena en Cache.
        */
        public List<Dictionary<string,object>> ListDict(string entityName, params object[] ids)
        {
            ids = ids.Distinct().ToArray();

            List<Dictionary<string, object>> response = new(ids.Length); //respuesta que sera devuelta

            List<object> searchIds = new(); //ids que no se encuentran en cache y deben ser buscados

            for(var i = 0; i < ids.Length; i++)
            {
                object? data;
                if (Cache.TryGetValue(entityName + ids[i], out data))
                {
                    response.Insert(i, (Dictionary<string, object>)data!);
                }else
                {
                    response.Insert(i, null);
                    searchIds.Add(ids[i]);
                }
            }

            if (searchIds.Count == 0) return response;

            List<Dictionary<string, object>> rows = Db.Query(entityName).Where("$id = @0").Parameters(searchIds).ListDict();

            foreach(Dictionary<string, object> row in rows)
            {
                int index = Array.IndexOf(ids, row["id"]);
                response[index] = EntityCache(entityName,row);
            }

            return response;
        }


        protected Dictionary<string, object> EntityCache(string entityName, Dictionary<string, object> row)
        {
            if(!Db.Entity(entityName).relations.IsNullOrEmpty()) 
                EntityCacheRecursive(Db.Entity(entityName).relations!, row);

            Cache.Set<Dictionary<string, object>>(entityName+row["id"].ToString(), row);
            return row;
        }

        protected void EntityCacheRecursive(Dictionary<string, EntityRel> relations, Dictionary<string, object> row)
        {
            foreach (var (fieldId, rel) in relations)
            {
                var entityName = rel.refEntityName;
                Dictionary<string, object> rowAux = new();
                foreach (var (column, value) in row)
                {
                    if (column.Contains(fieldId))
                    {
                        rowAux[column] = value;
                        row.Remove(column);
                    }
                    Cache.Set<Dictionary<string, object>>(entityName + rowAux["id"].ToString(), row);
                }
            }
        }

    }
}
