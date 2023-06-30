
using Newtonsoft.Json;
using SqlOrganize;
using SqlOrganizeMy;

Config config = new Config
{
    connectionString = "",
    modelPath = @"C:\xampp\htdocs\SqlOrganize\SqlOrganize\ConsoleBuildSchemaMy\model\"
};

var db = new DbMy(config);

var data = db.Query("persona").
Page(1).
Size(10).
ListDict();


string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(json);

