
using SchemaJsonMy;

var c = new ConfigMy()
{
    connection_string = "server=localhost;database=planfi10_20203;uid=root",
    db_name = "planfi10_20203",
    path = @"C:\xampp\htdocs\SqlOrganize\ConsoleBuildSchemaMy\model\"
};

BuildSchemaMy t = new(c);
t.FileEntities();
t.FileFields();

