using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using System.Threading.Tasks;

namespace gekos_server
{
    [Injectable(InjectionType.Singleton, TypePriority = OnLoadOrder.Database+1)]
    class Mod(
            DatabaseService databaseService,
            ModHelper modHelper,
            ISptLogger<Mod> sptLogger
        ) : IOnLoad
    {
        public static ModConfig config;
        public static ISptLogger<Mod> logger;

        public Task OnLoad()
        {
            logger = sptLogger;
            logger.Info("Gekos better progression booted.");
            return Task.CompletedTask;
        }
    }
}
