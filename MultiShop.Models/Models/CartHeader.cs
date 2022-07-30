using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Models.Models
{
    public class CartHeader
    {
        [Key]
        public int Id{ get; set; }
        public Guid UserId { get; set; }
        public int NoOfItems { get; set; }
    }
}
