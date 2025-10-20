using MyEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Domain.Entities
{

    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public ICollection<CartItem> CartItem { get; set; }
    }
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}