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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var result = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        
        
        public async Task<Category> CreateCategory(Category category)
        {
            var result=await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        
        public async Task DeleteCategoryById(int id)
        {

            var result = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);

            if(result != null)
            {
                _context.Category.Remove(result);
                await _context.SaveChangesAsync();
            }
        
        }
        public async Task<Category> UpdateCategory(Category category)
        {
            var result = await _context.Category.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (result != null)
            {
                result.Id = category.Id;
                result.Name = category.Name;
                await _context.SaveChangesAsync();
                return result;
            }
            
            return null;
            
        }
    }
}
