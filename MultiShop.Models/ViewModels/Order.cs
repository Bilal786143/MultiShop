using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Models.ViewModels
{
    public class Order
    {
        public int Id { get; set; }
        
        public string Email { get; set; }

        public string CustomerName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string OrderType { get; set; }


        public int ProductFId { get; set; }
        [ForeignKey("Id")]
        public virtual Product Product { get; set; }

    
    
    }
}
