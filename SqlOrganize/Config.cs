namespace SqlOrganize
{

    /// <summary>
    /// Configuración
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Path a los archivos del modelo
        /// </summary>
        public string modelPath { get; set; } = ".\\model\\";
        
        /// <summary>
        /// String de conexión con la base de datos
        /// </summary>
        public string connectionString { get; set; }
        
        /// <summary>
        /// String de concatenacion
        /// </summary>
        /// <remarks>
        /// Se utiliza para ciertas operaciones como por ejemplo la concatenación de campos
        /// </remarks>
        public string concatString { get; set; } = "~";
        
        /// <summary>
        /// String de separacion entre fieldId y fieldName para consultas a la base de datos
        /// </summary>
        public string idNameSeparatorString { get; set; } = "-";

        /// <summary>
        /// String utilizado para separar fieldId y fieldName en atributos en una clase
        /// </summary>
        public string idAttrSeparatorString { get; set; } = "__";

        /// <summary>
        /// fk asociada a id
        /// </summary>
        /// <remarks>
        /// Para algunas base de datos las fk están directamente asociadas a las Id.<br/>
        /// Se debe indicar para que las operaciones sean mas eficientes
        /// </remarks>
        public bool fkId = true;

        /// <summary>
        /// Nombre del identificador único de las tablas       
        /// </summary>
        /// <remarks>
        /// Todas las tablas deben tener un campos id con el mismo nombre de tipo string<br/>
        /// Si no existe, debe indicarse "_Id", _Id es un identificador único calculado
        /// </remarks>
        public string id = "_Id";

    }

}
