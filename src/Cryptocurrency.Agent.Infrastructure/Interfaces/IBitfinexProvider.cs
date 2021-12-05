using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitfinex.Net.Objects;
using Cryptocurrency.Agent.Infrastructure.Entities;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Cryptocurrency.Agent.Infrastructure.Interfaces
{
    public interface IBitfinexProvider
    {
        Task<IEnumerable<CryptocurrencySymbolOverview>> GetAllSymbols24HPricesAsync();
        Task<CryptocurrencySymbolOverview> GetSymbol24HPricesAsync(string symbol);
        //Task<WebCallResult<IEnumerable<BitfinexSymbolOverview>>> GetAllSymbols();
        Task<CallResult<UpdateSubscription>> SubscribeSymbolUpdatesAsync(string symbol, Action<DataEvent<BitfinexStreamSymbolOverview>> streamHandler);

        Task<CallResult<UpdateSubscription>> SubscribeSymbolCandleUpdatesAsync(string symbol, CryptocurrencyDataInterval interval, Action<DataEvent<IEnumerable<BitfinexKline>>> streamHandler);

        // Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetPairHistoricalPrices(string pair);
        Task<IEnumerable<CryptocurrencyCandleData>> GetSymbolKlinesAsync(string symbol, CryptocurrencyDataInterval interval, DateTime? startTime, DateTime? endTime, int? limit);
        Task UnsubscribeAsync(UpdateSubscription subscription);
    }
}
