namespace SchemaJson
{
    public class Config
    {
        public string connectionString { get; set; }
        public string dbName { get; set; }
        /*
        Path to model
        */
        public string path { get; set; } = "./model/";
        /*
        Alias reservados
        Habitualmente se definen las palabras reservadas del motor de base de datos
        */
        public List<string> reservedAlias { get; set; } = new List<string>();

        /// <summary>
        /// Entidades reservadas
        /// </summary>
        /// <remarks>Las entidades indicadas en la lista no se incluiran en el modelo</remarks>
        public List<string> reservedEntities { get; set; } = new List<string>();

        public string modelPath { get; set; } = "./model";

      

    }
}
