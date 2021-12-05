using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Cryptocurrency.Agent.Infrastructure.Interfaces;
using Cryptocurrency.Agent.Infrastructure.Entities;
using Cryptocurrency.Agent.Infrastructure.Utils.Mapper;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Cryptocurrency.Agent.Infrastructure.Services
{
    public class BinanceProvider : IBinanceProvider
    {
        private readonly IBinanceClient _client;
        private readonly IBinanceSocketClient _socketClient;
        private readonly ILogger<BinanceProvider> _logger;

        public BinanceProvider(IBinanceClient client, IBinanceSocketClient socketClient, ILogger<BinanceProvider> logger)
        {
            _client = client;
            _socketClient = socketClient;
            _logger = logger;
        }

        public async Task<IEnumerable<CryptocurrencySymbolOverview>> GetAllSymbols24HPricesAsync()
        {
            var response = await _client.Spot.Market.GetTickersAsync();
            if (response.Success)
            {
                var binanceTickersData = response.Data;
                return ObjectMapper.Mapper.Map<IEnumerable<CryptocurrencySymbolOverview>>(binanceTickersData);    
            }
            else
            {
                _logger.LogError($"Don't get data from Binance, error: {response.Error.Message}");
                return null;
            }
        }

        public async Task<CryptocurrencySymbolOverview> GetSymbol24HPricesAsync(string symbol)
        {
            var response = await _client.Spot.Market.GetTickerAsync(symbol);
            if (response.Success)
            {
                var binanceTickersData = response.Data;
                return ObjectMapper.Mapper.Map<CryptocurrencySymbolOverview>(binanceTickersData);    
            }
            else
            {
                _logger.LogError($"Don't get data from Binance, error: {response.Error.Message}");
                return null;
            }
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeAllSymbolsUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceTick>>> tickHandler)
        {
            return await _socketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync(tickHandler);
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeSymbolUpdatesAsync(string pair, Action<DataEvent<IBinanceTick>> tickHandler)
        {
            return await _socketClient.Spot.SubscribeToSymbolTickerUpdatesAsync(pair, tickHandler);
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeSymbolCandleUpdatesAsync(string symbol, CryptocurrencyDataInterval interval, Action<DataEvent<IBinanceStreamKlineData>> streamHandler)
        {
            var intervalBinance = ObjectMapper.Mapper.Map<KlineInterval>(interval);
            return await _socketClient.Spot.SubscribeToKlineUpdatesAsync(symbol, intervalBinance, streamHandler);
        }
        public async Task<IEnumerable<CryptocurrencyCandleData>> GetSymbolKlinesAsync(string symbol, CryptocurrencyDataInterval interval, DateTime? startTime, DateTime? endTime, int? limit)
        {
            var intervalBinance = ObjectMapper.Mapper.Map<KlineInterval>(interval);
            startTime = startTime.HasValue ? startTime : DateTime.Today.AddDays(-2);
            endTime = endTime.HasValue ? endTime : DateTime.Now;

            var response = await _client.Spot.Market.GetKlinesAsync(symbol, intervalBinance, startTime, endTime, limit);
            if (response.Success)
            {
                var binanceCandlesData = response.Data;
                return ObjectMapper.Mapper.Map<IEnumerable<CryptocurrencyCandleData>>(binanceCandlesData);
            }
            else
            {
                _logger.LogError($"Don't get data from Binance, error: {response.Error.Message}");
                return null;
            }
        }

        public async Task UnsubscribeAsync(UpdateSubscription subscription)
        {
            await _socketClient.UnsubscribeAsync(subscription);
        }
    }
}
