
using ModelOrganizeMy;
using System.Configuration;

var c = new ConfigMy()
{
    connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
    modelPath = ConfigurationManager.AppSettings.Get("modelPath"),
    configPath = ConfigurationManager.AppSettings.Get("configPath"),
    dbName = ConfigurationManager.AppSettings.Get("dbName"),
    idSource = "field_name",
};

BuildModelMy t = new(c);

t.CreateFileEntitites();

t.CreateFileFields();

