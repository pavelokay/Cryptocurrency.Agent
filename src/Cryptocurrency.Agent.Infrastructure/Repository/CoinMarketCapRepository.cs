using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Cryptocurrency.Agent.Core.Entities.CoinMarketCap;
using Cryptocurrency.Agent.Core.Interfaces;
using Cryptocurrency.Agent.Server.Data.JsonPolicies;

namespace Cryptocurrency.Agent.Infrastructure.Repository
{
    public class CoinMarketCapRepository : ICoinMarketCapRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CoinMarketCapRepository(IHttpClientFactory httpClientFactory)
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
