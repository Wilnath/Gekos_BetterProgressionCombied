using gekos_server.Config;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    [Injectable(TypePriority = OnLoadOrder.Database)]
    public class ConfigLoader(
            ModHelper modHelper,
            ISptLogger<ConfigLoader> logger
        ) : IOnLoad
    {
        private const string CONFIG_PATH = "/config";
        private const string CONFIG_NAME = "config.jsonc";
        private const string ADVANCED_CONFIG_PATH = "/config/advanced";
        private const string SECURE_CONTAINER_CONFIG_NAME = "secureContainerRewards.jsonc";
        private const string CUSTOM_ITEMS_CONFIG_NAME = "customItems.jsonc";

        public Task OnLoad()
        {
            string pathToMod = modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
            ModConfig config = modHelper.GetJsonDataFromFile<ModConfig>(pathToMod + CONFIG_PATH, CONFIG_NAME);

            if (config == null)
            {
                logger.Error("Failed to load config");
                return Task.CompletedTask;
            }


            // These are loaded in a separate file for... some reason
            // that reason is probably because of how this mod wants separation of config to work
            Dictionary<string, Dictionary<MongoId, Reward>> additionalQuestRewards 
                    = modHelper.GetJsonDataFromFile<Dictionary<string, Dictionary<MongoId, Reward>>>(pathToMod + ADVANCED_CONFIG_PATH, SECURE_CONTAINER_CONFIG_NAME);

            if (additionalQuestRewards == null)
            {
                logger.Error("Failed loading secure container quest changes");
                return Task.CompletedTask;
            }
            config.secureContainerProgression.AdditionalQuestRewards = additionalQuestRewards;
            config.customItemsConfig = modHelper.GetJsonDataFromFile<CustomItemsConfig>(pathToMod + ADVANCED_CONFIG_PATH, CUSTOM_ITEMS_CONFIG_NAME);
            config.customTradesConfig = modHelper.GetJsonDataFromFile<Dictionary<MongoId, Dictionary<MongoId, AddedTradeConfig>>>(pathToMod + ADVANCED_CONFIG_PATH, CUSTOM_TRADES_CONFIG_NAME);

            Mod.config = config;

            logger.Info("StrengthBuffLiftWeightInc: " + Mod.config.skillChanges.CustomMultipliers.SkillBuffMultipliers["StrengthBuffLiftWeightInc"].ToString());
            return Task.CompletedTask;
        }
    }

    public record ModConfig
    {
        public SecureContainerChangesConfig secureContainerProgression { get; set; }
        public FleaMarketConfig fleaMarketChanges { get; set; }
        public MiscalleanousConfig misc { get; set; }
        public BitcoinConfig bitcoinChanges { get; set; }
        public SICCBuffsConfig SICCBuffs { get; set; }
        public SkillChangesConfig skillChanges { get; set; }
        public CustomItemsConfig customItemsConfig { get; set; }
        public HideoutBuildsChangesConfig hideoutBuildsChanges { get; set; }
        public Dictionary<MongoId, Dictionary<MongoId, AddedTradeConfig>> customTradesConfig { get; set; }
    }
}
