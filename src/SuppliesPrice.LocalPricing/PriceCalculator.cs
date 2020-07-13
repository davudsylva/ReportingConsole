using Microsoft.Extensions.Configuration;
using SuppliesPrice.Contracts.Interfaces;
using System;

namespace SuppliesPrice.LocalPricing
{
    public class PriceCalculator: IPriceCalculator
    {
        private readonly decimal _usdAudExchange;
        public PriceCalculator(IConfiguration configuration)
        {
            _usdAudExchange = Decimal.Parse(configuration["audUsdExchangeRate"]);
        }
        public decimal ConvertToAud(decimal price, string currency)
        {
            if (currency == "AUD")
            {
                return price;
            }
            else if (currency == "USD")
            {
                return price / _usdAudExchange;
            }
            else
            {
                throw new ArgumentOutOfRangeException("currency", currency, "Unhandled value");
            }
        }
    }
}
