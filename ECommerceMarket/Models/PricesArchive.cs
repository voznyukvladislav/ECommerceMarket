using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class PricesArchive
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime ChangedAt { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
