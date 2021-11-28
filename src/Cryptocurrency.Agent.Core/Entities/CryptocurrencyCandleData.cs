using System;

public class CryptocurrencyCandleData{
     //
        // Сводка:
        //     The time this candlestick opened
        DateTime OpenTime { get; set; }
        //
        // Сводка:
        //     The price at which this candlestick opened
        decimal Open { get; set; }
        //
        // Сводка:
        //     The highest price in this candlestick
        decimal High { get; set; }
        //
        // Сводка:
        //     The lowest price in this candlestick
        decimal Low { get; set; }
        //
        // Сводка:
        //     The price at which this candlestick closed
        decimal Close { get; set; }
        //
        // Сводка:
        //     The volume traded during this candlestick
        decimal BaseVolume { get; set; }
        //
        // Сводка:
        //     The close time of this candlestick
        DateTime CloseTime { get; set; }
        //
        // Сводка:
        //     The volume traded during this candlestick in the asset form
        decimal QuoteVolume { get; set; }
        //
        // Сводка:
        //     The amount of trades in this candlestick
        int TradeCount { get; set; }
        //
        // Сводка:
        //     Taker buy base asset volume
        decimal TakerBuyBaseVolume { get; set; }
        //
        // Сводка:
        //     Taker buy quote asset volume
        decimal TakerBuyQuoteVolume { get; set; }
}