using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Models.Models.DTOs
{
   public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public Object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string>ErrorMessage  { get; set; }
    }
}
