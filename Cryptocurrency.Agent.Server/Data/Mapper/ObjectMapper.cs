using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Binance.Net.Interfaces;
using Bitfinex.Net.Objects;

namespace Cryptocurrency.Agent.Server.Data.Mapper
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
                CreateMap<IBinanceKline, KlinesData>().ReverseMap();
                CreateMap<BitfinexKline, KlinesData>()
                    .ForMember("CloseTime", opt => opt.MapFrom(c => c.Timestamp))
                    .ForMember("BaseVolume", opt => opt.MapFrom(c => c.Volume))
                    .ReverseMap();

                CreateMap<BitfinexKline, StreamKlin>()
                    .ForMember("CloseTime", opt => opt.MapFrom(c => c.Timestamp))
                    .ForMember("BaseVolume", opt => opt.MapFrom(c => c.Volume))
                    .ReverseMap();

                CreateMap<IBinanceStreamKlineData, StreamKlinData>()
                    .ForMember("Data", opt => opt.MapFrom(c => c.Data))
                    .ForMember("Symbol", opt => opt.MapFrom(c => c.Symbol))
                    .ReverseMap();
                CreateMap<IBinanceStreamKline, StreamKlin>().ReverseMap();


                //CreateMap<Category, CategoryModel>().ReverseMap();
                //CreateMap<Wishlist, WishlistModel>().ReverseMap();
                //CreateMap<Compare, CompareModel>().ReverseMap();
                //CreateMap<Order, OrderModel>().ReverseMap();
                //CreateMap<Cart, CartModel>().ReverseMap();
                //CreateMap<CartItem, CartItemModel>().ReverseMap();
            }
        }
    }
}
