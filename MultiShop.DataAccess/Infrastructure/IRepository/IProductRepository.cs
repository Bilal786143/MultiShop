using MultiShop.Models.Request;
using MultiShop.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(ProductCreateRequest product);
        Task<Product> EditProduct(ProductEditRequest product);
        Task<bool> DeleteProducts(int id);
        bool IsProductExist(int id);
    }
}
