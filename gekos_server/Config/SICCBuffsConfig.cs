using SPTarkov.Server.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    public class SICCBuffsConfig
    {
        public bool enable { get; set; }
        public bool canHoldWhatDocsCan { get; set; }
        public List<string> additionalWhitelistedItems { get; set; }
    }
}
