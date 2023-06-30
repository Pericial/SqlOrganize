
using SchemaJsonMy;

var c = new ConfigMy()
{
    connection_string = "server=planfines2.com.ar;database=planfi10_20203;uid=planfi10_2020;pwd=Marcelita1024",
    db_name = "planfi10_20203",
    path = @"C:\xampp\htdocs\SqlOrganize\SqlOrganize\ConsoleBuildSchemaMy\model\"
};

BuildSchemaMy t = new(c);
t.FileEntities();
t.FileFields();