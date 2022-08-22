using System.Collections.Generic;

namespace MultiShop.Models.Models
{
    public class Cart
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetails> CartDetails{ get; set; }
    }
}
