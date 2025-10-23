using MyEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Domain.Entities
{
    public class Order
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public int Status {  get; set; }
        public string Address { get; set; }
    }
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public Product Product { get; set; }
        public long ProductPrice {  get; set; }
        public int Quantity { get; set; }
    }
}