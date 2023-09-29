using HarfBuzzSharp;
using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using SqlOrganizeMy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy
{
    static class ContainerApp
    {
        public static Db db;

        public static Config config = new Config
        {
            id = "id",
            fkId = true,
            connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
            modelPath = ConfigurationManager.AppSettings.Get("modelPath"),
            emailDocenteUser = ConfigurationManager.AppSettings.Get("emailDocenteUser"),
            emailDocentePassword = ConfigurationManager.AppSettings.Get("emailDocentePassword"),
            emailDocenteHost = ConfigurationManager.AppSettings.Get("emailDocenteHost"),
            emailDocenteFromAddress = ConfigurationManager.AppSettings.Get("emailDocenteFromAddress"),
            emailDocenteFromName = ConfigurationManager.AppSettings.Get("emailDocenteFromName"),
            emailDocenteBcc = ConfigurationManager.AppSettings.Get("emailDocenteBcc"),
        };


        static ContainerApp()
        {

            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            Model model = new Model();
            db = new DbApp(config, model, cache);

        }

        public static EntityValues? Values(this IDictionary<string, object>? data, string entityName)
        {
            if (data.IsNullOrEmptyOrDbNull()) return null;
            return db.Values(entityName).Values(data);
        }

        public static EntityValues? ValuesSet(this IDictionary<string, object>? data, string entityName, string? fieldId = null)
        {
            if (data.IsNullOrEmptyOrDbNull()) return null;
            return db.Values(entityName, fieldId).Set(data);
        }

    }
}
