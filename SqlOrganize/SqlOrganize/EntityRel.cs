using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{
    public class EntityRel
    {
        public string field_name { get; set; }
        public string entity_name { get; set; }
        public string field_ref_name { get; set; } = "id";
        public string parent_id { get; set; }
    }
}
