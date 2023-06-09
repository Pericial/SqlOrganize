
namespace SqlOrganize
{
    /*
    Lectura de json
    */
    public class EntityTree
    {
        public string field_name { get; set; }
        public string entity_name { get; set; }
        public string field_ref_name { get; set; } = "id";
        public Dictionary<string, EntityTree> children { get; set; }
    }
}
