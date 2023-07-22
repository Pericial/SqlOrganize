
using SchemaJsonMy;
using System.Configuration;

var c = new ConfigMy()
{
    connection_string = ConfigurationManager.AppSettings.Get("connectionString"),
    path = ConfigurationManager.AppSettings.Get("modelPath"),
    db_name = ConfigurationManager.AppSettings.Get("dbName"),
};

BuildSchemaMy t = new(c);
t.FileEntities();
t.FileFields();

