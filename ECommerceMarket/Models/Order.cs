using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new List<OrderProduct>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderedAt { get; set; }
        public decimal Price { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual List<OrderProduct> OrderProducts { get; set; }
    }
}
