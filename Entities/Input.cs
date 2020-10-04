using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebShell.Entities
{
    public class Input
    {
        public int Id { get; set; }
        public string Command { get; set; }
        public DateTime DateTime { get; set; }
    }
}
