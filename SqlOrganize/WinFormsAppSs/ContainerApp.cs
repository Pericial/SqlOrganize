using SqlOrganize;
using SqlOrganizeSs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSs
{
    public class ContainerApp
    {
        public static Config config = new Config()
        {
            modelPath = @"C:\projects\SqlOrganize\SqlOrganize\ConsoleBuildSchemaSs\model\",
            connectionString = "Data Source=DQFC2G3;Initial Catalog=Gestadm_CTAPilar;Integrated Security=True;TrustServerCertificate=true;",
        };

        public static Db db = new DbSs(config);

        public Config Config()
        {
            return config;
        }
        public Db Db()
        {
            return db;
        }
    }
}
