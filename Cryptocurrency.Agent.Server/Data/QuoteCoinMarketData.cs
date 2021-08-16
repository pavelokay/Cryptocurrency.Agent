using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Server.Data
{
    public class QuoteCoinMarketData
    {
        [JsonPropertyName("USD")]
        public CurrencyCoinMarketData USD { get; set; }
        [JsonPropertyName("BTC")]
        public CurrencyCoinMarketData BTC { get; set; }
    }
}
