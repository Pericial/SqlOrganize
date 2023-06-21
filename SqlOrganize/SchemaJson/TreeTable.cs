using System.Text.RegularExpressions;
using Utils;

namespace SchemaJson
{
    internal class TreeTable
    {
        public List<Table> Tables { get; set; }
        public string TableName { get; set; }
        protected List<string> Names = new();
        string Content = "";

        public TreeTable(List<Table> tables, string tableName) {
            Tables = tables;
            TableName = tableName;
        }

        public Table getTableByName(string tableName)
        {
            foreach(Table t in Tables)
                if (t.Name == tableName) return t;

            throw new Exception("No se encontro " + tableName);
        }

        public string CreateFile()
        {
            Table t = getTableByName(TableName);
            if (t.Fk.IsNullOrEmpty()) return "";
            List<string> tableNames = new();
            tableNames.Add(t.Name!);

            Content += @"    """ + t.Name + @""": {
";
            Content += Fk(t, tableNames);
            Content += "}";
            return Content;
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

        protected string Fk(Table table, List<string> tablesVisited, string blankSpaces = "", string alias = null)
        {
            tablesVisited.Add(table.Name);
            List<Field> fk = table.FieldsFkNotReferenced(tablesVisited);
            foreach (Field field in fk)
            {
                string fieldId = GetName(field.COLUMN_NAME, field.Alias);
                Content += @"
" + blankSpaces + "\"" + fieldId + "\": {\"fieldName\":\"" + field.COLUMN_NAME + "\", \"entityName\":\"" + field.REFERENCED_TABLE_NAME + "\", \"children\":{";

                if (!tablesVisited.Contains(field.REFERENCED_TABLE_NAME!))
                    Fk(getTableByName(field.REFERENCED_TABLE_NAME!), tablesVisited, blankSpaces + "    ", field.Alias);

                Content += "}},";
            }

            if (!fk.IsNullOrEmpty()) Content = Content.RemoveLastIndex(',');

            return Content;
        }
    }
}
