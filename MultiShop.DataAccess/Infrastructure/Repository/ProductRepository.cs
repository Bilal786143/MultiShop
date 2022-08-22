using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccess.Data;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Request;
using MultiShop.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public ProductRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }               
        public async Task<Product> CreateProduct(ProductCreateRequest request,string picPath)
        {
            var product = new Product
            {
                Name = request.Name,
                CatFId = request.CatFId,
                Description = request.Description,
                SalePrice = request.SalePrice,
                DiscountPrice = request.DiscountPrice,
                ProductImagePath = picPath
            };
            await _dbcontext.Product.AddAsync(product);
            await _dbcontext.SaveChangesAsync();
            return product;
        }
        public async Task<bool> DeleteProducts(int id)
        {
            var result = await _dbcontext.Product.FirstOrDefaultAsync(x => x.Id == id);
            _dbcontext.Product.Remove(result);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<Product> EditProduct(ProductEditRequest request)
        {
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                CatFId = request.CatFId,
                Description = request.Description,
                SalePrice = request.SalePrice,
                DiscountPrice = request.DiscountPrice,
                //ProductImage = picPath
            };
            _dbcontext.Product.Update(product);
            await _dbcontext.SaveChangesAsync();
            return product;
        }
        public async Task<Product> GetProductById(int id)
        {
            var result = await _dbcontext.Product.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public bool IsProductExist(int id)
        {
            return _dbcontext.Product.Any(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbcontext.Product.ToListAsync();
        }
    }
}
