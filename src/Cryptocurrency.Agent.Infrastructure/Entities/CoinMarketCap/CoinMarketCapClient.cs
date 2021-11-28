using System;
using System.Net.Http;

namespace Cryptocurrency.Agent.Infrastructure.Entities.CoinMarketCap{
    public class CoinMarketCapClient
    {
        public HttpClient Client { get; set; }
        public CoinMarketCapClient(HttpClient httpClient){
            httpClient.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/");
            httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "b54ca9cc-027e-4281-9894-e7b234f479a0");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClentFactory");
            Client = httpClient;
        }
    }
}