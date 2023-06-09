
using SqlOrganize;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data.Common;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Xml;

Dictionary<string, object> config = new Dictionary<string, object>()
 {
    { "connection_string", "Data Source=DQFC2G3;Initial Catalog=Gestadm_CTAPilar;Integrated Security=True;TrustServerCertificate=true;"},
    { "path_model","C:\\projects\\cs_sqlo\\ConsoleApp1\\model\\"}, //localhost
};

//var db = new DbMy(config);
DbSs db = new(config);
var data = db.query("SUJETOS").
    Where("CAST($FECHA_CARGA AS DATE) = @0").
    Parameters("2007-12-04").
    All();


string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(json);





