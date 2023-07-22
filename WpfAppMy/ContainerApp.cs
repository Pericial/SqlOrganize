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
        public static Config config = new Config
        {
            connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
            modelPath = ConfigurationManager.AppSettings.Get("modelPath"),
        };

        public static Db db = new DbMy(config);

        public static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public static DbCache queryCache = new DbCache(db, cache);

        public static Config Config()
        {
            return config;
        }
        public static Db Db()
        {
            return db;
        }

        public static DbCache DbCache()
        {
            return queryCache;
        }
    }
}
