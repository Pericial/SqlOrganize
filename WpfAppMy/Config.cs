using SqlOrganize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy
{
    public class Config : SqlOrganize.Config
    {
        public string? emailDocentePassword { get; set; }
        public string? emailDocenteHost { get; set; }
        public string? emailDocenteFromAddress { get; set; }
        public string? emailDocenteFromName { get; set; }
        public string? emailDocenteBcc { get; set; }
        public string? emailDocenteUser { get; set; }

    }
}
