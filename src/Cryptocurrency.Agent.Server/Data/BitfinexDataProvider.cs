using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitfinex.Net;
using Bitfinex.Net.Interfaces;
using Bitfinex.Net.Objects;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Cryptocurrency.Agent.Server.Data
{
    public class BitfinexDataProvider
    {
        private IBitfinexClient _client;
        private IBitfinexSocketClient _socketClient;

        public BitfinexDataProvider(IBitfinexClient client, IBitfinexSocketClient socketClient)
        {
            _client = client;
            _socketClient = socketClient;
        }

        public Task<WebCallResult<IEnumerable<BitfinexSymbolOverview>>> Get24HPrices()
        {
            return _client.GetTickerAsync(default, "ALL");
        }

        //public Task<CallResult<UpdateSubscription>> SubscribeAllTickerUpdates(Action<IEnumerable<IBinanceTick>> tickHandler)
        //{
        //    //return _socketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(tickHandler);
        //    return _socketClient.
        //}
        //public Task<WebCallResult<IEnumerable<BitfinexSymbolOverview>>> GetAllSymbols()
        //{
        //    return _client.get
        //}
        public Task<CallResult<UpdateSubscription>> SubscribePairTickerUpdates(string pair, Action<BitfinexStreamSymbolOverview> handler)
        {
            return _socketClient.SubscribeToTickerUpdatesAsync(pair, handler);
        }

        public Task<CallResult<UpdateSubscription>> SubscribePairCandleUpdates(string pair, TimeFrame interval, Action<IEnumerable<BitfinexKline>> streamHandler)
        {
            return _socketClient.SubscribeToKlineUpdatesAsync(pair, interval, streamHandler);
        }

        //public Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetPairHistoricalPrices(string pair)
        //{
        //    return _client.Spot.Market.GetHistoricalSymbolTradesAsync(pair, 10);
        //}
        public Task<WebCallResult<IEnumerable<BitfinexKline>>> GetPairKlines(string pair, TimeFrame interval, DateTime? startTime, DateTime? endTime, int? limit)
        {
            return _client.GetKlinesAsync(interval, pair, default, limit, startTime, endTime);
        }

        public async Task Unsubscribe(UpdateSubscription subscription)
        {
            await _socketClient.Unsubscribe(subscription);
        }
    }
}
