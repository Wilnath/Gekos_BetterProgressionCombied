using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    public record SecureContainerChangesConfig
    {
        public bool enable { get; set; }
        public string starterContainer { get; set; }
        public Dictionary<string, List<List<int>>> sizeChanges { get; set; }
        public Dictionary<string, Dictionary<MongoId, Reward>> AdditionalQuestRewards { get; set; }
    }
}
