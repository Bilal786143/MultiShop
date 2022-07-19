using MultiShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAllAdmin();
        Task<Admin> GetAdminById( int id);
        Task<Admin> CreateAdmin(Admin admin);
        Task<Admin> EditAdmin(Admin admin);
        Task DeleteAdmin(int id);

        

    }
}
