using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccess.Data;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Request;
using MultiShop.Models.Response;
using MultiShop.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Order.ToListAsync();
        }
        public async Task<CreateOrderResponse> CreateOrder(OrderCreateRequest request)
        {
            var order = new Order
            {
                Email = request.Email,
                CustomerName = request.CustomerName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                PaymentMethod = request.PaymentMethod,
                GrandTotal=request.GrandTotal,
                OrderDate = request.OrderDate,
                OrderType = request.OrderType,
                UserFid = request.UserFid
            };
            var result = await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return new CreateOrderResponse
            {
                Order = result.Entity
            };
        }
        public async Task<Order> GetOrderById(int id)
        {
            var result = await _context.Order.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public async Task<EditOrderResponse> EditOrder(OrderEditRequest request)
        {
            var order = new Order
            {
                Id = request.Id,
                Email = request.Email,
                CustomerName = request.CustomerName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                PaymentMethod = request.PaymentMethod,
                OrderDate = request.OrderDate,
                GrandTotal=request.GrandTotal,
                OrderType = request.OrderType,
                UserFid = request.UserFid
            };
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return new EditOrderResponse
            {
                Order = order
            };
        }
        public async Task<bool> DeleteOrder(int id)
        {
            var result = await _context.Order.FirstOrDefaultAsync(x => x.Id == id);
            _context.Order.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool IsOrderExist(int id)
        {
            return _context.Order.Any(x => x.Id == id);
        }
    }
}
