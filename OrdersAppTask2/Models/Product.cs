using System;
using System.Collections.Generic;

#nullable disable

namespace OrdersAppTask2.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public string ProductName { get; set; }
        public int? ProductPrice { get; set; }
        public string ProductCategory { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
