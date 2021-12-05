using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Cryptocurrency.Agent.Infrastructure.Entities;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Cryptocurrency.Agent.Infrastructure.Interfaces
{
    /// <summary>
    /// Connect to Binance API
    /// </summary>
    public interface IBinanceProvider
    {
        Task<IEnumerable<CryptocurrencySymbolOverview>> GetAllSymbols24HPricesAsync();
        Task<CryptocurrencySymbolOverview> GetSymbol24HPricesAsync(string symbol);
        Task<CallResult<UpdateSubscription>> SubscribeAllSymbolsUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceTick>>> tickHandler);

        Task<CallResult<UpdateSubscription>> SubscribeSymbolUpdatesAsync(string symbol, Action<DataEvent<IBinanceTick>> tickHandler);

        Task<CallResult<UpdateSubscription>> SubscribeSymbolCandleUpdatesAsync(string symbol, CryptocurrencyDataInterval interval, Action<DataEvent<IBinanceStreamKlineData>> streamHandler);
        Task<IEnumerable<CryptocurrencyCandleData>> GetSymbolKlinesAsync(string symbol, CryptocurrencyDataInterval interval, DateTime? startTime, DateTime? endTime, int? limit);
        Task UnsubscribeAsync(UpdateSubscription subscription);
        
    }
}
