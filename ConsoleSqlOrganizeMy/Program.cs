
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


var data = db.Query("comision")
    .Fields("sede-_Id, sede-numero, sede-nombre, domicilio-_Id, domicilio-calle, domicilio-numero, domicilio-entre, domicilio-localidad, domicilio-barrio")
    .Size(0)
    .Where(@"
        $calendario-anio = @0 
        AND $calendario-semestre = @1 
    ")
    .Parameters("2023", "2").ListDict();

foreach (var row in data)
{

    string html = @"
<form method=""post"" action=""index4.php?a=32&amp;b=2"">
Nombre
<input name=""nombre_institucion"" value=""{0}"">
<br>Tipo	
<input name=""tipo_establecimiento"" value=""{1}"">
<br>Calle
<input name=""direccion_institucion"" value=""{2}"">
<br>Numero
<input name=""nro_direccion"" value=""{3}"">
<br>Entre
<input name=""calle1"" value=""{4}"">
<br>Entre 2
<input name=""calle2"" value=""{5}"">
<br>Localidad
<input name=""localidad_institucion"" value=""{6}"">
<br>Telefono 1
<input name=""numero"" value=""0"">
<br>Telefono 2
<input name=""numero2"" value=""0"">
<br>Activa		
<input name=""activa""  value=""0"">
<button class=""bot_2"">Enviar Datos</button>
</form>
";

    Console.WriteLine(html,
        row["sede-nombre"], 
        "Otros", 
        row["domicilio-calle"], 
        row["domicilio-numero"], 
        row["domicilio-entre"],
        "",
        row["domicilio-localidad"] + " " + row["domicilio-barrio"]
    );
}


