using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Domain.Entities
{
    public class ProductAttribute
    {
        public int ProductAttributeId {  get; set; }
        public string AttributeName {  get; set; }
        public long? AttributeNumericValue {  get; set; }
        public string AttributeCharacterValue { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}