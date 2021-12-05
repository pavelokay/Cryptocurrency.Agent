using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptocurrency.Agent.Infrastructure.Entities.CoinMarketCap;

namespace Cryptocurrency.Agent.Infrastructure.Interfaces
{
    public interface ICoinMarketCapProvider
    {
        Task<List<CoinMarketCapSymbolData>> GetSymbolsDataAsync();
    }
}
