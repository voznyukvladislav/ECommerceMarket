using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class PresetAttribute
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public int PresetId { get; set; }

        public virtual Attribute Attribute { get; set; } = null!;
        public virtual Preset Preset { get; set; } = null!;
    }
}
