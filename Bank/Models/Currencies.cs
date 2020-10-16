using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Currencies
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public double ExchangeRate { get; set; }
    }
}
