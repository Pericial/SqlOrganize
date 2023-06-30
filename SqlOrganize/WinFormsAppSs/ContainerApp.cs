using SqlOrganize;
using SqlOrganizeSs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Extensions.Caching.Memory;

namespace WinFormsAppSs
{
    public class ContainerApp
    {
        public static Config config = new Config
        {
            connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
            modelPath = ConfigurationManager.AppSettings.Get("modelPath"),
        };

        public static Db db = new DbSs(config);

        public static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public static QueryCache queryCache = new QueryCache(db, cache);

        public Config Config()
        {
            return config;
        }
        public Db Db()
        {
            return db;
        }

        public QueryCache QueryCache()
        {
            return new Quer
        }
    }
}
