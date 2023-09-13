
namespace ModelOrganize
{
    /*
    Varios campos que podrian ser considerados como int, se definen como object 
    porque los distintos motores de base de datos asignan tipos diferentes.
    Ejemplos: CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE
    */
    public class Column
    {
        public string Alias { get; set; }
        public string DataType { get; set; }

        public string TABLE_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
        public object? COLUMN_DEFAULT { get; set; }
        public int IS_NULLABLE { get; set; } 
        public string? DATA_TYPE { get; set; }
        
        public object? CHARACTER_MAXIMUM_LENGTH { get; set; }
        public object? MAX_LENGTH { get; set; }
        public object? NUMERIC_PRECISION { get; set; }
        public object? NUMERIC_SCALE { get; set; }
        public string? REFERENCED_TABLE_NAME { get; set; }
        public string? REFERENCED_COLUMN_NAME { get; set; }
        public int IS_PRIMARY_KEY { get; set; }
        public int IS_UNIQUE_KEY { get; set; }
        public int CONSTRAINT_NAME { get; set; }
        public int IS_FOREIGN_KEY { get; set; }
        public int IS_UNSIGNED { get; set; } = 0;

        public string COLUMN_TYPE { get; set; }

    }
}
