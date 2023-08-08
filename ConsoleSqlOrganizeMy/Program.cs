
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


var data = db.Query("persona")
    .Where("$apellidos > @0")
    .Parameters("A")
    .Size(0)
    .Unique(new Dictionary<string, object>() { { "numero_documento", "31073351" } });

string sql = data.Sql();
Console.WriteLine(sql);

Console.WriteLine(data.ListDict());


