namespace SqlOrganize
{
    public class Config
    {
        /*
        Path a los archivos del modelo
        */
        public string ModelPath { get; set; } = ".\\model\\";
        
        /*
        Sufijo de sobrescritura de archivos del modelo
        */
        public string modelSuffix { get; set; } = "_";

        /*
        String de conexin con la base de datos
        */
        public string connectionString { get; set; } = "_";



    }

}
