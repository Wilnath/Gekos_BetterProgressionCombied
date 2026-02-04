using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Utils.Json;

namespace gekos_server.Config
{
    [Injectable]
    public class CustomItems(
            DatabaseService databaseService
        )
    {
        public void AddNewItems()
        {
            // TODO: See if this is fun from gekos

            Dictionary<string, IEnumerable<Buff>> buffs = databaseService.GetTables().Globals.Configuration.Health.Effects.Stimulator.Buffs;
            Dictionary<MongoId, TemplateItem> items = databaseService.GetTables().Templates.Items;
            Dictionary<string, LazyLoad<Dictionary<string, string>>> locales = databaseService.GetTables().Locales.Global;
        }
    }
}
