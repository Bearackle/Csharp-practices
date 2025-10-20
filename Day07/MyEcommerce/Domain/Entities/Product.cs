using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEcommerce.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId {  get; set; }
        public virtual Category Category { get; set; }
        public long Price { get; set; }
        public int ProductStock { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<ProductAttribute> Attributes { get; set; }
    }
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public string ImagePath {  get; set; }
    }   
}