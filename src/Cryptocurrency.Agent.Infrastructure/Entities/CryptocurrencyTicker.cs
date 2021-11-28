namespace Cryptocurrency.Agent.Infrastructure.Entities{
    /// <summary>
    ///  MiniTick info
    /// </summary>
    public class CryptocurrencyTicker{
        /// <summary>
        /// Symbol
        /// </summary>
        string Symbol { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        decimal LastPrice { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        decimal OpenPrice { get; set; }
        /// <summary>
        /// High
        /// </summary>
        decimal HighPrice { get; set; }
        /// <summary>
        /// Low
        /// </summary>
        decimal LowPrice { get; set; }
        /// <summary>
        /// Total traded volume
        /// </summary>
        decimal BaseVolume { get; set; }
        /// <summary>
        /// Total traded alternate asset volume
        /// </summary>
        decimal QuoteVolume { get; set; }
    }
}