using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPTarkov.Server.Core.Models.Eft;

namespace gekos_server.Changes
{
    [Injectable(TypePriority = OnLoadOrder.Database + 1)]
    public class SkillChanges(
            DatabaseService databaseService
        ) : IOnLoad
    {
        public Task OnLoad()
        {
            SkillChangesConfig skillConfig = Mod.config.skillChanges;
            if (!skillConfig.enable)
            {
                return Task.CompletedTask;
            }

            SPTarkov.Server.Core.Models.Eft.Common.Config eftConfig = databaseService.GetGlobals().Configuration;
            eftConfig.SkillFreshEffectiveness = skillConfig.SkillFreshEffectiveness;
            eftConfig.SkillFreshPoints = skillConfig.SkillFreshPoints;
            eftConfig.SkillPointsBeforeFatigue = skillConfig.SkillPointsBeforeFatigue;
            eftConfig.SkillMinEffectiveness = skillConfig.SkillMinEffectiveness;
            return Task.CompletedTask;
        }
    }
}
