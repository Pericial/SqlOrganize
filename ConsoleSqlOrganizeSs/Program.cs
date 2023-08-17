// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using SqlOrganize;
using SqlOrganizeSs;
using System.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Utils;

Config config = new Config
{
    connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
    modelPath = ConfigurationManager.AppSettings.Get("modelPath"),
};


var db = new DbSs(config);


var query = db.Query("PERSONAL");

var cache = new MemoryCache(new MemoryCacheOptions());

var qc = new DbCache(db, cache);

var data = qc.ListDict(query);




var dataCantidadPersonal = db.Query("PERSONAL").
    Select("COUNT(*) AS cantidad_personal").
    Group("$DTOJUD").ListDict();


var d = dataCantidadPersonal.ListToDict("DTOJUD");


string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);


Console.WriteLine(json);
/*
var query = db.Query("PERSONAL").
    Where(db.config.id " IN (@0)").
    Parameters(new List<object> {"01-DNI~90", "01-DNI~91"});

var cache = new MemoryCache(new MemoryCacheOptions());

var qc = new DbCache(db, cache);

var data = qc.ListDict(query);

data[0]["APELLIDO"] = "CIERRESAP";
data[0]["NOMBRES"] = "CIERRESNOM";

//var data2 = qc.ListObj<Personal>(query);

string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

//db.Persist("PERSONAL").Update(data[0]).Exec();

Console.WriteLine(json);
*/



public class Personal
{
    public string TIPODOC { get; set; }
    public int NRODOC { get; set; }
    public string DTOJUD__DESCRIPCION { get; set; }
    public string NIVEL_SEGU__DESCRIPCION { get; set; }

}
/*W

DbCache qc = new(query);
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

DbCache qc2 = new(query);
data = qc2.ExecuteR();









string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(json);

*/




