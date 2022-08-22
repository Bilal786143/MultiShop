using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MultiShop.Models.Models
{
    public class CartDetails
    {
        [Key]
        public int CartDetailsId { get; set; }
        public int CartHeaderFId { get; set; }
        [ForeignKey("CartHeaderFId")]
        public virtual CartHeader CartHeader { get; set; }
        public int ProductFId { get; set; }
        [ForeignKey("ProductFId")]
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}
