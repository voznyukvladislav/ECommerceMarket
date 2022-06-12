using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new List<OrderProduct>();
            PricesArchives = new List<PricesArchive>();
            ProductAttributes = new List<ProductAttribute>();
        }

        public int Id { get; set; }
        public int? DiscountId { get; set; }
        public int PresetId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = null!;
        public string PhotosJson { get; set; } = null!;

        public virtual Discount? Discount { get; set; }
        public virtual Preset Preset { get; set; } = null!;
        public virtual List<OrderProduct> OrderProducts { get; set; }
        public virtual List<PricesArchive> PricesArchives { get; set; }
        public virtual List<ProductAttribute> ProductAttributes { get; set; }
    }
}
