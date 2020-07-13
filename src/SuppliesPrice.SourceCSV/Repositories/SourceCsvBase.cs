using SuppliesPrice.Contracts.Interfaces;
using SuppliesPrice.Contracts.Models;
using SuppliesPrice.SourceCSV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SuppliesPrice.SourceCSV.Repositories
{
    public class SourceCsvBase : IPriceSource
    {
        public string _feedFilename;

        public SourceCsvBase(string feedFilename)
        {
            _feedFilename = feedFilename;
        }

        public async Task<IEnumerable<ReportingSupply>> GetAll()
        {
            var allItems = new List<ReportingSupply>();
            var supplyItems = await GetPriceList();
            foreach (var item in supplyItems)
            {
                allItems.Add(new ReportingSupply()
                {
                    Id = item.Identifier,
                    ItemName = item.Desc,
                    Price = item.CostAUD,
                    Currency = "AUD"
                });
            }
            return allItems;
        }

        async Task<IEnumerable<SupplyItem>> GetPriceList()
        {
            var lines = await File.ReadAllLinesAsync($"Resources/{_feedFilename}");
            var first = true;
            var allItems = new List<SupplyItem>();
            foreach (var line in lines)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    var split = line.Split(",");
                    allItems.Add(new SupplyItem()
                    {
                        Identifier = split[0],
                        Desc = split[1],
                        Unit = split[2],
                        CostAUD = Decimal.Parse(split[3])
                    });
                }
            }

            return allItems;
        }
    }
}
