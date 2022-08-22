using MultiShop.Models.Request;
using MultiShop.Models.Response;
using MultiShop.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface IOrderDetailsRepository
    {
        Task<IEnumerable<OrderDetails>> GetAllOrdersDetails();
        Task<OrderDetails> GetOrderDetailsById(int id);
        Task<OrderdetailsResponse> CreateOrderDetails(OrderDetailsRequest request);
    }
}
