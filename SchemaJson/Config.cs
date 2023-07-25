namespace SchemaJson
{
    public class Config
    {
        public string connection_string { get; set; }
        public string db_name { get; set; }
        /*
        Path to model
        */
        public string path { get; set; } = "./model/";
        /*
        Alias reservados
        Habitualmente se definen las palabras reservadas del motor de base de datos
        */
        public List<string> reserved_alias { get; set; } = new List<string>();

        Dictionary<string, Table> entities = new();

        Dictionary<string, Field> fields = new();

    }
}
