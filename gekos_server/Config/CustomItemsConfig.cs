using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Spt.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server.Config
{
    public class CustomItemsConfig
    {
        public Dictionary<string, List<Buff>> customBuffs { get; set; }
        public Dictionary<MongoId, TemplateItem> customItems { get; set; }
        // public Dictionary<MongoId, LocaleBase> customLocales { get; set; }
        public Dictionary<MongoId, CustomTrade> customTrades { get; set; }
    }
}
