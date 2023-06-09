﻿
namespace SchemaJson
{
    public class Table
    {
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public List<Field> FieldsData { get; set; } = new();
        public List<string> Pk { get; set; } = new();
        public List<string> Fields { get; set; } = new();
        public List<string> Fk { get; set; } = new();
        public List<string> Unique { get; set; } = new();
        public List<Tree> Tree { get; set; } = new();
        public List<string> NotNull { get; set; } = new();

        public List<Field> FieldsFkNotReferenced(List<string> referencedTableNames)
        {
            List<Field> fields = new();
            foreach (var field in FieldsData)
                if((field.IS_FOREIGN_KEY == 1) && (!referencedTableNames.Contains(field.REFERENCED_TABLE_NAME!)))
                    fields.Add(field);

            return fields;
        }

    }
}
