using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptocurrency.Agent.Core.Entities.CoinMarketCap;

namespace Cryptocurrency.Agent.Core.Interfaces
{
    public interface ICoinMarketCapRepository
    {
        Task<List<CoinMarketCapSymbolData>> GetCryptocurrenciesList();
    }
}
