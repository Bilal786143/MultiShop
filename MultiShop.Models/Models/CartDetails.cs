using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Models.Models
{
    public class CartDetails
    {
        public int Id { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int CartHeaderFId { get; set; }
        [ForeignKey("CartHeaderFId")]
        public virtual CartHeader CartHeader { get; set; }
        public int ProductFId { get; set; }
        [ForeignKey("ProductFId")]
        public virtual Product Product { get; set; }
    }
}
