using System;
using System.ComponentModel.DataAnnotations;

namespace MultiShop.Models.Request
{
    public class OrderCreateRequest
    { 
        public string Email { get; set; }
        public string CustomerName { get; set; } 
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public double GrandTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderType { get; set; }
        public string UserFid { get; set; }
        public int ProductFId { get; set; }

    }
}
