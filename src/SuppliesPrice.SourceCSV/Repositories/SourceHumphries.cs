using SuppliesPrice.Contracts.Interfaces;
using SuppliesPrice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuppliesPrice.SourceCSV.Repositories
{
    public class SourceHumphries: SourceCsvBase
    {
        public SourceHumphries(): base("humphries.csv")
        {
        }

    }
}
