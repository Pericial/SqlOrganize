
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

var data = db.Query("comision").Size(10).
    Where("$autorizada = @0").Parameters("0").
    ListDict();

var s = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(s);