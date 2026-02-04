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
    internal class ContainerChanges(DatabaseService databaseService) : IOnLoad
    {
        public Task OnLoad()
        {
            foreach (KeyValuePair<string, List<int>> item in Mod.config.misc.containerSizeChanges)
            {
                if (!databaseService.GetTables().Templates.Items.ContainsKey(item.Key))
                {
                    continue;
                }

                int sizeH = item.Value[0];
                int sizeV = item.Value[1];
                databaseService.GetTables().Templates.Items[item.Key].Properties.Grids.First().Properties.CellsH = sizeH;
                databaseService.GetTables().Templates.Items[item.Key].Properties.Grids.First().Properties.CellsV = sizeV;
            }
            return Task.CompletedTask;
        }
    }
}
