using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using SuppliesPrice.Contracts.Interfaces;
using SuppliesPrice.Core;
using System;
using SuppliesPrice.SourceJson;
using SuppliesPrice.SourceCSV;
using SuppliesPrice.SourceJson.Repositories;
using SuppliesPrice.SourceCSV.Repositories;
using SuppliesPrice.LocalPricing;

namespace SuppliesPriceLister
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var services = ConfigureServices();
                var serviceProvider = services.BuildServiceProvider();

                var report = serviceProvider.GetService<PriceReport>();
                var output = await report.CreateReport();
                foreach (var line in output)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.ToString()}");
            }
        }

        private static IServiceCollection ConfigureServices()
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                       .AddJsonFile("appsettings.json", optional: false);

            IConfigurationRoot configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IPriceSource, SourceHumphries>();
            services.AddTransient<IPriceSource, SourceMegaCorp>();
            services.AddTransient<IPriceCalculator, PriceCalculator>();
            services.AddTransient<PriceReport>();
            services.AddSingleton<IConfiguration>(configuration);

            return services;
        }
    }
}
