using MultiShop.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAllAdmin();
        Task<Admin> GetAdminById( int id);
        Task<Admin> CreateAdmin(Admin admin);
        Task<Admin> EditAdmin(Admin admin);
        Task<bool> DeleteAdmin(int id);
        bool IsAdminExist(int id);
    }
}
