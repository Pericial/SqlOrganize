
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SqlOrganize;
using SqlOrganizeMy;
using System.Configuration;

Config config = new Config
{
    connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
    modelPath = ConfigurationManager.AppSettings.Get("modelPath"),
};

var db = new DbMy(config);

var data = db.Query("persona").
Page(1).
Size(10).
ListDict();




MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

QueryCache queryCache = new QueryCache(db, cache);

object[] ids = new object[4] { "10", "100", "101", "102"  };

 data = queryCache.ListDict("persona", ids);

string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(json);


