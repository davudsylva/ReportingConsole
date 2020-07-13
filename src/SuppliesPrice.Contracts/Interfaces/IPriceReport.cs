using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuppliesPrice.Contracts.Interfaces
{
    public interface IPriceReport
    {
        Task<IEnumerable<string>> CreateReport();
    }
}
