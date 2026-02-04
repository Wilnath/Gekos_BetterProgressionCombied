using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    public class BitcoinConfig
    {
        public bool enable { get; set; }
        public bool cannotBuyGPU { get; set; }
        public bool overrideValue { get; set; }
        public int value { get; set; }
        public float btcFarmSpeedMult { get; set; }
        public float gpuBoostRate { get; set; }
        public int btcCapacity { get; set; }
    }
}
