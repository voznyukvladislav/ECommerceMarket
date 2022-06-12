using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class Preset
    {
        public Preset()
        {
            PresetAttributes = new List<PresetAttribute>();
            Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; } = null!;
        public virtual List<PresetAttribute> PresetAttributes { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
