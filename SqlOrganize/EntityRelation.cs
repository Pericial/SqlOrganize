using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{
    public class EntityRelation
    {
        public string fieldName { get; set; }
        public string refEntityName { get; set; }
        public string refFieldName { get; set; } = "id";
        public string? parentId { get; set; }
    }
}
