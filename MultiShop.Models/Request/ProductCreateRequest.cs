﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Models.Request
{
    public class ProductCreateRequest
    {
        [Required(ErrorMessage = "Product Name Is Required"), Display(Name = "Product Name")]
        [MaxLength(100, ErrorMessage = "Product Name is not Greater than 100")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Description Is Required"), Display(Name = "Product Description")]
        [MaxLength(500, ErrorMessage = "Product Description is not Greater than 500")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Product Sale Price Is Required"), Display(Name = "Product Price")]

        public decimal SalePrice { get; set; }

        [Display(Name = "Product Discount Price (Optional)")]
        public decimal? DiscountPrice { get; set; }
        
        [ForeignKey("Id")]
        public int CatFId { get; set; }
       

    } 
}