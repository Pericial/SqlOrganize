
using SqlOrganize;
using SqlOrganizeMy;

Config config = new Config
{
    connectionString = "server=localhost;database=planfi10_20203;uid=root;",
    modelPath = @"C:\xampp\htdocs\SqlOrganize\SqlOrganize\ConsoleBuildSchemaMy\model\"
};

var db = new DbMy(config);

var query = db.Query("persona").
Page(1).
Size(10).
Where("$id > 0 OR $id > 'A'").
Sql();
Console.WriteLine(query);
