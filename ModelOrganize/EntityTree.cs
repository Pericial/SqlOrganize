
namespace ModelOrganize
{
    /*
    Lectura de json
    */
    public class EntityTree
    {
        public string fieldName { get; set; }
        public string refEntityName { get; set; }
        public string refFieldName { get; set; }
        public Dictionary<string, EntityTree> children { get; set; }

    }
}
