using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPrice.SourceJson.Models
{
    class Partner
    {
        public string Name { get; set; }
        public string PartnerType { get; set; }
        public string PartnerAddress { get; set; }
        public IEnumerable<Supply> Supplies { get; set; }
    }
}
