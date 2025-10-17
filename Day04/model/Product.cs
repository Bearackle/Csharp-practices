using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day04.model
{
    [Table("products")]
    public class Product
    {
        [Key]
        public int ProductId { set; get; }
        [Required]
        [StringLength(5)]
        public string Name { set; get; }
        public long Price { set; get; }
        [StringLength(5)]
        public string Provider { set; get; }
        public int BrandId {  set; get; }
        public Brand Brand { set; get; }

      
    }
}
