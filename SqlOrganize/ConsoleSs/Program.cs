
using SqlOrganize;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data.Common;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Extensions.Caching.Memory;
using SchemaJsonSs;

Dictionary<string, object> config = new Dictionary<string, object>()
 {
    { "connection_string", "Data Source=DQFC2G3;Initial Catalog=Gestadm_CTAPilar;Integrated Security=True;TrustServerCertificate=true;"},
    { "path_model","C:\\projects\\cs_sqlo\\ConsoleApp1\\model\\"}, //localhost
};

//var db = new DbMy(config);
DbSs db = new(config);

var c = new ConfigSs()
{
    connection_string = "Data Source=DQFC2G3;Initial Catalog=Gestadm_CTAPilar;Integrated Security=True;TrustServerCertificate=true;",
    db_name = "Gestadm_CTAPilar"
};

var t = new BuildSchemaSs(c);

Console.WriteLine("end");



/*
var query = db.Query("SUJETOS").
FieldsAs("$APELLIDO, $NOMBRES").
Where("CAST($FECHA_CARGA AS DATE) = @0 AND $APELLIDO IN (@1)").
Parameters("2007-12-04", new List<object> { "AMADO", "ACOSTA" }).
Page(1).
Size(10).
Fetch("All");

QueryCache qc = new(query);
var data = qc.ExecuteR();
data = qc.ExecuteR();

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



