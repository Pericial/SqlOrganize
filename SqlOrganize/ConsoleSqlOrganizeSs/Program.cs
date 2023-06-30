// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using SqlOrganize;
using SqlOrganizeSs;
using System.Configuration;
using System.Collections.Specialized;




Config config = new Config
{
    connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
    modelPath = ConfigurationManager.AppSettings.Get("modelPath"),
};

var db = new DbSs(config);

var data = db.Query("ESTADISTICA_SALDOS").
    Page(1).
    Size(10).
    Where("$_Id > 'A'").
    ListDict();


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




