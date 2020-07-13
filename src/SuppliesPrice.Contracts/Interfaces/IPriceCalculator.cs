using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPrice.Contracts.Interfaces
{
    public interface IPriceCalculator
    {
        decimal ConvertToAud(decimal price, string currency);
    }
}
