using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccess.Data;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<Product> CreateProduct(Product product)
        {
             _dbcontext.Product.Add(product);
            await _dbcontext.SaveChangesAsync();
            return product;

        }


        public async Task<bool> DeleteProducts(int id)
        {
            bool isExist = IsProductExist(id);
            if (!isExist)
            {

                return false;


            }

            var result = await _dbcontext.Product.FirstOrDefaultAsync(x => x.Id == id);
            _dbcontext.Product.Remove(result);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Product> EditProduct(Product product)
        {
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
            var result = await _dbcontext.Product.ToListAsync();
            return result;
        }


    }
}
