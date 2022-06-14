using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceMarket.Models
{
    public partial class Attribute
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ToFilter { get; set; }
        public List<PresetAttribute> PresetAttributes { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
    }
}
