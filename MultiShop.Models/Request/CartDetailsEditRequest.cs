using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Models.Request
{
    public class CartDetailsEditRequest
    {
        public int Id { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int CartHeaderFId { get; set; }
        public int ProductFId { get; set; }
    }
}
