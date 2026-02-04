using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    public class FleaMarketConfig
    {
        public bool disableFleaMarket { get; set; }
        public bool stillAllowKeys { get; set; }
        public List<string> fleaWhitelist { get; set; }
    }
}
