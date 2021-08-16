using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Server.Data
{
    public class StreamKlin
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        //public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal BaseVolume { get; set; }
        public int TradeCount { get; set; }
        public bool Final { get; set; }
    }
}
