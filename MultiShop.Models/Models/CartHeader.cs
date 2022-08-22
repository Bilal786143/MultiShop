using System.ComponentModel.DataAnnotations;

namespace MultiShop.Models.Models
{
    public class CartHeader
    {
        [Key]
        public int Id{ get; set; }
        public string UserId { get; set; }
    }
}
