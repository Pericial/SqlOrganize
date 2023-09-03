using Microsoft.Extensions.Caching.Memory;
using SqlOrganize;
using SqlOrganizeMy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos
{
    class ContainerApp
    {
        public static Config configFines = new ()
        {
            id = "id",
            fkId = true,
            modelPath = ConfigurationManager.AppSettings.Get("modelPathFines"),
            connectionString = ConfigurationManager.AppSettings.Get("connectionStringFines"),
        };

        public static Config configPedidos = new ()
        {
            fkId = true,
            modelPath = ConfigurationManager.AppSettings.Get("modelPathPedidos"),
            connectionString = ConfigurationManager.AppSettings.Get("connectionStringPedidos"),
        };

        public static Db dbFines = new DbMy(configFines);

        public static Db dbPedidos = new DbMy(configPedidos);

        public static MemoryCache cacheFines = new MemoryCache(new MemoryCacheOptions());

        public static DbCache dbCacheFines = new DbCache(dbFines, cacheFines);

        public static MemoryCache cachePedidos = new MemoryCache(new MemoryCacheOptions());

        public static DbCache dbCachePedidos = new DbCache(dbPedidos, cachePedidos);

    }
}
