using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{
    public static class Utils
    {

        public static void CopyValues<T>(T target, T source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (!value.IsNullOrEmpty())
                    prop.SetValue(target, value, null);
            }
        }

        public static Dictionary<K, V> MergeManyDicts<K, V>(IEnumerable<Dictionary<K, V>> dictionaries) where K : notnull
        {
            Dictionary<K, V> result = new Dictionary<K, V>();

            foreach (Dictionary<K, V> dict in dictionaries)
            {
                dict.ToList().ForEach(pair => result[pair.Key] = pair.Value);
            }

            return result;
        }

        public static Dictionary<K, V> MergeDicts<K, V>(Dictionary<K, V> dictionary1, Dictionary<K, V> dictionary2) where K : notnull
        {
            Dictionary<K, V> result = new Dictionary<K, V>();

            dictionary1.ToList().ForEach(pair => result[pair.Key] = pair.Value);
            dictionary2.ToList().ForEach(pair => result[pair.Key] = pair.Value);

            return result;
        }
        
        public static bool IsNullOrEmpty(this IList List)
        {
            return (List == null || List.Count < 1);
        }

        public static bool IsNullOrEmpty(this IDictionary Dictionary)
        {
            return (Dictionary == null || Dictionary.Count < 1);
        }

        public static bool IsNullOrEmpty(this object? o)
        {
            if (o is null)
            {
                return true;
            }

            if (o is IDictionary)
            {
                return ((IDictionary)o).IsNullOrEmpty();
            }
            if (o is IList) return ((IList)o).IsNullOrEmpty();
            if (o is string)
            {
                return String.IsNullOrEmpty((string)o);
            }
            return false;
        }

        public static bool IsList(this object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static bool IsTuple(this object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Tuple<>));
        }

        public static bool IsDictionary(this object o)
        {
            if (o == null) return false;
            return o is IDictionary &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }

        public static bool IsArray(this object o)
        {
            return o.GetType().IsArray;
        }

        public static List<object> add_prefix_multi_list(List<object> list, string prefix = "")
        {
            List<object> clonedList = new List<object>(list);

            if (!clonedList.IsNullOrEmpty())
            {
                if (clonedList.ElementAt(0).IsList())
                {
                    for (var i = 0; i < clonedList.Count; i++)
                    {
                        var a = add_prefix_multi_list(clonedList[i] as List<object>, prefix);
                        clonedList[i] = a;
                    }
                }
                else
                {
                    clonedList[0] = prefix + (clonedList[0] as string);
                }
            }

            return clonedList;
        }

        /*
        https://stackoverflow.com/questions/5083709/convert-from-sqldatareader-to-json 
        */
        public static List<Dictionary<string, object>> Serialize(DbDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }
        private static Dictionary<string, object> SerializeRow(List<string> cols,
                                                        DbDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        public static string ReplaceFirst(this string @this, string oldValue, string newValue)
        {
            int startindex = @this.IndexOf(oldValue);

            if (startindex == -1)
            {
                return @this;
            }

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }
    }


}
