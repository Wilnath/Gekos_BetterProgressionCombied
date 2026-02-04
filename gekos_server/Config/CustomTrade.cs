using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server.Config
{
    public record CustomTrade
    {
        public List<TemplateItem> items;
        public Dictionary<MongoId, BarterScheme> barter_scheme;
        public Dictionary<MongoId, int> loyal_level_items;
    }
}
