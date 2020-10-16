using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Currencies
    {
        public string Currency { get; set; }
        public int id { get; set; }
        public float ExchangeRate { get; set; }
    }
}
