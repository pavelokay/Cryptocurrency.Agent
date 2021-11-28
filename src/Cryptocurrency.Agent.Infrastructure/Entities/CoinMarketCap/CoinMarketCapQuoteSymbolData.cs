using System.Text.Json.Serialization;

namespace Cryptocurrency.Agent.Infrastructure.Entities.CoinMarketCap{
    public class CoinMarketCapDataQuoteSymbol
    {
        [JsonPropertyName("USD")]
        public CoinMarketCapSymbolData USD { get; set; }
        [JsonPropertyName("BTC")]
        public CoinMarketCapSymbolData BTC { get; set; }
    }
}