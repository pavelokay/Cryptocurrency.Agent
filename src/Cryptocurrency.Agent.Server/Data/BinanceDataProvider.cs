using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Binance.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Server.Data
{
    public class BinanceDataProvider
    {
        private IBinanceClient _client;
        private IBinanceSocketClient _socketClient;

        public BinanceDataProvider(IBinanceClient client, IBinanceSocketClient socketClient)
        {
            _client = client;
            _socketClient = socketClient;
        }

        public Task<WebCallResult<IEnumerable<IBinanceTick>>> Get24HPrices()
        {
            return _client.Spot.Market.Get24HPricesAsync();
        }

        public Task<CallResult<UpdateSubscription>> SubscribeAllTickerUpdates(Action<IEnumerable<IBinanceTick>> tickHandler)
        {
            return _socketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(tickHandler);
        }

        public Task<CallResult<UpdateSubscription>> SubscribePairTickerUpdates(string pair, Action<IBinanceTick> tickHandler)
        {
            return _socketClient.Spot.SubscribeToSymbolTickerUpdatesAsync(pair, tickHandler);
        }

        public Task<CallResult<UpdateSubscription>> SubscribePairCandleUpdates(string pair, KlineInterval interval, Action<IBinanceStreamKlineData> streamHandler)
        {
            return _socketClient.Spot.SubscribeToKlineUpdatesAsync(pair, interval, streamHandler);
        }

        public Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetPairHistoricalPrices(string pair)
        {
            return _client.Spot.Market.GetHistoricalSymbolTradesAsync(pair, 10);
        }
        public Task<WebCallResult<IEnumerable<IBinanceKline>>> GetPairKlines(string pair, KlineInterval interval, DateTime? startTime, DateTime? endTime, int? limit)
        {
            return _client.Spot.Market.GetKlinesAsync(pair, interval, startTime, endTime, limit);
        }

        public async Task Unsubscribe(UpdateSubscription subscription)
        {
            await _socketClient.Unsubscribe(subscription);
        }
    }
}
