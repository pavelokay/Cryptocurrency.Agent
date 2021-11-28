using System.Text.Json.Serialization;

namespace Cryptocurrency.Agent.Core.Entities.CoinMarketCap{
    public class CoinMarketCapDataQuoteSymbol
    {
        [JsonPropertyName("USD")]
        public CoinMarketCapSymbolData USD { get; set; }
        [JsonPropertyName("BTC")]
        public CoinMarketCapSymbolData BTC { get; set; }
    }
}