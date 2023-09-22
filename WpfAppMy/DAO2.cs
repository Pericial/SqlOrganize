using Google.Protobuf.WellKnownTypes;
using MySqlX.XDevAPI.Relational;
using SqlOrganize;
using System;
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
    public class DAO2
    {
        public IDictionary<string, object>? RowByEntityFieldValue(string entityName, string fieldName, object value)
        {
            var q = ContainerApp.db.Query(entityName).Where("$" + fieldName + " = @0").Parameters(value);
            return ContainerApp.dbCache.Dict(q);
        }

        public IDictionary<string, object>? RowByEntityUnique(string entityName, IDictionary<string, object> source)
        {
            var q = ContainerApp.db.Query(entityName).Unique(source);

            if (!source[ContainerApp.config.id].IsNullOrEmpty())
                q.Where("($" + ContainerApp.config.id + " != @0)").Parameters(source[ContainerApp.config.id]);
            if (!q.IsUnique())
                return null;

            return ContainerApp.dbCache.Dict(q);
        }

        public void Persist(EntityValues v)
        {
            if (v.Get(ContainerApp.config.id).IsNullOrEmpty())
            {
                v.Default().Reset();
                var p = ContainerApp.db.Persist(v.entityName).Insert(v.values).Exec();
                ContainerApp.dbCache.Remove(p.detail);
            }
            else
            {
                v.Reset();
                var p = ContainerApp.db.Persist(v.entityName).Update(v.values).Exec();
                ContainerApp.dbCache.Remove(p.detail);
            }
        }


        public IDictionary<string, object>? RowByUniqueFieldOrUniqueValues(string fieldName, EntityValues values)
        {
            if (ContainerApp.db.Field(values.entityName, fieldName).IsUnique())
                return RowByEntityFieldValue(values.entityName, fieldName, values.Get(fieldName));
            else
                return RowByEntityUnique(values.entityName, values.Get());
        }
    }
}
