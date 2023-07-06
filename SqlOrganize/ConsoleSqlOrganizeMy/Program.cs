
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

var query = db.Query("curso").
    Where("$_Id IN (@0)").
    Parameters(new List<object> { "6496098054a4f", "6496098051999" });

var cache = new MemoryCache(new MemoryCacheOptions());

var qc = new QueryCache(db, cache);

var data = qc.ListDict(query);

data = qc.ListDict(query);

string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(json);


