namespace ModelOrganize
{
    public class Config
    {
        public string connectionString { get; set; }
        public string dbName { get; set; }
        /*
        Path to model
        */
        public string modelPath { get; set; } = "./Model/";
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

        public string configPath { get; set; } = "./Model";

        public string dataPath { get; set; } = "./Model/Data";
        public string dataNamespace { get; set; } = "MyApp.Data";

        public string modelNamespace { get; set; } = "MyApp.Model";




        /// <summary>
        /// Referencia para definir los alias e identificadores de fields 
        /// </summary>
        public string idSource { get; set; } = "entity_name"; //field_name or entity_name
    }
}
