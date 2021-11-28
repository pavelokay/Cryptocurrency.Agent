using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Cryptocurrency.Agent.Core.Entities;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Cryptocurrency.Agent.Core.Interfaces
{
    /// <summary>
    /// Connect to Binance API
    /// </summary>
    public interface IBinanceRepository
    {
        Task<WebCallResult<IEnumerable<CryptocurrencySymbolOverview>>> Get24HPrices();
        Task<CallResult<UpdateSubscription>> SubscribeAllTickerUpdates(Action<IEnumerable<CryptocurrencySymbolOverview>> tickHandler);

        Task<CallResult<UpdateSubscription>> SubscribePairTickerUpdates(string pair, Action<CryptocurrencySymbolOverview> tickHandler);

        Task<CallResult<UpdateSubscription>> SubscribePairCandleUpdates(string pair, CryptocurrencyDataInterval interval, Action<CryptocurrencyStreamCandleData> streamHandler);
        Task<WebCallResult<IEnumerable<CryptocurrencyRecentTrade>>> GetPairHistoricalPrices(string pair);
        Task<WebCallResult<IEnumerable<CryptocurrencyCandleData>>> GetPairKlines(string pair, CryptocurrencyDataInterval interval, DateTime? startTime, DateTime? endTime, int? limit);
        Task Unsubscribe(UpdateSubscription subscription);
        
    }
}
