
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



var query = db.Query("persona").
    Where("$numero_documento = @0").
    Parameters("31073351");


var data = query.ListDict();

string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(json);

data[0]["nombres"] = "IIIII";
data[0]["apellidos"] = "CCCCC";

var ep = db.Persist("persona").Insert(data[0]);
ep = db.Persist("persona").Update(data[0]);
ep = db.Persist("persona").Persist(data[0]);


//ep.Update(data[0]).Exec();
