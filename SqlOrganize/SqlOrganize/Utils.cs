using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{
    public static class Utils
    {

        public static string TraduceSql(Db db, string entityName, string _sql)
        {
            string sql = "";
            int field_start = -1;

            for (int i = 0; i < _sql.Length; i++)
            {
                if (_sql[i] == '$')
                {
                    field_start = i;
                    continue;
                }

                if (field_start != -1)
                {
                    if ((_sql[i] != ' ') && (_sql[i] != ')') && (_sql[i] != ',')) continue;
                    sql += TraduceSql_(db, entityName, _sql, field_start, i - field_start - 1);
                    field_start = -1;
                }

                sql += _sql[i];

            }

            if (field_start != -1)
                sql += TraduceSql_(db, entityName, _sql, field_start, _sql.Length - field_start - 1);


            return sql;
        }

        public static string TraduceSql_(Db db, string entityName, string _sql, int fieldStart, int fieldEnd)
        {
            var fieldName = _sql.Substring(fieldStart + 1, fieldEnd);

            string ff = "";
            if (fieldName.Contains('-'))
            {
                List<string> fff = fieldName.Split("-").ToList();
                ff += db.Mapping(db.Entity(entityName).relations[fff[0]].refEntityName, fff[0]).Map(fff[1]);
            }
            else
                ff += db.Mapping(entityName).Map(fieldName);

            return ff;
        }
    }
}
