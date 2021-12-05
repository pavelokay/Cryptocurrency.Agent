using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Binance.Net.Interfaces;
using Bitfinex.Net.Objects;
using Cryptocurrency.Agent.Infrastructure.Entities;

namespace Cryptocurrency.Agent.Infrastructure.Utils.Mapper
{
    public class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<AspnetRunDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;

        public class AspnetRunDtoMapper : Profile
        {
            public AspnetRunDtoMapper()
            {
                CreateMap<IBinanceTick, CryptocurrencySymbolOverview>().ReverseMap();
                CreateMap<IBinanceKline, CryptocurrencyCandleData>().ReverseMap();

                CreateMap<IBinanceStreamKlineData, CryptocurrencyStreamCandleData>()
                    .ForMember("Data", opt => opt.MapFrom(c => c.Data))
                    .ForMember("Symbol", opt => opt.MapFrom(c => c.Symbol))
                    .ReverseMap();
                CreateMap<IBinanceStreamKline, CryptocurrencyStreamCandle>().ReverseMap();

                CreateMap<BitfinexSymbolOverview, CryptocurrencySymbolOverview>().ReverseMap();
                CreateMap<BitfinexKline, CryptocurrencyCandleData>()
                    .ForMember("CloseTime", opt => opt.MapFrom(c => c.Timestamp))
                    .ForMember("BaseVolume", opt => opt.MapFrom(c => c.Volume))
                    .ReverseMap();

                CreateMap<BitfinexKline, CryptocurrencyStreamCandle>()
                    .ForMember("CloseTime", opt => opt.MapFrom(c => c.Timestamp))
                    .ForMember("BaseVolume", opt => opt.MapFrom(c => c.Volume))
                    .ReverseMap();
            }
        }
    }
}
