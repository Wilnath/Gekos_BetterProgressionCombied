using gekos_server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    public class SkillChangesConfig
    {
        public bool enable { get; set; }
        public float SkillFreshEffectiveness { get; set; }
        public float SkillFreshPoints { get; set; }
        public float SkillPointsBeforeFatigue { get; set; }
        public float SkillMinEffectiveness { get; set; }
        public SkillPointsSystemConfig SkillPointsSystem { get; set; }
        public SkillCustomMultipliersConfig CustomMultipliers { get; set; }
    }
}
