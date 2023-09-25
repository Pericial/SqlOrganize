using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using SqlOrganizeMy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy
{
    class ContainerApp
    {
        public static WpfAppMy.Config config = new Config
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

        public static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public static Db db = new DbApp(config, cache);



        public static SqlOrganize.DAO dao = new (db);

        public static Config Config()
        {
            return config;
        }
        public static Db Db()
        {
            return db;
        }

    }
}
