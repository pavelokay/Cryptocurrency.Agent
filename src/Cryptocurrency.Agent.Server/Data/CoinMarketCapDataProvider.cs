using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Cryptocurrency.Agent.Server.Data.JsonPolicies;

namespace Cryptocurrency.Agent.Server.Data
{
    public class CoinMarketCapDataProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CoinMarketCapDataProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<CryptocurrencyCoinMarketData>> GetCryptocurrenciesList()
        {
            var client = _httpClientFactory.CreateClient("CoinMarketCapClient");

            var result = await client.GetAsync("v1/cryptocurrency/listings/latest");

                if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var deserializeOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
                };

                var root = JsonSerializer.Deserialize<RootCoinMarketCapData>(content, deserializeOptions);

                var cryptocurrencies = root.Data;
                //var status = root.Status;

                return cryptocurrencies;
            }
            return null;
        }
    }
}
