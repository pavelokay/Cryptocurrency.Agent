using Coinbase.Pro;
using Coinbase.Pro.Models;
using Coinbase.Pro.WebSockets;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Server.Data
{
    public class CoinbaseDataProvider
    {
        private ICoinbaseProClient _client;
        private CoinbaseProWebSocket _socketClient;
        public CoinbaseDataProvider(ICoinbaseProClient client)
        {
            _client = client;
            _socketClient = new CoinbaseProWebSocket();
        }

        public async Task<List<Stats>> GetCoinbase24HPrices()
        {
            var stats = new List<Stats>();
            var products = await _client.MarketData.GetProductsAsync();
            for(var i = 0; i < 10; i++)
            {
                stats.Add(await _client.MarketData.GetStatsAsync(products[i].Id));
            }
            return stats;
        }

        public Task<List<Candle>> GetCoinbasePairKlins(string pair, int interval, DateTime startTime, DateTime endTime)
        {
            return _client.MarketData.GetHistoricRatesAsync(pair, startTime, endTime, interval);
        }
        public Subscription SubscribeCoinbasePairCandlesUpdates(string pair)
        {
            var sub = new Subscription
            {
                ProductIds =
                {
                    pair,
                },
                Channels =
                {
                    "hearbeat",
                }
            };
            _socketClient.SubscribeAsync(sub);
            return sub;
        }

        public void UnsubscribeCoinbase(Subscription sub)
        {
            _socketClient.Unsubscribe(sub);
        }

    }
}
