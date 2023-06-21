
namespace SchemaJson
{
    public class Table
    {
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public List<Field> Fields { get; set; } = new();
        public string? Pk { get; set; }
        public List<string> PkAux { get; set; } = new();
        public List<string> Fk { get; set; } = new();
        public List<string> Nf { get; set; } = new();
        public List<string> Unique { get; set; } = new();
        public List<string> UniqueMultiple { get; set; } = new();
        public List<Tree> Tree { get; set; } = new();



        public List<Field> FieldsFkNotReferenced(List<string> referencedTableNames)
        {
            List<Field> fields = new();
            foreach (var field in Fields)
                if (!referencedTableNames.Contains(field.REFERENCED_TABLE_NAME!))
                    fields.Add(field);

            return fields;
        }

    }
}
