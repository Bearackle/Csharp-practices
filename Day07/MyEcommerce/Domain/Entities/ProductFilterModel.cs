using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Domain.Entities
{
    public class ProductFilterModel
    {
        public int? CategoryId { get; set; }
        public long? MinPrice { get; set; }
        public long? MaxPrice { get; set; }
        public string Sort { get; set; }
    }
}