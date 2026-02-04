using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.DI;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Spt.Server;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Utils;
using System.Linq;
using System.ComponentModel;
using SPTarkov.Server.Core.Models.Common;

namespace gekos_server
{
    [Injectable(InjectionType = InjectionType.Singleton, TypePriority = OnLoadOrder.Database+1)]
    public class SecureContainerChanges(
            DatabaseService databaseService,
            ModHelper modHelper,
            HashUtil hashUtil,
            ISptLogger<SecureContainerChanges> logger
        ) : IOnLoad
    {
        public Task OnLoad()
        {
            ApplySizeChanges();
            ApplyAdditionalQuestRewards();
            ApplyStarterContainer();
            return Task.CompletedTask;
        }

        public void ApplyAdditionalQuestRewards()
        {
            DatabaseTables tables = databaseService.GetTables();
            Dictionary<string, Dictionary<MongoId, Reward>> extraRewards = Mod.config.secureContainerProgression.AdditionalQuestRewards;
            foreach (KeyValuePair<string, Dictionary<MongoId, Reward>> questStateToAppend in extraRewards)
            {
                foreach (KeyValuePair<MongoId, Reward> questIDToReward in questStateToAppend.Value)
                {
                    tables.Templates.Quests[questIDToReward.Key].Rewards.TryGetValue(questStateToAppend.Key, out List<Reward> rewardList);
                    rewardList.Add(questIDToReward.Value);
                }
            }
        }

        public void ApplySizeChanges()
        {
            foreach (KeyValuePair<string, List<List<int>>> item in Mod.config.secureContainerProgression.sizeChanges)
            {
                TemplateItem containerItem = databaseService.GetTables().Templates.Items[item.Key];
                Grid gridTemplate = containerItem.Properties.Grids.First();
                List<List<int>> gridSizes = item.Value;
                List<Grid> newGrids = new();
                for (int i = 0; i < gridSizes.Count; i++)
                {
                    int cellsH = gridSizes[i][0];
                    int cellsV = gridSizes[i][1];
                    Grid newGrid = new();

                    newGrid.Name = (i + 1).ToString();
                    newGrid.Id = hashUtil.GetHashCode().ToString();
                    newGrid.Parent = "664a55d84a90fc2c8a6305c9";
                    newGrid.Properties = new GridProperties
                    {
                        Filters = gridTemplate.Properties.Filters,
                        CellsH = cellsH,
                        CellsV = cellsV,
                        MinCount = 0,
                        MaxCount = 0,
                        MaxWeight = 0,
                        IsSortingTable = false
                    };
                    newGrid.Prototype = "55d329c24bdc2d892f8b4567";

                    newGrids.Add(newGrid);
                }
                containerItem.Properties.Grids = newGrids;
            }
        }

        public void ApplyStarterContainer()
        {
            Dictionary<string, ProfileSides> profileTemplates = databaseService.GetTables().Templates.Profiles;
            foreach (KeyValuePair<string, ProfileSides> item in profileTemplates)
            {
                Item bearContainer = item.Value.Bear.Character.Inventory.Items.Find((Item x) => (x.SlotId == "SecuredContainer"));
                if (bearContainer != null)
                {
                    bearContainer.Template = Mod.config.secureContainerProgression.starterContainer;
                }

                Item usecContainer = item.Value.Usec.Character.Inventory.Items.Find((Item x) => (x.SlotId == "SecuredContainer"));
                if (usecContainer != null)
                {
                    usecContainer.Template = Mod.config.secureContainerProgression.starterContainer;
                }
            }
        }


    }
}
