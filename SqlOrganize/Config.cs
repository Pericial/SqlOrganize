namespace SqlOrganize
{
    public class Config
    {
        /*
        Path a los archivos del modelo
        */
        public string modelPath { get; set; } = ".\\model\\";
        
        /*
        Sufijo de sobrescritura de archivos del modelo
        */
        public string modelSuffix { get; set; } = "_";

        /*
        String de conexin con la base de datos
        */
        public string connectionString { get; set; }

        public string concatString { get; set; } = "~";
        
        /// <summary>
        /// String de separacion entre fieldId y fieldName para consultas a la base de datos
        /// </summary>
        public string idNameSeparatorString { get; set; } = "-";

        /// <summary>
        /// String utilizado para separar fieldId de atributos en una clase
        /// </summary>
        public string idAttrSeparatorString { get; set; } = "__";


        /// <summary>
        /// fk asociada a id
        /// </summary>
        /// <remarks>
        /// Para algunas base de datos las fk estan directamente asociadas a las Id.<br/>
        /// Se debe indicar para que las operaciones sean mas eficientes
        /// </remarks>
        public bool fkId = false;


    }

}
