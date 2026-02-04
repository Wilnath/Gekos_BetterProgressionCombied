using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server.Config
{
    public class HideoutBuildsChangesConfig
    {
        public bool enable { get; set; }
        public int threshold { get; set; }
        public float factor { get; set; }
        public bool roundDown { get; set; }
    }
}
