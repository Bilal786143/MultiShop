using MultiShop.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admins>> GetAllAdmin();
        Task<Admins> GetAdminById( int id);
        Task<Admins> CreateAdmin(Admins admin);
        Task<Admins> EditAdmin(Admins admin);
        Task<bool> DeleteAdmin(int id);
        bool IsAdminExist(int id);
    }
}
