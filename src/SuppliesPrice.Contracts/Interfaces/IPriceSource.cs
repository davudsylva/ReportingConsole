using SuppliesPrice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuppliesPrice.Contracts.Interfaces
{
    public interface IPriceSource
    {
        Task<IEnumerable<ReportingSupply>> GetAll();
    }
}
