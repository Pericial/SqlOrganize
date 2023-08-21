﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy
{
    /// <summary>
    /// Data Access Object generica
    /// </summary>
    public class DAO
    {
        public Dictionary<string, object>? RowByEntityFieldValue(string entityName, string fieldName, object value)
        {
            var q = ContainerApp.db.Query(entityName).Where("$" + fieldName + " = @0").Parameters(value);
            return ContainerApp.dbCache.Dict(q);
        }

        public Dictionary<string, object>? RowByEntityUnique(string entityName, Dictionary<string, object> source)
        {
            var q = ContainerApp.db.Query(entityName).Unique(source);

            if (!source[ContainerApp.config.id].IsNullOrEmpty())
                q.Where("($" + ContainerApp.config.id + " != @0)").Parameters(source[ContainerApp.config.id]);
            if (!q.IsUnique())
                return null;

            return ContainerApp.dbCache.Dict(q);
        }
    }
}