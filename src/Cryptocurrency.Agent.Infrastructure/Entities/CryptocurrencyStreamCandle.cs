namespace Cryptocurrency.Agent.Infrastructure.Entities
{
    public class CryptocurrencyStreamCandle
    {
        //
        // Сводка:
        //     Interval
        CryptocurrencyDataInterval Interval { get; set; }
        //
        // Сводка:
        //     Is this kline final
        bool Final { get; set; }
        //
        // Сводка:
        //     Id of the first trade in this kline
        long FirstTrade { get; set; }
        //
        // Сводка:
        //     Id of the last trade in this kline
        long LastTrade { get; set; }
    }
}