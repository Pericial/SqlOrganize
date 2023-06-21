﻿using System.Text.RegularExpressions;
using Utils;

namespace SchemaJson
{
    internal class BuildTree
    {
        public List<Table> Tables { get; set; }
        public string TableName { get; set; }
        protected List<string> Names = new();
        string Content = "";

        public BuildTree(List<Table> tables, string tableName) {
            Tables = tables;
            TableName = tableName;
        }

        public List<Tree> Build()
        {
            Table t = getTableByName(TableName);
            if (t.Fk.IsNullOrEmpty()) return new();
            
            List<string> tablesVisited = new();
            return Fk(t, tablesVisited);
        }

        public Table getTableByName(string tableName)
        {
            foreach(Table t in Tables)
                if (t.Name == tableName) return t;

            throw new Exception("No se encontro " + tableName);
        }

        protected string GetName(string name, string? alias = null, string separator = "_")
        {
            if (!Names.Contains(name))
            {
                Names.Add(name);
                return name;
            }

            if (alias != null)
            {
                name = name + separator + alias;
                return GetName(name);
            }

            Match match = Regex.Match(name, @"\d");
            if (match.Success)
            {
                string number = match.Groups[match.Groups.Count - 1].Value;
                name = name.Replace(number, "");
                name += Convert.ToInt16(number) + 1;
                return GetName(name);
            }

            name += "1";
            return GetName(name);
        }

        protected List<Tree> Fk(Table table, List<string> tablesVisited, string alias = null)
        {
            tablesVisited.Add(table.Name!);
            List<Field> fk = table.FieldsFkNotReferenced(tablesVisited);
            List<Tree> list = new();
            foreach (Field field in fk)
            {
                string fieldId = GetName(field.COLUMN_NAME, field.Alias);

                Tree tree = new()
                {
                    FieldId = fieldId,
                    FieldName = field.COLUMN_NAME,
                    EntityName = field.REFERENCED_TABLE_NAME!,
                };

                if (!tablesVisited.Contains(field.REFERENCED_TABLE_NAME!))
                    tree.Children = Fk(getTableByName(field.REFERENCED_TABLE_NAME!), tablesVisited, field.Alias);

                list.Add(tree);
            }

            return list;
        }
    }
}
