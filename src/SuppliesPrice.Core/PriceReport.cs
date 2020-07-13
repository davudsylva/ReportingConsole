using SuppliesPrice.Contracts.Interfaces;
using SuppliesPrice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuppliesPrice.Core
{
    public class PriceReport : IPriceReport
    {
        private readonly IEnumerable<IPriceSource> _allSources;
        private readonly IPriceCalculator _priceCalculator;

        public PriceReport(IEnumerable<IPriceSource> allSources, IPriceCalculator priceCalculator)
        {
            _allSources = allSources;
            _priceCalculator = priceCalculator;
        }

        public async Task<IEnumerable<string>> CreateReport() 
        {

            var tasks = new List<Task<IEnumerable<ReportingSupply>>>();
            foreach (var source in _allSources)
            {
                tasks.Add(source.GetAll());
            }
            await Task.WhenAll(tasks);

            var combined = new List<ReportingSupply>();
            foreach (var executed in tasks)
            {
                combined.AddRange(executed.Result);
            }

            var processed = combined
                .Select(x => new
                {
                    Id = x.Id,
                    ItemName = x.ItemName,
                    Price = _priceCalculator.ConvertToAud(x.Price, x.Currency)
                })
                .OrderBy(x => 0 - x.Price)
                .Select(x => $"{x.Id}, {x.ItemName}, {x.Price:C2}")
                .ToList();

            return processed;
        }
    }
}
