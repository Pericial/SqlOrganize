﻿// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using SqlOrganize;
using SqlOrganizeSs;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

Config config = new Config
{
    connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
    modelPath = ConfigurationManager.AppSettings.Get("modelPath"),
};

var db = new DbSs(config);

var query = db.Query("PERSONAL").
    Fields("NIVEL_SEGU-NIVEL_SEGU, DTOJUD-DESCRIPCION, APELLIDO").
    Where("$_Id IN (@0)").
    Parameters(new List<object> {"01-DNI~90", "01-DNI~91"});

var cache = new MemoryCache(new MemoryCacheOptions());

var qc = new QueryCache(db, cache);

var data = qc.ListDict(query);

data = qc.ListDict(query);

string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(json);

/*W

QueryCache qc = new(query);
var data = qc.Execute();
data = qc.Execute();
System.Console.WriteLine(data);

query = db.Query("SUJETOS").
FieldsAs("$APELLIDO, $NOMBRES").
Where("CAST($FECHA_CARGA AS DATE) = @0 AND $APELLIDO IN (@1)").
Parameters("2007-12-04", new List<object> { "AMADO", "ACOSTA" }).
Page(1).
Size(1).
Fetch("All");

QueryCache qc2 = new(query);
data = qc2.ExecuteR();









string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(json);

*/



