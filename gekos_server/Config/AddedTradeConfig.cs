using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server.Config
{
    public class AddedTradeConfig
    {
        public MongoId item { get; set; }
        public int loyaltyLevel { get; set; }
        public List<BarterScheme> barterItems { get; set; }
    }
}
