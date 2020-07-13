using SuppliesPrice.Contracts.Interfaces;
using SuppliesPrice.Contracts.Models;
using SuppliesPrice.SourceJson.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuppliesPrice.SourceJson.Repositories
{
    public class SourceJsonBase : IPriceSource
    {
        public string _feedFilename;

        public SourceJsonBase(string feedFilename)
        {
            _feedFilename = feedFilename;
        }

        public async Task<IEnumerable<ReportingSupply>> GetAll()
        {
            var allItems = new List<ReportingSupply>();
            var priceList = await GetPriceList();
            foreach (var partner in priceList.Partners)
            {
                foreach (var supply in partner.Supplies)
                {
                    allItems.Add(new ReportingSupply()
                    {
                        Id = supply.Id.ToString(),
                        ItemName = supply.Description,
                        Price = supply.PriceInCents / 100,
                        Currency = "USD"
                    });
                }
            }
            return allItems;
        }

        async Task<PriceList> GetPriceList()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var jsonString = await File.ReadAllTextAsync($"Resources/{_feedFilename}");
            var priceList = JsonSerializer.Deserialize<PriceList>(jsonString, options);
            return priceList;
        }
    }
}