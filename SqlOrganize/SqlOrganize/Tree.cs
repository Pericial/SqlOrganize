
namespace SqlOrganize
{
    /*
    Lectura de json
    */
    public class Tree
    {
        public string fieldName { get; set; }
        public string refEntityName { get; set; }
        public string refFieldName { get; set; } = "id";
        public Dictionary<string, Tree> children { get; set; }
    }
}
