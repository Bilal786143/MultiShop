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
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Admin> CreateAdmin(Admin admin)
        {
            var result = await  _context.Admin.AddAsync(admin);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAdmin(int id)
        {
            var result =   await _context.Admin.FindAsync(id);
            if (result != null)
            {
                _context.Admin.Remove(result);
                await _context.SaveChangesAsync();
                

            }
            
        }

        public async Task<Admin> EditAdmin(Admin admin)
        {
          _context.Admin.Update(admin);
          await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Admin> GetAdminById(int id)
        {
            var result =  await _context.Admin.FindAsync(id);
            return result;
        }

        public  async Task<IEnumerable<Admin>> GetAllAdmin()
        {

            var result =  await _context.Admin.ToListAsync();
            return result;
        }
    }
}
