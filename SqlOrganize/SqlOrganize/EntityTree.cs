
namespace SqlOrganize
{
    /*
    Lectura de json
    */
    public class EntityTree
    {
        public string FieldName { get; set; }
        public string EntityName { get; set; }
        public string FieldRefName { get; set; } = "id";
        public Dictionary<string, EntityTree> Children { get; set; }
    }
}
