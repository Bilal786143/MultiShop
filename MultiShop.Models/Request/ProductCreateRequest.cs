using Microsoft.AspNetCore.Http;

namespace MultiShop.Models.Request
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }  
        public string Description { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public IFormFile ProductImage { get; set; }
        public int CatFId { get; set; }
    } 
}
