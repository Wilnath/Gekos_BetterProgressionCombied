using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server.Config
{
    public class SkillPointsSystemConfig
    {
        public bool enable { get; set; }
        public int skillPointsPerLevel { get; set; }
        public bool automaticallyRefundOverflows { get; set; }
        public bool enableDeallocation { get; set; }
    }
}
