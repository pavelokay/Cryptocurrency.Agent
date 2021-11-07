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
        Task<IEnumerable<CryptocurrencyTick>> Get24HPrices();
        Task<IEnumerable<CryptocurrencyTick>> SubscribeAllTickerUpdate(Action<IEnumerable<CryptocurrencyTick>> tickHandler);

        Task<CallResult<UpdateSubscription>> SubscribePairTickerUpdates(string pair, Action<CryptocurrencyTick> tickHandler);

        Task<CallResult<UpdateSubscription>> SubscribePairCandleUpdates(string pair, KlineInterval interval, Action<IBinanceStreamKlineData> streamHandler);
        Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetPairHistoricalPrices(string pair);
        Task<WebCallResult<IEnumerable<IBinanceKline>>> GetPairKlines(string pair, KlineInterval interval, DateTime? startTime, DateTime? endTime, int? limit);
        Task Unsubscribe(UpdateSubscription subscription);
    }
}
