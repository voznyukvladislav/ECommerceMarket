using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class ProductAttribute
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public int AttributeId { get; set; }
        public int ProductId { get; set; }

        public virtual Attribute Attribute { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
