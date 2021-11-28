using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Cryptocurrency.Agent.Core.Interfaces;
using Cryptocurrency.Agent.Core.Entities;

namespace Cryptocurrency.Agent.Infrastructure.Repository
{
    public class BinanceRepository : IBinanceRepository
    {
        private IBinanceClient _client;
        private IBinanceSocketClient _socketClient;

        public BinanceRepository(IBinanceClient client, IBinanceSocketClient socketClient)
        {
            _client = client;
            _socketClient = socketClient;
        }

        public Task<WebCallResult<IEnumerable<CryptocurrencySymbolOverview>>> Get24HPrices()
        {
            return _client.Spot.Market.GetTickerAsync();
        }

        public Task<CallResult<UpdateSubscription>> SubscribeAllTickerUpdates(Action<IEnumerable<CryptocurrencySymbolOverview>> tickHandler)
        {
            return _socketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(tickHandler);
        }

        public Task<CallResult<UpdateSubscription>> SubscribePairTickerUpdates(string pair, Action<CryptocurrencySymbolOverview> tickHandler)
        {
            return _socketClient.Spot.SubscribeToSymbolTickerUpdatesAsync(pair, tickHandler);
        }

        public Task<CallResult<UpdateSubscription>> SubscribePairCandleUpdates(string pair, CryptocurrencyDataInterval interval, Action<CryptocurrencyStreamCandleData> streamHandler)
        {
            return _socketClient.Spot.SubscribeToKlineUpdatesAsync(pair, interval, streamHandler);
        }

        public Task<WebCallResult<IEnumerable<CryptocurrencyRecentTrade>>> GetPairHistoricalPrices(string pair)
        {
            return _client.Spot.Market.GetHistoricalSymbolTradesAsync(pair, 10);
        }
        public Task<WebCallResult<IEnumerable<CryptocurrencyCandleData>>> GetPairKlines(string pair, CryptocurrencyDataInterval interval, DateTime? startTime, DateTime? endTime, int? limit)
        {
            return _client.Spot.Market.GetKlinesAsync(pair, interval, startTime, endTime, limit);
        }

        public async Task Unsubscribe(UpdateSubscription subscription)
        {
            await _socketClient.Unsubscribe(subscription);
        }
    }
}
