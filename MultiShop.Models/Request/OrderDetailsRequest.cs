using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Models.Request
{
   public class OrderDetailsRequest
    {
        public int Id { get; set; }
        public int ProductQuantity { get; set; }
        public double SalePrice { get; set; }
        public double TotalPrice { get; set; }
        public int ProductFId { get; set; }
        public int OrderFId { get; set; }
       

    }
}
