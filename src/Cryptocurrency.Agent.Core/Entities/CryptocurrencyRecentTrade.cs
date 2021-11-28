public class CryptocurrencyRecentTrade{
    //
        // Сводка:
        //     The id of the trade
        long OrderId { get; set; }
        //
        // Сводка:
        //     The price of the trade
        decimal Price { get; set; }
        //
        // Сводка:
        //     The base quantity of the trade
        decimal BaseQuantity { get; set; }
        //
        // Сводка:
        //     The quote quantity of the trade
        decimal QuoteQuantity { get; set; }
        //
        // Сводка:
        //     The timestamp of the trade
        DateTime TradeTime { get; set; }
        //
        // Сводка:
        //     Whether the buyer is maker
        bool BuyerIsMaker { get; set; }
        //
        // Сводка:
        //     Whether the trade was made at the best match
        bool IsBestMatch { get; set; }
}