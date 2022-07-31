using MultiShop.Models.Models;
using MultiShop.Models.Request;
using MultiShop.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface ICartDetailsRepository
    {
        Task<IEnumerable<CartDetails>> GetAllCartDetails();
        Task<CartDetails> GetCartDetailsById(int id);
        Task<CartDetailsCreateResponse> CreateCartDetails(CartDetailsCreateRequest cartDetails);
        Task<CartDetailsEditResponse> EditCartDetails(CartDetailsEditRequest cartDetails);
        Task<bool> DeleteCartDetails(int id);
        bool IsCartDetailsExist(int id);
    }
}
