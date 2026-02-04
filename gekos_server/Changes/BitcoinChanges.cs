using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPTarkov.Server.Core.Models.Eft.Hideout;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;

namespace gekos_server
{
    [Injectable(TypePriority = OnLoadOrder.Database + 1)]
    public class BitcoinChanges(
            DatabaseService databaseService
        ) : IOnLoad
    {
        public Task OnLoad()
        {
            if (!Mod.config.bitcoinChanges.enable)
            {
                return Task.CompletedTask;
            }

            if (Mod.config.bitcoinChanges.overrideValue)
            {
                HandbookItem item = databaseService.GetTables().Templates.Handbook.Items.Find((item) => item.Id == ItemTpl.BARTER_PHYSICAL_BITCOIN);
                if (item == null)
                {
                    Mod.logger.Error("Could not find base bitcoin to edit");
                    return Task.CompletedTask;
                }
                item.Price = Mod.config.bitcoinChanges.value;
            }

            List<HideoutProduction> btcProduction = databaseService.GetTables().Hideout.Production.Recipes.FindAll((production) => production.EndProduct == ItemTpl.BARTER_PHYSICAL_BITCOIN);
            foreach (HideoutProduction prod in btcProduction)
            {
                prod.ProductionTime = Math.Round((double)prod.ProductionTime / Mod.config.bitcoinChanges.btcFarmSpeedMult);
                prod.ProductionLimitCount = Mod.config.bitcoinChanges.btcCapacity;
            }

            databaseService.GetTables().Hideout.Settings.GpuBoostRate = Mod.config.bitcoinChanges.gpuBoostRate;

            if (Mod.config.bitcoinChanges.cannotBuyGPU)
            {
                foreach (Trader trader in databaseService.GetTables().Traders.Values)
                {
                    if (trader.Assort == null)
                    {
                        continue;
                    }
                    trader.Assort.Items = trader.Assort.Items.FindAll((item) => item.Template != "57347ca924597744596b4e71" || Utilities.IsBarterTrade(item, trader));
                }
            }

            return Task.CompletedTask;
        }
    }
}
