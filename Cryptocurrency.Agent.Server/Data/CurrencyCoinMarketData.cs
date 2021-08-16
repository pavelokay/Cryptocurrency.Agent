using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Server.Data
{
    public class CurrencyCoinMarketData
    {
        public decimal Price { get; set; }

        [JsonPropertyName("volume_24h")]
        public decimal Volume24h { get; set; }
        public decimal PercentChange1h { get; set; }

        [JsonPropertyName("percent_change_24h")]
        public decimal PercentChange24h { get; set; }
        public decimal PercentChange7d { get; set; }
        public decimal MarketCap { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
