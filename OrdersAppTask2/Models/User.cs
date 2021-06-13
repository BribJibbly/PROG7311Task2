using System;
using System.Collections.Generic;

#nullable disable

namespace OrdersAppTask2.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
