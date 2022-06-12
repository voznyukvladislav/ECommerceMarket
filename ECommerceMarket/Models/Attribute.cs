using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class Attribute
    {
        public Attribute()
        {
            PresetAttributes = new List<PresetAttribute>();
            ProductAttributes = new List<ProductAttribute>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual List<PresetAttribute> PresetAttributes { get; set; }
        public virtual List<ProductAttribute> ProductAttributes { get; set; }
    }
}
