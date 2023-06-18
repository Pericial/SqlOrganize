
namespace SchemaJson
{
    public class Field
    {
        public string TABLE_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
        public object? COLUMN_DEFAULT { get; set; }
        public int IS_NULLABLE { get; set; } 
        public string? DATA_TYPE { get; set; }
        public int? CHARACTER_MAXIMUM_LENGTH { get; set; }
        public byte? NUMERIC_PRECISION { get; set; }
        public int? NUMERIC_SCALE { get; set; }
        public string? REFERENCED_TABLE_NAME { get; set; }
        public string? REFERENCED_COLUMN_NAME { get; set; }
        public int IS_PRIMARY_KEY { get; set; }
        public int IS_UNIQUE_KEY { get; set; }
        public int IS_FOREIGN_KEY { get; set; }
    }
}
