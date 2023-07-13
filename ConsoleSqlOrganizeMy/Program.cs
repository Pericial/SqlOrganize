
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


var query = db.Query("disposicion").Size(0).
    Where("$planificacion-plan != '3' AND $planificacion-plan != '367FE6CBD444EB' AND $planificacion-plan != '5D7ACEDBE52A7'").
    Order("$planificacion-anio ASC, $planificacion-semestre ASC, $planificacion-plan ASC");

/*
var query = db.Query("alumno").
    Where("$plan IS NULL");
*/

var data = query.ListDict();
var sql = "";
var i = 0;
foreach (var item in data)
{
    if (i == 5) 
        i = 0;
    i++;
    sql += @"UPDATE disposicion SET orden_informe_coordinacion_distrital = " + i + " WHERE id = '" + item["id"] + @"';
";
}

Console.WriteLine(sql);

string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

Console.WriteLine(json);



//ep.Update(data[0]).Exec();
