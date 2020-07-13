using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPrice.Contracts.Models
{
    public class ReportingSupply
    {
        public string Id { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
