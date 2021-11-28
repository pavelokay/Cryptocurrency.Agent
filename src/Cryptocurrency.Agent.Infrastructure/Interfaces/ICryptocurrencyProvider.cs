using System;

namespace Cryptocurrency.Agent.Infrastructure.Interfaces
{
    public interface ICryptocurrencyProvider
    {
        private readonly IBinanceProvider _binanceProvider;
        private readonly IBitfinexProvider _bitfinexProvider;

        public ICryptocurrencyProvider(IBinanceProvider binanceProvider, IBitfinexProvider bitfinexProvider){
            
        }
}
}
