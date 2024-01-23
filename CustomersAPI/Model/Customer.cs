using System;
using System.Collections.Generic;

#nullable disable

namespace CustomersAPI.Model
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
