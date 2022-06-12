using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
        public decimal OrderedPrice { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
