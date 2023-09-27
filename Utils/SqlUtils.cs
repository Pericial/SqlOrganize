using FastMember;
using System.Data.Common;

namespace Utils
{
    public static class SqlUtils
    {
        /*
        https://stackoverflow.com/questions/41040189/fastest-way-to-map-result-of-sqldatareader-to-object
        
        Los caracteres especiales de fieldName son reemplazados por "__"
            Ej. persona-nombres > persona__nombres         
        */
        public static T Obj<T>(this DbDataReader rd) where T : class, new()
        {
            Type type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = new T();

            for (int i = 0; i < rd.FieldCount; i++)
            {
                if (!rd.IsDBNull(i))
                {
                    string fieldName = rd.GetName(i).Replace("-","__").Replace(".","__");

                    if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                        accessor[t, fieldName] = rd.GetValue(i);
                }
            }

            return t;
        }

        public static List<T> ColOfObj<T>(this DbDataReader rd) where T : class, new()
        {
            var results = new List<T>();

            while (rd.Read())
                results.Add(rd.Obj<T>());

            return results;
        }


        /*
        https://stackoverflow.com/questions/5083709/convert-from-sqldatareader-to-json 
        */
        public static List<Dictionary<string, object>> Serialize(this DbDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = reader.ColumnNames();

            while (reader.Read())
                results.Add(reader.SerializeRowCols(cols));

            return results;
        }
        public static Dictionary<string, object> SerializeRow(this DbDataReader reader)
        {
            var cols = reader.ColumnNames();
            var result = new Dictionary<string, object>();
            reader.Read();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        public static Dictionary<string, object> SerializeRowCols(this DbDataReader reader, List<string> cols)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        public static List<string> ColumnNames(this DbDataReader reader)
        {
            return Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
        }

        public static List<T> ColumnValues<T>(this DbDataReader reader, string columnName)
        {
            var result = new List<T>();
            while (reader.Read())
                result.Add((T)reader[columnName]);
            return result;
        }

        public static List<T> ColumnValues<T>(this DbDataReader reader, int columnNumber)
        {
            var result = new List<T>();
            while (reader.Read())
                result.Add((T)reader.GetValue(columnNumber));
            return result;
        }

        public static bool IsNullOrEmptyOrDbNull(this object value)
        {
            return value == System.DBNull.Value || value.IsNullOrEmpty();
        }

        public static bool IsDbNull(this object value)
        {
            return (value == System.DBNull.Value);
        }

        
    }


}
