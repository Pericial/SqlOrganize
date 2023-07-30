using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelOrganize
{
    public class EntityRelation
    {
        public string fieldName { get; set; }
        public string refEntityName { get; set; }
        public string refFieldName { get; set; }
        public string? parentId { get; set; }
    }
}
