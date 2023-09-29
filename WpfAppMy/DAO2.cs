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
            return ContainerApp.db.Query(entityName).Where("$" + fieldName + " = @0").Parameters(value).DictCache();
        }

        public IDictionary<string, object>? RowByEntityUnique(string entityName, IDictionary<string, object> source)
        {
            var q = ContainerApp.db.Query(entityName).Unique(source);

            if (!source[ContainerApp.db.config.id].IsNullOrEmpty())
                q.Where("($" + ContainerApp.db.config.id + " != @0)").Parameters(source[ContainerApp.db.config.id]);

            return q.DictCache();
        }

        public void Persist(EntityValues v)
        {
            if (v.Get(ContainerApp.db.config.id).IsNullOrEmpty())
            {
                v.Default().Reset();
                var p = ContainerApp.db.Persist(v.entityName).Insert(v.values).Exec().RemoveCache();
            }
            else
            {
                v.Reset();
                var p = ContainerApp.db.Persist(v.entityName).Update(v.values).Exec().RemoveCache();
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
