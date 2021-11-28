using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cryptocurrency.Agent.Infrastructure.Entities.CoinMarketCap{
    public class CoinMarketCapSymbolData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }

        [JsonPropertyName("Cmc_rank")]
        public int CMCRank { get; set; }
        public int NumMarketPairs { get; set; }
        public decimal CirculatingSupply { get; set; }
        public decimal TotalSupply { get; set; }
        public decimal? MaxSupply { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime DateAdded { get; set; }
        public List<string> Tags { get; set; }
        public object Platform { get; set; }
        public CoinMarketCapDataQuoteSymbol Quote { get; set; }
    }
}
