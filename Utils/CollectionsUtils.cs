﻿using FastMember;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class CollectionUtils
    {
        public static void CopyValues<T>(this T target, T source)
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

        public static void CopyNotNullValues<T>(this T target, T source)
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

        public static void Merge(this IDictionary<string, object> dictionary1, IDictionary<string, object> dictionary2, string prefix = "")
        {
            dictionary2.ToList().ForEach(pair => dictionary1[prefix + pair.Key] = pair.Value);
        }

        public static void MergeNotNull(this IDictionary<string, object> dictionary1, IDictionary<string, object> dictionary2, string prefix = "")
        {
            dictionary2.ToList().ForEach(pair => {
                if (pair.Value.IsNullOrEmpty()) { dictionary1[prefix + pair.Key] = pair.Value; } 
            } );
        }

        public static bool IsNullOrEmpty(this IList? List)
        {
            return List == null || List.Count < 1;
        }

        public static bool IsNullOrEmpty(this IDictionary? Dictionary)
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


        /// <summary>
        /// Lista de valores de una entrada del diccionario
        /// </summary>
        /// <typeparam name="T">Tipo de retorno</typeparam>
        /// <param name="rows">Lista de diccionarios</param>
        /// <param name="key">Llave del diccionario</param>
        /// <returns>Lista de valores de una entrada del diccionario</returns>
        public static IEnumerable<T> Column<T>(this IEnumerable<Dictionary<string, object>> rows, string key)
        {
            List<T> response = new();
            foreach (Dictionary<string, object> row in rows)
                foreach (var (k, v) in row)
                    if (k == key)
                        response.Add((T)v);

            return response;
        }

        public static IEnumerable<T> ConvertToListOfObject<T>(this IEnumerable<Dictionary<string, object>> rows) where T : class, new()
        {
            var results = new List<T>();

            foreach(var row in rows)
                results.Add(row.ConvertToObject<T>());

            return results;
        }

        public static T ConvertToObject<T>(this IDictionary<string, object> source) where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                string fieldName = item.Key.Replace("-", "__");

                if(someObjectType.GetProperty(fieldName) != null)
                    if (item.Value != System.DBNull.Value)
                        someObjectType
                            .GetProperty(fieldName)!
                            .SetValue(someObject, item.Value, null);
                    else
                        someObjectType
                            .GetProperty(fieldName)!
                            .SetValue(someObject, null, null);
            }

            return someObject;
        }

        public static IDictionary<string, object?> ConvertToDict(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name.Replace("__", "-"),
                propInfo => propInfo.GetValue(source, null)
            );

        }

        public static IEnumerable<Dictionary<string, object?>> ConvertToListOfDict(this IEnumerable<object> source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            List<Dictionary<string, object>> response = new();
            foreach (var s in source)
            {
                response.Add((Dictionary<string, object>)ConvertToDict(s, bindingAttr)!);
            }
            return response;
        }

        public static IDictionary<object, Dictionary<string, object>> ConvertToDictOfDict(this IEnumerable<Dictionary<string, object>> source , string key)
        {
            var response = new Dictionary<object, Dictionary<string, object>>();
            foreach(Dictionary<string, object> i in source)
                response[i[key]] = i;

            return response;
        }

        public static void MergeByKeys(this IEnumerable<Dictionary<string, object>> source, IEnumerable<Dictionary<string, object>> source2, string key1, string? key2 = null, string prefix = "")
        {
            key2 = key2 ?? key1;

            var s = source2.ConvertToDictOfDict(key2);

            foreach (var item in source)
            {
                if (s.ContainsKey(item[key1]))
                    item.Merge(s[item[key1]], prefix);
            }
        }

        public static void MergeByKeysFirst(this IEnumerable<Dictionary<string, object>> source, IEnumerable<Dictionary<string, object>> source2, string key1, string? key2 = null)
        {
            key2 = key2 ?? key1;

            var s = source2.ConvertToDictOfDict(key2);

            foreach (var item in source)
            {
                if (s.ContainsKey(item[key1]))
                {
                    item.Merge(s[item[key1]]);
                    break;
                }
            }
        }

        public static IDictionary<string, List<Dictionary<string, object>>> GroupByKey(this IEnumerable<Dictionary<string, object>> source, string key)
        {
            Dictionary<string, List<Dictionary<string, object>>> response = new();
            foreach(Dictionary<string, object> row in source)
            {
                if (!response.ContainsKey(key))
                    response[key] = new();
                response[key].Add(row);
            }
            return response;
        }

        public static IDictionary<object, Dictionary<string, object>> DictionaryByKey(this IEnumerable<Dictionary<string, object>> source, string key)
        {
            Dictionary<object, Dictionary<string, object>> response = new();
            foreach (Dictionary<string, object> row in source)
                response[row[key]] = row;

            return response;
        }

        public static IDictionary<string, object> AddPrefixToKeyOfDict(this IDictionary<string, object> source, string prefix)
        {
            Dictionary<string, object> response = new();
            foreach(var (key, obj) in source)
                response[prefix + key] = obj;

            return response;
        }

        public static List<Dictionary<string, object>> AddPrefixToKeyOfDictOfEnum(this IEnumerable<Dictionary<string, object>> source, string prefix)
        {
            List<Dictionary<string, object>> response = new();
            foreach(Dictionary<string, object> row in source)
                response.Add((Dictionary<string, object>)row.AddPrefixToKeyOfDict(prefix));
            return response;
        }

        /// <summary>
        /// Add prefix to each element of list of strings
        /// </summary>
        /// <param name="source"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static IEnumerable<string> AddPrefixToEnum(this IEnumerable<string> source, string prefix)
        {
            List<string> response = new();
            foreach (string e in source)
                response.Add(prefix + e);
            return response;
        }

        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> items)
        {
            foreach (var item in items)
                oc.Add(item);
        }

        public static IDictionary<string, T> ToDictionary<T>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
            return dictionary;
        }

        /// <summary>
        /// https://www.dotnetperls.com/sort-strings-length
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static IEnumerable<string> SortByLength(this IEnumerable<string> e, string sort = "ASC")
        {
            // Use LINQ to sort the array received and return a copy.
            var sorted = (sort == "DESC") ?
                from s in e orderby s.Length ascending select s : from s in e orderby s.Length descending select s;

            return sorted;
        }

    }


}


   


