using System;
using Cryptocurrency.Agent.Core.Interfaces;

namespace Cryptocurrency.Agent.Application.Services
{
    public class CryptocurrencyService
    {
        private readonly IBinanceRepository _binanceRepository;
        private readonly IBitfinexRepository _bitfinexRepository;

        Task<WebCallResult<IEnumerable<BitfinexSymbolOverview>>> Get24HPrices();
        //Task<WebCallResult<IEnumerable<BitfinexSymbolOverview>>> GetAllSymbols();
        Task<CallResult<UpdateSubscription>> SubscribePairTickerUpdates(string pair, Action<DataEvent<BitfinexStreamSymbolOverview>> streamHandler);

        Task<CallResult<UpdateSubscription>> SubscribePairCandleUpdates(string pair, TimeFrame interval, Action<DataEvent<IEnumerable<BitfinexKline>>> streamHandler);

        // Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetPairHistoricalPrices(string pair);
        Task<WebCallResult<IEnumerable<BitfinexKline>>> GetPairKlines(string pair, TimeFrame interval, DateTime? startTime, DateTime? endTime, int? limit);
        Task Unsubscribe(UpdateSubscription subscription); 
    }
}
