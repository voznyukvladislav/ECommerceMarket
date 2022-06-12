using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Value { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
