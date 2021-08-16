using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Server.Data
{
    public class GeneralTickerData
    {
        public string Symbol { get; set; }
        public string SymbolWithSlash
        {
            get
            {
                return Symbol.Substring(0, 3) + "/" + Symbol.Substring(3);
            }
        }
        public string Exchange { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal LastPrice { get; set; }
        public decimal BaseVolume { get; set; }
        public decimal PriceChange { get; set; }
        public decimal PriceChangePercent { get; set; }
        public DateTime CloseTime { get; set; }
        public DateTime OpenTime { get; set; }
        public long TotalTrades { get; set; }

    }
}
