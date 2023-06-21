
using SchemaJsonSs;

var c = new ConfigSs()
{
    connection_string = "Data Source=DQFC2G3;Initial Catalog=Gestadm_CTAPilar;Integrated Security=True;TrustServerCertificate=true;",
    db_name = "Gestadm_CTAPilar",
    path = @"C:\projects\SqlOrganize\SqlOrganize\ConsoleBuildSchemaSs\model\"
};

BuildSchemaSs t = new(c);
//t.Entities();



