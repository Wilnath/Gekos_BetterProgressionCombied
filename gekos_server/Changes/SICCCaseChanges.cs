using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    [Injectable(TypePriority = OnLoadOrder.Database + 1)]
    public class SICCCaseChanges(
            DatabaseService databaseService
        ) : IOnLoad
    {
        public Task OnLoad()
        {
            if (!Mod.config.SICCBuffs.enable)
            {
                return Task.CompletedTask;
            }

            HashSet<MongoId> newFilter = new();

            var docsFilter = databaseService.GetTables().Templates.Items[ItemTpl.CONTAINER_DOCUMENTS_CASE].Properties.Grids.First().Properties.Filters.First().Filter;
            var SICCFilter = databaseService.GetTables().Templates.Items[ItemTpl.CONTAINER_SICC].Properties.Grids.First().Properties.Filters.First().Filter;

            if (Mod.config.SICCBuffs.canHoldWhatDocsCan)
            {
                newFilter.UnionWith(docsFilter);
            }
            newFilter.UnionWith(SICCFilter);
            foreach (var item in Mod.config.SICCBuffs.additionalWhitelistedItems)
            {
                newFilter.Add((MongoId)item);
            }

            databaseService.GetTables().Templates.Items[ItemTpl.CONTAINER_SICC].Properties.Grids.First().Properties.Filters.First().Filter = newFilter;
            return Task.CompletedTask;
        }
    }
}
