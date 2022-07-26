using MultiShop.Models.Request;
using MultiShop.Models.Response;
using MultiShop.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<CreateOrderResponse> CreateOrder(OrderCreateRequest order);
        Task<EditOrderResponse> EditOrder(OrderEditRequest request);
        Task<bool> DeleteOrder(int id);
        bool IsOrderExist(int id);
    }
}
