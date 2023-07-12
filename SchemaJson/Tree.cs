using System.Text.RegularExpressions;
using Utils;

namespace SchemaJson
{
    public class Tree
    {
        public string FieldId { get; set; }
        public string FieldName { get; set; }
        public string RefFieldName { get; set; }
        public string RefEntityName { get; set; }
        public List<Tree> Children { get; set; }
    }
}
        