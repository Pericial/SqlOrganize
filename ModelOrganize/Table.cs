
using System.Data.Common;

namespace ModelOrganize
{
    public class Table
    {
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public List<Column> Columns { get; set; } = new();
        public List<string> Pk { get; set; } = new();
        public List<string> ColumnNames { get; set; } = new();
        public List<string> Fk { get; set; } = new();
        public List<string> Unique { get; set; } = new();
        public List<List<string>> UniqueMultiple { get; set; } = new();
        public List<string> NotNull { get; set; } = new();

        public List<Column> ColumnsFkNotReferenced(List<string> referencedTableNames)
        {
            List<Column> columns = new();
            foreach (var column in Columns)
                if((column.IS_FOREIGN_KEY == 1) && (!referencedTableNames.Contains(column.REFERENCED_TABLE_NAME!)))
                    columns.Add(column);

            return columns;
        }

    }
}
