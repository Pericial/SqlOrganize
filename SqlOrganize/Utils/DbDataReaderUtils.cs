﻿using System.Data.Common;

namespace Utils
{
    public static class DbDataReaderUtils
    {

        /*
        https://stackoverflow.com/questions/5083709/convert-from-sqldatareader-to-json 
        */
        public static List<Dictionary<string, object>> Serialize(DbDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = ColumnNames(reader);

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }
        private static Dictionary<string, object> SerializeRow(List<string> cols, DbDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        public static List<string> ColumnNames(DbDataReader reader)
        {
            return Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
        }

        public static List<T> ColumnValues<T>(DbDataReader reader, string columnName)
        {
            var result = new List<T>();
            while (reader.Read())
                result.Add((T)reader[columnName]);
            return result;
        }
    }


}
