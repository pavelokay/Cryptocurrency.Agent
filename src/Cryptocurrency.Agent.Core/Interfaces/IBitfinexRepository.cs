using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitfinex.Net.Objects;
using Cryptocurrency.Agent.Core.Entities;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Cryptocurrency.Agent.Core.Interfaces
{
    public interface IBitfinexRepository
    {
        Task<WebCallResult<IEnumerable<CryptocurrencySymbolOverview>>> Get24HPrices();
        //Task<WebCallResult<IEnumerable<BitfinexSymbolOverview>>> GetAllSymbols();
        Task<CallResult<UpdateSubscription>> SubscribePairTickerUpdates(string pair, Action<DataEvent<CryptocurrencySymbolOverview>> streamHandler);

        Task<CallResult<UpdateSubscription>> SubscribePairCandleUpdates(string pair, CryptocurrencyDataInterval interval, Action<DataEvent<IEnumerable<CryptocurrencyCandleData>>> streamHandler);

        // Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetPairHistoricalPrices(string pair);
        Task<WebCallResult<IEnumerable<CryptocurrencyCandleData>>> GetPairKlines(string pair, CryptocurrencyDataInterval interval, DateTime? startTime, DateTime? endTime, int? limit);
        Task Unsubscribe(UpdateSubscription subscription);
    }
}
