using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Models.Eft.Hideout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace gekos_server
{
    [Injectable(TypePriority = OnLoadOrder.Database + 1)]
    internal class CraftingChanges(
            DatabaseService databaseService
        ) : IOnLoad
    {
        private readonly List<MongoId> forbbidenCrafts =
        [
            ItemTpl.BARTER_PHYSICAL_BITCOIN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_GEARCRATE_BLUE_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_GEARCRATE_GREEN_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_GEARCRATE_VIOLET_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_JEWELRYCRATE_BLUE_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_JEWELRYCRATE_GREEN_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_JEWELRYCRATE_VIOLET_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_JUNKCRATE_BLUE_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_JUNKCRATE_GREEN_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_JUNKCRATE_VIOLET_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_WEAPONCRATE_BLUE_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_WEAPONCRATE_GREEN_OPEN,
            ItemTpl.RANDOMLOOTCONTAINER_ARENA_WEAPONCRATE_VIOLET_OPEN,
            ItemTpl.DRINK_CANISTER_WITH_PURIFIED_WATER,
            ItemTpl.DRINK_BOTTLE_OF_FIERCE_HATCHLING_MOONSHINE
        ];

        public Task OnLoad()
        {
            List<HideoutProduction> crafts = databaseService.GetTables().Hideout.Production.Recipes.FindAll((production) => { return !forbbidenCrafts.Contains(production.EndProduct); });
            float craftProductMultiplier = Mod.config.misc.craftProductMultiplier;
            float craftTimeMultiplier = Mod.config.misc.craftTimeMultiplier;
            foreach (var craft in crafts)
            {
                craft.Count *= Convert.ToInt32(craftProductMultiplier);
                craft.ProductionTime *= craftTimeMultiplier;
            }
            return Task.CompletedTask;
        }
    }
}
