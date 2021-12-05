using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cryptocurrency.Agent.Infrastructure.Entities.CoinMarketCap;
using Cryptocurrency.Agent.Infrastructure.Interfaces;
using Cryptocurrency.Agent.Infrastructure.Utils.JsonPolicies;
using Microsoft.Extensions.Logging;

namespace Cryptocurrency.Agent.Infrastructure.Services
{
    public class CoinMarketCapProvider : ICoinMarketCapProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CoinMarketCapProvider> _logger;
        public CoinMarketCapProvider(IHttpClientFactory httpClientFactory, ILogger<CoinMarketCapProvider> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<CoinMarketCapSymbolData>> GetSymbolsDataAsync()
        {
            var client = _httpClientFactory.CreateClient("CoinMarketCapClient");

            var response = await client.GetAsync("v1/cryptocurrency/listings/latest");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var deserializeOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
                };

                var root = JsonSerializer.Deserialize<CoinMarketCapRootData>(content, deserializeOptions);

                var symbolsData = root.Data;
                //var status = root.Status;

                return symbolsData;
            }
            else
            {
                _logger.LogError($"Don't get data from CoinMarketCap, code: {response.StatusCode}");
            }
            return null;
        }
    }
}
