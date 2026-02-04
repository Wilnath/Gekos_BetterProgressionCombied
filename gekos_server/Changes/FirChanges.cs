using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Eft.Hideout;

namespace gekos_server.Changes
{
    [Injectable(TypePriority = OnLoadOrder.Database + 1)]
    public class FirChanges(
            DatabaseService databaseService,
            LocaleService localeService
        ) : IOnLoad
    {
        public Task OnLoad()
        {
            if (Mod.config.misc.removeFirFromQuests)
            {
                RemoveFirFromQuests();
                RemoveFirFromRepeatables();
            }
            
            if (Mod.config.misc.removeFirFromFlea)
            {
                RemoveFirFromFlea();
            }

            if (Mod.config.misc.removeFirFromHideout)
            {
                RemoveFirFromHideout();
            }

            return Task.CompletedTask;
        }

        public void RemoveFirFromQuests()
        {
            foreach (Quest quest in databaseService.GetTables().Templates.Quests.Values)
            {
                var sets = new List<List<QuestCondition>?> {
                    quest.Conditions.AvailableForFinish,
                    quest.Conditions.AvailableForStart,
                    quest.Conditions.Fail,
                    quest.Conditions.Started,
                    quest.Conditions.Success
                };

                foreach (var set in sets)
                {
                    if (set == null)
                    {
                        continue;
                    }

                    foreach (var condition in set)
                    {
                        if (condition.ConditionType == "HandoverItem" || condition.ConditionType == "FindItem")
                        {
                            condition.OnlyFoundInRaid = false;
                        }
                    }
                }
            }

            foreach (var item in localeService.GetLocaleDb())
            {
                // TODO: Remove FiR text
            }
        }

        public void RemoveFirFromFlea()
        {
            databaseService.GetTables().Globals.Configuration.RagFair.IsOnlyFoundInRaidAllowed = false;
        }

        public void RemoveFirFromHideout()
        {
            List<HideoutArea> hideoutAreas = databaseService.GetTables().Hideout.Areas;

            foreach (var area in hideoutAreas)
            {
                foreach (var stage in area.Stages.Values)
                {
                    List<StageRequirement> itemReq = stage.Requirements.FindAll(item => item.Type == "Item");

                    foreach (var req in itemReq)
                    {
                        req.IsSpawnedInSession = false;
                    }
                }
            }
        }

        public void RemoveFirFromRepeatables()
        {
            // TODO
        }
    }
}
