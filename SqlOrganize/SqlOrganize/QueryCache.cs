﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SqlOrganize
{

    /*
    Clase interna para organizar campos
    */
    internal class FieldsOrganize
    {
        Db Db;

        string EntityName;

        /*
        Campos a consultar de relaciones, 
        Se pueden agregar fk adicionales necesarias para comparar
        */
        public List<string> Fields;

        /*
        FieldsId que deben ser consultados en el orden correspondiente
        */
        public List<string> FieldsIdOrder = new();

        /*
        Campos a consultar de la entidad principal "entityName", 
        */
        public List<string> FieldsMain = new();

        public FieldsOrganize(Db db, string entityName, List<string> fields)
        {
            Db = db;
            EntityName = entityName;
            Fields = fields;
            this.OrganizeFields(0);
            this.OrganizeOrder(Db.Entity(entityName).tree);
        }
        /*
        Organizar fields 
        Se agregan los campos necesarios para consultar y comparar el arbol de fields
        */
        protected void OrganizeFields(int index) {
            if (Fields[index].Contains("-"))
            {
                var f = Fields[index].Split('-');
                EntityRel r = Db.Entity(EntityName).relations[f[0]];
                string fkName = (!r.parentId.IsNullOrEmpty()) ? r.parentId + "-" + r.fieldName : r.fieldName;
                if (!Fields.Contains(fkName)) { Fields.Add(fkName); }
            } else
                FieldsMain.Add(Fields[index]);

            if (++index < Fields.Count)
                OrganizeFields(index);
        }

        protected void OrganizeOrder(Dictionary<string, EntityTree> tree)
        {
            foreach(var (fieldId, et) in tree)
            {
                bool recorrerChildren = false;
                for(var j = 0; j < Fields.Count; j++)
                {
                    if (Fields[j].Contains("-"))
                    {
                        var f = Fields[j].Split("-");
                        if (f[0] == fieldId && !FieldsIdOrder.Contains(fieldId))
                        {
                            FieldsIdOrder.Add(fieldId);
                            recorrerChildren = true;
                        }
                            
                    }
                }
                if(recorrerChildren && !et.children.IsNullOrEmpty())
                    OrganizeOrder(et.children);
            }
        }
    }

    /*
    Uso de cache para resutado de consulta
    */
    public class QueryCache
    {
        public Db Db { get; }
        
        public MemoryCache Cache { get; set; }

        public QueryCache (Db db, MemoryCache cache)
        {
            Db = db;           
            Cache = cache;
        }

        /*
        Ejecuta la consulta y la almacena en Cache
        *
        public object Exec(EntityQuery Query)
        {
            List<string> queries;
            if (!Cache.TryGetValue("queries", out queries))
                queries = new();

            object result;
            string queryKey = Query!.ToString();
            if (!Cache.TryGetValue(queryKey, out result))
            {
                result = Query.Exec<object>();
                Cache.Set(queryKey, result);
                queries.Add(queryKey);
                Cache.Set<List<string>>("queries", queries);
            }
            return result;
        }*/

        /*
        Obtener campos de una entidad (sin relaciones)
        Si no encuentra valores en el Cache, realiza una consulta a la base de datos
        y lo almacena en Cache.
        */
        public List<Dictionary<string,object>> ListDict(string entityName, params object[] ids)
        {
            ids = ids.Distinct().ToArray();

            List<Dictionary<string, object>> response = new(ids.Length); //respuesta que sera devuelta

            List<object> searchIds = new(); //ids que no se encuentran en cache y deben ser buscados

            for(var i = 0; i < ids.Length; i++)
            {
                object? data;
                if (Cache.TryGetValue(entityName + ids[i], out data))
                {
                    response.Insert(i, (Dictionary<string, object>)data!);
                }else
                {
                    response.Insert(i, null);
                    searchIds.Add(ids[i]);
                }
            }

            if (searchIds.Count == 0) return response;

            List<Dictionary<string, object>> rows = Db.Query(entityName).Where("$_Id IN (@0)").Parameters(searchIds).ListDict();

            foreach(Dictionary<string, object> row in rows)
            {
                int index = Array.IndexOf(ids, row["id"]);
                response[index] = EntityCache(entityName,row);
            }

            return response;
        }

        protected List<Dictionary<string, object>> _ListDict(EntityQuery query)
        {
            List<string> queries;
            if (!Cache.TryGetValue("queries", out queries))
                queries = new();

            List<Dictionary<string, object>> result;
            string queryKey = query!.ToString();
            if (!Cache.TryGetValue(queryKey, out result))
            {
                result = query.ListDict();
                Cache.Set(queryKey, result);
                queries!.Add(queryKey);
                Cache.Set<List<string>>("queries", queries);
            }
            return result!;
        }

        public List<Dictionary<string, object>> ListDict(EntityQuery query)
        {
            List<Dictionary<string, object>> response = new();

            if (query.fields.IsNullOrEmpty() || !query.select.IsNullOrEmpty() || !query.group.IsNullOrEmpty())
                return _ListDict(query);
            
            EntityQuery queryAux = (EntityQuery)query.Clone();
            queryAux.fields = "$_Id";

            List<string> ids = queryAux.Column<string>();

            List<string> fields = query.fields!.Replace("$", "").Split(',').ToList().Select(s => s.Trim()).ToList();

            FieldsOrganize fo = new(Db, query.entityName, fields);

            List<Dictionary<string, object>> data = ListDict(query.entityName, ids.ToArray());

            
            for (var i = 0; i < data.Count; i++)
            {
                response.Add(new());
                for (var j = 0; j < fo.Fields.Count; j++) response[i][fo.Fields[j]] = null;
                for (var j = 0; j < fo.FieldsMain.Count; j++) response[i][fo.FieldsMain[j]] = data[i][fo.FieldsMain[j]];
            }
            return TraduceFieldsId(fo, response, 0);

        }

        public void TraduceFieldsId(FieldsOrganize fo, List<Dictionary<string, object>> response, int index)
        {
            if (index >= fo.FieldsIdOrder.Count) return response;
            {
                if (response.Count == 0) return response;

                var entity_name:string = efo.relations[fieldId]["entity_name"];
                var parentId:string = efo.relations[fieldId]["parent_id"];
                var field_name:string = efo.relations[fieldId]["field_name"];
                var fkName:string = (parentId) ? efo.prefix + parentId + "-" + field_name : efo.prefix + field_name;

                var ids = arrayUnique(
                  arrayColumn(response, fkName).filter(function(el) { return el != null; })
      );

                return this.getAll(entity_name, ids).pipe(
                  map(
                    data => {
                        if (!data.length) return response;

                        for (var i = 0; i < response.length; i++)
                        {
                            for (var j = 0; j < data.length; j++)
                            {
                                if (response[i][fkName] == data[j]["id"])
                                {
                                    for (var k = 0; k < efo.relQuery[fieldId].length; k++)
                                    {
                                        var n = efo.relQuery[fieldId][k]
        
                              response[i][efo.prefix + fieldId + "-" + n] = data[j][n]
        
                            }
                                    break;
                                }
                            }
                        }
                        return response;
                    }
                  )
                );
            }
        }


        protected Dictionary<string, object> EntityCache(string entityName, Dictionary<string, object> row)
        {
            if(!Db.Entity(entityName).relations.IsNullOrEmpty()) 
                EntityCacheRecursive(Db.Entity(entityName).relations!, row);

            Cache.Set<Dictionary<string, object>>(entityName+row["id"].ToString(), row);
            return row;
        }

        protected void EntityCacheRecursive(Dictionary<string, EntityRel> relations, Dictionary<string, object> row)
        {
            foreach (var (fieldId, rel) in relations)
            {
                var entityName = rel.refEntityName;
                Dictionary<string, object> rowAux = new();
                foreach (var (column, value) in row)
                {
                    string f = fieldId + Db.config.idNameSeparatorString;
                    if (column.Contains(f))
                    {
                        string ff = column.Substring(f.Length);
                        rowAux[ff] = value;
                        row.Remove(column);
                    }
                }
                Cache.Set<Dictionary<string, object>>(entityName + rowAux["_Id"].ToString(), row);

            }
        }

    }
}
