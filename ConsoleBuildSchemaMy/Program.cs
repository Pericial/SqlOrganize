﻿
using ModelOrganize;
using ModelOrganizeMy;
using System.Configuration;

var c = new ConfigMy()
{
    connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
    path = ConfigurationManager.AppSettings.Get("modelPath"),
    dbName = ConfigurationManager.AppSettings.Get("dbName"),
};

BuildModelMy t = new(c);
foreach (var (key, field) in t.fields)
{
    foreach (var (k, f) in field)
    {
        if(f.name == "id")
        {
            f.defaultValue = "guid";
        }
    }

}

t.CreateFileEntitites();

t.CreateFileFields();

