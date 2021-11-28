using System.Collections.Generic;

namespace Cryptocurrency.Agent.Infrastructure.Entities.CoinMarketCap{
    public class CoinMarketCapRootData
    {
        public List<CoinMarketCapSymbolData> Data { get; set; }
        public CoinMarketCapStatusData Status { get; set; }
    }
}
