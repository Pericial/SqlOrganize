﻿
using ModelOrganizeSs;

var c = new ConfigSs()
{
    connectionString = "Data Source=DQFC2G3;Initial Catalog=Gestadm_CTAPilar;Integrated Security=True;TrustServerCertificate=true;",
    dbName = "Gestadm_CTAPilar",
    path = @"C:\projects\SqlOrganize\ConsoleBuildSchemaSs\model\"
};

ModelOrganizeSs t = new(c);
t.FileEntities();
t.FileFields();




