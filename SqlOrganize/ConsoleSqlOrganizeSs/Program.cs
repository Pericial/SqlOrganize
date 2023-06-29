// See https://aka.ms/new-console-template for more information
using SqlOrganize;
using SqlOrganizeSs;


Config config = new Config
{
    connectionString = @"Data Source=DQFC2G3;Initial Catalog=Gestadm_CTAPilar;Integrated Security=True;TrustServerCertificate=true;",
    modelPath = @"C:\projects\SqlOrganize\SqlOrganize\ConsoleBuildSchemaSs\model\"
};

var db = new DbSs(config);

var query = db.Query("$id").
Page(1).
Size(10).
Where("$id > 0 OR $id > 'A'").
Sql();
Console.WriteLine(query);


/*

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




