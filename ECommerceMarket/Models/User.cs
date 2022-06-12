using System;
using System.Collections.Generic;

namespace ECommerceMarket.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual List<Order> Orders { get; set; }
    }
}
