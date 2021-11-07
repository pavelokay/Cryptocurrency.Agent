using Binance.Net;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Spot;
using Bitfinex.Net;
using Bitfinex.Net.Interfaces;
using Bitfinex.Net.Objects;
//using Coinbase.Pro.WebSockets;
//using Coinbase.Pro.Models;
//using Coinbase.Pro;
using Cryptocurrency.Agent.Server.Data;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polly;

namespace Cryptocurrency.Agent.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            BitfinexClient.SetDefaultOptions(new BitfinexClientOptions()
            {
                LogVerbosity = LogVerbosity.Debug
            });

            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                LogVerbosity = LogVerbosity.Debug
                //ApiCredentials = new ApiCredentials("Credentials", "Credentials")
            });
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddHttpClient("CoinMarketCapClient", client =>
            {
                client.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/");
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "b54ca9cc-027e-4281-9894-e7b234f479a0");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));

            AddCryptoServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
        

        private void AddCryptoServices(IServiceCollection services)
        {
            services.AddTransient<IBinanceClient, BinanceClient>();
            services.AddTransient<IBinanceSocketClient, BinanceSocketClient>();

            services.AddTransient<IBitfinexClient, BitfinexClient>();
            services.AddTransient<IBitfinexSocketClient, BitfinexSocketClient>();

            //services.AddTransient<ICoinbaseProClient, CoinbaseProClient>();

            services.AddSingleton<BinanceDataProvider>();
            services.AddSingleton<BitfinexDataProvider>();
            services.AddSingleton<CoinMarketCapDataProvider>();
            //services.AddSingleton<CoinbaseDataProvider>();
        }
    }
}
