using MultiShop.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(Category category);
        Task<bool> DeleteCategoryById(int id);
        Task<Category> UpdateCategory(Category category);
        bool IsCategoryExist(int id);
    }
}
