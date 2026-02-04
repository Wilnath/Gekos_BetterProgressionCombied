using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    public static class Utilities
    {
        private static readonly List<MongoId> CURRENCIES =
        [
            "5449016a4bdc2d6f028b456f", //Roubles
            "5696686a4bdc2da3298b456a", //Dollars
            "569668774bdc2da2298b4568", //Euroes
            "5d235b4d86f7742e017bc88a", //GP Coin
            "6656560053eaaa7a23349c86"  //Lega medal
        ];

        public static bool IsCurrency(MongoId templateId)
        {
            return CURRENCIES.Contains(templateId);
        }

        public static bool IsBarterTrade(Item item, Trader trader)
        {
            List<List<BarterScheme>> scheme = trader.Assort.BarterScheme[item.Id];

            foreach (var ask in scheme)
            {
                foreach (var ask1 in ask)
                {
                    if (!IsCurrency(ask1.Template))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
