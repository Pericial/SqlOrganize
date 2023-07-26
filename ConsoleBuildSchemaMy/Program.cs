
using SchemaJsonMy;
using System.Configuration;

var c = new ConfigMy()
{
    connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
    path = ConfigurationManager.AppSettings.Get("modelPath"),
    dbName = ConfigurationManager.AppSettings.Get("dbName"),
};

BuildSchemaMy t = new(c);
t.CreateFileEntitites();
t.CreateFileFields();

