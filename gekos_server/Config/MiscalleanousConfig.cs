using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    public class MiscalleanousConfig
    {
        public bool removeFirFromQuests { get; set; }
        public bool removeFirFromHideout { get; set; }
        public bool removeFirFromFlea { get; set; }
        public float craftProductMultiplier { get; set; }
        public float craftTimeMultiplier { get; set; }
        public bool enableExtraQuestRewards { get; set; }
        public bool addCustomTrades { get; set; }
        public Dictionary<string, int> stackSizeOverride { get; set; }
        public Dictionary<string, List<int>> containerSizeChanges { get; set; }
        public Dictionary<string, int> priceChanges { get; set; }
    }
}
