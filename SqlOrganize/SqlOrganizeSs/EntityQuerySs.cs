﻿using SqlOrganize;
using Utils;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace SqlOrganizeSs
{
    public class EntityQuerySs : EntityQuery
    {

        public EntityQuerySs(Db db, string entity_name) : base(db, entity_name)
        {
        }

        public override List<Dictionary<string, object>> All()
        {
            using SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            connection.Open();
            string sql = Sql();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            for (var i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].IsList())
                {
                    var _parameters = (parameters[i] as List<object>).Select((x, j) => Tuple.Create($"@{i}_{j}", x));
                    sql = sql.ReplaceFirst("@" + i.ToString(), string.Join(",", _parameters.Select(x => x.Item1)));
                    foreach (var parameter in _parameters)
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                } else {
                    command.Parameters.AddWithValue(i.ToString(), parameters[i]);
                }
            }

            command.CommandText = sql;
                command.ExecuteNonQuery();
                using SqlDataReader reader = command.ExecuteReader();

            return reader.Serialize<object>();
        }

        public override List<Dictionary<string, object>> Tree()
        {
            throw new NotImplementedException();
        }



        protected override string sql_limit()
        {
            if (size.IsNullOrEmpty()) return "";
            page = page.IsNullOrEmpty() ? 1 : page;
            return "OFFSET " + ((page - 1) * size) + @" ROWS
FETCH FIRST " + size + " ROWS ONLY";
        }


        protected override string SqlOrder()
        {
            if (order.IsNullOrEmpty())
            {
                var o = db.Entity(entityName).orderDefault;
                order = o.IsNullOrEmpty() ? "" : string.Join(", ", o.Select(x => "$" + x));
            }

            return ((order.IsNullOrEmpty()) ? "ORDER BY (SELECT NULL)" : "ORDER BY " + traduce(order!)) + @"
";
        }

        public override DbDataReader Execute()
        {
            SqlConnection connection = new SqlConnection((string)db.config.connectionString);
            connection.Open();
            string sql = Sql();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            for (var i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].IsList())
                {
                    var _parameters = (parameters[i] as List<object>).Select((x, j) => Tuple.Create($"@{i}_{j}", x));
                    sql = sql.ReplaceFirst("@" + i.ToString(), string.Join(",", _parameters.Select(x => x.Item1)));
                    foreach (var parameter in _parameters)
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                }
                else
                {
                    command.Parameters.AddWithValue(i.ToString(), parameters[i]);
                }
            }

            command.CommandText = sql;
            command.ExecuteNonQuery();
            return command.ExecuteReader();

        }
    }
}