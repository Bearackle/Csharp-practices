using MyEcommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Domain.Models
{
    public class OrderViewModel
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public PaymentMethod PaymentMethod { get; set; } 
    }
}