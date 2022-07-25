using MultiShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> CreateOrder(Order order);
        Task<Order> EditOrder(Order order);
        Task<bool> DeleteOrder(int id);
        bool IsOrderExist(int id);

    }
}
