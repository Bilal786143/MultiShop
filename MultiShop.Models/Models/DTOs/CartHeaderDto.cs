using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Models.Models.DTOs
{
    public class CartHeaderDto
    {
   
        public int Id{ get; set; }
        public string UserId { get; set; }



        //public int NoOfItems { get; set; }
    }
}
