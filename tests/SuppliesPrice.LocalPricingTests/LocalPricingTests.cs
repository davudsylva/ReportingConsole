using Microsoft.Extensions.Configuration;
using Moq;
using SuppliesPrice.LocalPricing;
using System;
using Xunit;

namespace SuppliesPrice.LocalPricingTests
{
    public class LocalPricingTests
    {
        [Fact]
        public void AudShouldConvertToAud()
        {
            var calculator = EstablishEnvironment();
            Assert.Equal(1.5M, calculator.ConvertToAud(1.5M, "AUD"));
        }

        [Fact]
        public void UsdShouldConvertToAud()
        {
            var calculator = EstablishEnvironment();
            Assert.Equal(3.0M, calculator.ConvertToAud(1.5M, "USD"));
        }

        [Fact]
        public void UnknownShouldThrowAnException()
        {
            var calculator = EstablishEnvironment();
            Assert.Throws<ArgumentOutOfRangeException>(() => calculator.ConvertToAud(1.5M, "NZD"));
        }

        private PriceCalculator EstablishEnvironment()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "audUsdExchangeRate")]).Returns("0.5").ToString();

            var priceCalculator = new PriceCalculator(mockConfiguration.Object);
            return priceCalculator;
        }
    }
}
