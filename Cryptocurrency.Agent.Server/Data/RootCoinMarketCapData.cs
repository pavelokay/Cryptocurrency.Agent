using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Server.Data
{
    public class RootCoinMarketCapData
    {
        public List<CryptocurrencyCoinMarketData> Data { get; set; }
        public StatusMarketCapData Status { get; set; }
    }
}
