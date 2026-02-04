using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Helpers;

namespace gekos_server
{
    [Injectable(TypePriority = OnLoadOrder.Database + 1)]
    public class FleaChanges(
            DatabaseService databaseService,
            ItemHelper itemHelper
        ) : IOnLoad
    {
        public Task OnLoad()
        {
            Dictionary<MongoId, TemplateItem> allItems = databaseService.GetTables().Templates.Items;
            foreach (KeyValuePair<MongoId, TemplateItem> item in allItems)
            {
                bool allowed = Mod.config.fleaMarketChanges.fleaWhitelist.Contains(item.Key);
                if (Mod.config.fleaMarketChanges.stillAllowKeys && itemHelper.IsOfBaseclass(item.Key, BaseClasses.KEY) && item.Value.Properties.CanSellOnRagfair.Equals(true))
                {
                    allowed = true;
                }
                item.Value.Properties.CanRequireOnRagfair = allowed && item.Value.Properties.CanRequireOnRagfair.Equals(true);
                item.Value.Properties.CanSellOnRagfair = allowed;
            }

            return Task.CompletedTask;
        }
    }
}
