﻿using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Extensions.Caching.Memory;
using SqlOrganizeMy;

namespace WinFormsAppMy
{
    public static class ContainerApp
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
