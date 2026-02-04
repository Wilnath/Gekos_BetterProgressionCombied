using gekos_server.Config;
using Microsoft.Extensions.Logging;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server.Changes
{
    [Injectable(TypePriority = OnLoadOrder.Database + 1)]
    public class BuildChanges(DatabaseService databaseService) : IOnLoad
    {
        public Task OnLoad()
        {
            if (Mod.config.hideoutBuildsChanges.enable)
            {
                ChangeHideoutBuildCosts();
            }
            return Task.CompletedTask;
        }

        public void ChangeHideoutBuildCosts()
        {
            HideoutBuildsChangesConfig config = Mod.config.hideoutBuildsChanges;

            foreach (var area in databaseService.GetTables().Hideout.Areas)
            {
                foreach (var stage in area.Stages.Values)
                {
                    var nonCurrencyReq = stage.Requirements.FindAll((req) => !Utilities.IsCurrency(req.TemplateId));
                    foreach (var requirement in nonCurrencyReq)
                    {
                        if (requirement.Count != null && requirement.Count != 0)
                        {
                            float newCount = (float)requirement.Count;

                            newCount -= config.threshold;
                            if (newCount > 0)
                            {
                                newCount *= config.factor;
                            }
                            newCount += config.threshold;

                            if (config.roundDown)
                            {
                                requirement.Count = (int)Math.Floor(newCount);
                            }
                            else
                            {
                                requirement.Count = (int)Math.Ceiling(newCount);
                            }

                        }
                    }
                }
            }
        }
    }
}
