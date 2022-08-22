using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
   
    public class BaseController : ControllerBase
    { 
        public static object ErrorResponse(Exception ex)
        {
            return (StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
