using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitfinex.Net.Interfaces;
using Bitfinex.Net.Objects;
using Cryptocurrency.Agent.Core.Entities;
using Cryptocurrency.Agent.Core.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Cryptocurrency.Agent.Infrastructure.Repository
{
    public class BitfinexRepository : IBitfinexRepository
    {
        private IBitfinexClient _client;
        private IBitfinexSocketClient _socketClient;

        public BitfinexRepository(IBitfinexClient client, IBitfinexSocketClient socketClient)
        {
            _client = client;
            _socketClient = socketClient;
        }

        public Task<WebCallResult<IEnumerable<CryptocurrencySymbolOverview>>> Get24HPrices()
        {
            return _client.GetTickersAsync(default);
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
        public Task<CallResult<UpdateSubscription>> SubscribePairTickerUpdates(string pair, Action<DataEvent<CryptocurrencySymbolOverview>> streamHandler)
        {
            return _socketClient.SubscribeToTickerUpdatesAsync(pair, streamHandler);
        }

        public Task<CallResult<UpdateSubscription>> SubscribePairCandleUpdates(string pair, CryptocurrencyDataInterval interval, Action<DataEvent<IEnumerable<CryptocurrencyCandleData>>> streamHandler)
        {
            return _socketClient.SubscribeToKlineUpdatesAsync(pair, interval, streamHandler);
        }

        //public Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetPairHistoricalPrices(string pair)
        //{
        //    return _client.Spot.Market.GetHistoricalSymbolTradesAsync(pair, 10);
        //}
        public Task<WebCallResult<IEnumerable<CryptocurrencyCandleData>>> GetPairKlines(string pair, CryptocurrencyDataInterval interval, DateTime? startTime, DateTime? endTime, int? limit)
        {
            return _client.GetKlinesAsync(interval, pair, default, limit, startTime, endTime);
        }

        public async Task Unsubscribe(UpdateSubscription subscription)
        {
            await _socketClient.UnsubscribeAsync(subscription);
        }
    }
}
