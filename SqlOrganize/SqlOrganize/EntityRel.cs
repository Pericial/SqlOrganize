using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{
    public class EntityRel
    {
        public string FieldName { get; set; }
        public string EntityName { get; set; }
        public string FieldRefName { get; set; } = "id";
        public string ParentId { get; set; }
    }
}
