using Moq;
using SuppliesPrice.Contracts.Interfaces;
using SuppliesPrice.Contracts.Models;
using SuppliesPrice.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SuppliesPrice.CoreTests
{
    public class ReportingTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(2, 0, 0)]
        [InlineData(0, 2, 0)]
        [InlineData(0, 0, 2)]
        [InlineData(2, 3, 4)]

        public async Task ReportShouldHandleVariousSizedLists(int list1Count, int list2Count, int list3Count)
        {
            var report = EstablishEnvironment(list1Count, list2Count, list3Count);

            var lines = await report.CreateReport();

            Assert.Equal(list1Count + list2Count + list3Count, lines.Count());
            Assert.Equal(list1Count, lines.Where(x => x.Contains("list1")).Count());
            Assert.Equal(list2Count, lines.Where(x => x.Contains("list2")).Count());
            Assert.Equal(list3Count, lines.Where(x => x.Contains("list3")).Count());
        }


        private PriceReport EstablishEnvironment(int list1Count, int list2Count, int list3Count)
        {
            var mockSource1 = new Mock<IPriceSource>();
            var list1 = new List<ReportingSupply>();
            for (int idx = 0; idx < list1Count; idx++)
            {
                list1.Add(new ReportingSupply() { Id = Guid.NewGuid().ToString(), ItemName = $"list1-item{idx}", Currency = "AUD", Price = 1.0M });
            }
            mockSource1.Setup(x => x.GetAll()).ReturnsAsync(list1);

            var mockSource2 = new Mock<IPriceSource>();
            var list2 = new List<ReportingSupply>();
            for (int idx = 0; idx < list2Count; idx++)
            {
                list2.Add(new ReportingSupply() { Id = Guid.NewGuid().ToString(), ItemName = $"list2-item{idx}", Currency = "AUD", Price = 1.0M });
            }
            mockSource2.Setup(x => x.GetAll()).ReturnsAsync(list2);

            var mockSource3 = new Mock<IPriceSource>();
            var list3 = new List<ReportingSupply>();
            for (int idx = 0; idx < list3Count; idx++)
            {
                list3.Add(new ReportingSupply() { Id = Guid.NewGuid().ToString(), ItemName = $"list3-item{idx}", Currency = "AUD", Price = 1.0M });
            }
            mockSource3.Setup(x => x.GetAll()).ReturnsAsync(list3);

            var mockPriceCalculator = new Mock<IPriceCalculator>();
            mockPriceCalculator.Setup(x => x.ConvertToAud(It.IsAny<decimal>(), It.IsAny<string>())).Returns(12.34M);

            var reporter = new PriceReport(
                new List<IPriceSource>() { mockSource1.Object, mockSource2.Object, mockSource3.Object }, 
                mockPriceCalculator.Object
            );

            return reporter;
        }
    }
}
