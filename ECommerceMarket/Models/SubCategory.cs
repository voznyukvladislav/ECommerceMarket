using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Presets = new List<Preset>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual List<Preset> Presets { get; set; }
    }
}
