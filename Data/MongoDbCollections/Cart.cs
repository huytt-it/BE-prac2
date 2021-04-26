using System;
using System.Collections.Generic;
using System.Text;

namespace Data.MongoDbCollections
{
    public class Cart
    {
        public string IdProduct { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}
