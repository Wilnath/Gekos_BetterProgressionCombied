using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server.Config
{
    public class SkillCustomMultipliersConfig
    {
        public int GlobalXPMultiplier { get; set; }
        public Dictionary<string, float> SkillXPMultipliers { get; set; }
        public Dictionary<string, float> SkillBuffMultipliers { get; set; }
    }
}
