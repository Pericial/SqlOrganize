﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{

    /// <summary>
    /// Metodos de acceso a datos de uso general
    /// </summary>
    public class DAO
    {
        protected Db Db;
        protected DbCache Cache;

        public DAO(Db db, DbCache Cache) {
            this.Cache = Cache;
            this.Db = db;
        }

        public IEnumerable<Dictionary<string, object>> Search<T>(string entityName, T param) where T : class
        {
            var q = Db.Query(entityName).Search(param).Size(0);
            return Cache.ColOfDict(q);
        }

        public void UpdateValueRel(string entityName, string key, object value, Dictionary<string, object> source)
        {
            EntityPersist p = Db.Persist(entityName).UpdateValueRel(key, value, source).Exec();
            Cache.Remove(p.detail);
        }

        public IDictionary<string, object> Get(string entityName, object id)
        {
            var q = Db.Query(entityName).Where("$"+Db.config.id+" = @0").Parameters(id);
            return Cache.Dict(q);
        }

    }
}