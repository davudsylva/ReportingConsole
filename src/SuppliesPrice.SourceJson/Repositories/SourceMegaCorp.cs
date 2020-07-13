using SuppliesPrice.Contracts.Interfaces;
using SuppliesPrice.Contracts.Models;
using SuppliesPrice.SourceJson.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuppliesPrice.SourceJson.Repositories
{
    public class SourceMegaCorp : SourceJsonBase
    {
        public SourceMegaCorp():base("megacorp.json")
        {
        }
    }
}