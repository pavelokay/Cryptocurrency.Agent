using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitfinex.Net.Interfaces;
using Bitfinex.Net.Objects;
using Cryptocurrency.Agent.Infrastructure.Entities;
using Cryptocurrency.Agent.Infrastructure.Interfaces;
using Cryptocurrency.Agent.Infrastructure.Utils.Mapper;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace Cryptocurrency.Agent.Infrastructure.Services
{
    public class BitfinexProvider : IBitfinexProvider
    {
        private readonly IBitfinexClient _client;
        private readonly IBitfinexSocketClient _socketClient;
        private readonly ILogger<BitfinexProvider> _logger;

        public BitfinexProvider(IBitfinexClient client, IBitfinexSocketClient socketClient, ILogger<BitfinexProvider> logger)
        {
            _client = client;
            _socketClient = socketClient;
            _logger = logger;
        }

        public async Task<IEnumerable<CryptocurrencySymbolOverview>> GetAllSymbols24HPricesAsync()
        {
            var response =  await _client.GetTickersAsync(default);
            if (response.Success)
            {
                var bitfinexTikersData = response.Data;
                return ObjectMapper.Mapper.Map<IEnumerable<CryptocurrencySymbolOverview>>(bitfinexTikersData);
            }
            else
            {
                _logger.LogError($"Don't get data from Bitfinex, error: {response.Error.Message}");
                return null;
            }
        }

        public async Task<CryptocurrencySymbolOverview> GetSymbol24HPricesAsync(string symbol)
        {
            var response =  await _client.GetTickerAsync(symbol);
            if (response.Success)
            {
                var bitfinexTikerData = response.Data.FirstOrDefault();
                return ObjectMapper.Mapper.Map<CryptocurrencySymbolOverview>(bitfinexTikerData);
            }
            else
            {
                _logger.LogError($"Don't get data from Bitfinex, error: {response.Error.Message}");
                return null;
            }
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
        public async Task<CallResult<UpdateSubscription>> SubscribeSymbolUpdatesAsync(string symbol, Action<DataEvent<BitfinexStreamSymbolOverview>> streamHandler)
        {
            return await _socketClient.SubscribeToTickerUpdatesAsync(symbol, streamHandler);
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeSymbolCandleUpdatesAsync(string symbol, CryptocurrencyDataInterval interval, Action<DataEvent<IEnumerable<BitfinexKline>>> streamHandler)
        {
            var intervalBitfinex = ObjectMapper.Mapper.Map<TimeFrame>(interval);
            return await _socketClient.SubscribeToKlineUpdatesAsync(symbol, intervalBitfinex, streamHandler);
        }

        //public Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetPairHistoricalPrices(string pair)
        //{
        //    return _client.Spot.Market.GetHistoricalSymbolTradesAsync(pair, 10);
        //}
        public async Task<IEnumerable<CryptocurrencyCandleData>> GetSymbolKlinesAsync(string symbol, CryptocurrencyDataInterval interval, DateTime? startTime, DateTime? endTime, int? limit)
        {
            var intervalBitfinex = ObjectMapper.Mapper.Map<TimeFrame>(interval);
            startTime = startTime.HasValue ? startTime : DateTime.Today.AddDays(-2);
            endTime = endTime.HasValue ? endTime : DateTime.Now;

            var response = await _client.GetKlinesAsync(intervalBitfinex, symbol, default, limit, startTime, endTime);
            if (response.Success)
            {
                var bitfinexKlinesData = response.Data;
                return ObjectMapper.Mapper.Map<IEnumerable<CryptocurrencyCandleData>>(bitfinexKlinesData);
            }
            else
            {
                _logger.LogError($"Don't get data from Bitfinex, error: {response.Error.Message}");
                return null;
            }
        }

        public async Task UnsubscribeAsync(UpdateSubscription subscription)
        {
            await _socketClient.UnsubscribeAsync(subscription);
        }
    }
}
