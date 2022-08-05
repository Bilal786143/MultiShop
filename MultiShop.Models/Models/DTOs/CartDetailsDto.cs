using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MultiShop.Models.Models.DTOs
{
    public class CartDetailsDto
    {
        [Key]
        public int CartDetailsId { get; set; }
       
        public int CartHeaderFId { get; set; }
        [ForeignKey("CartHeaderFId")]
        public virtual CartHeaderDto CartHeader { get; set; }
        public int ProductFId { get; set; }
        [ForeignKey("ProductFId")]
        public virtual ProductDto Product { get; set; }
       public int Count { get; set; }



        //public int ProductQuantity { get; set; }
        //public decimal TotalPrice { get; set; 
    }
}
