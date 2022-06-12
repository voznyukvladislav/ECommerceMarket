using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new List<SubCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual List<SubCategory> SubCategories { get; set; }
    }
}
