using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OrdersAppTask2.Models
{
    public partial class Order
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int OrderId { get; set; }
        public string Username { get; set; }
        public string ProductName { get; set; }

        public virtual Product ProductNameNavigation { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}
