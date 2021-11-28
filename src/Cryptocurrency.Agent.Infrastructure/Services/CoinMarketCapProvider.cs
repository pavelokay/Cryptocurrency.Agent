using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Cryptocurrency.Agent.Infrastructure.Entities.CoinMarketCap;
using Cryptocurrency.Agent.Infrastructure.Interfaces;
using Cryptocurrency.Agent.Infrastructure.Utils.JsonPolicies;

namespace Cryptocurrency.Agent.Infrastructure.Services
{
    public class CoinMarketCapProvider : ICoinMarketCapProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CoinMarketCapProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<CoinMarketCapSymbolData>> GetCryptocurrenciesList()
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

                var root = JsonSerializer.Deserialize<CoinMarketCapRootData>(content, deserializeOptions);

                var cryptocurrencies = root.Data;
                //var status = root.Status;

                return cryptocurrencies;
            }
            return null;
        }
    }
}
