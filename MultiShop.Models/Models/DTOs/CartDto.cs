using System.Collections.Generic;
namespace MultiShop.Models.Models.DTOs
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails{ get; set; }
    }
}
