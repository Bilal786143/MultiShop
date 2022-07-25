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

        public async Task<Order> CreateOrder(Order order)
        {
            var result = await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return result.Entity;
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

        public async Task<Order> EditOrder(Order order)
        {
            bool isExist = IsOrderExist(order.Id);
            if (isExist)
            {
                var result = await _context.Order.FirstOrDefaultAsync(x => x.Id == order.Id);
                result.Id = order.Id;
                result.Email = order.Email;
                result.Address = order.Address;
                result.CustomerName = order.CustomerName;
                result.OrderDate = order.OrderDate;
                result.OrderType = order.OrderType;
                result.PaymentMethod = order.PaymentMethod;
                result.PhoneNumber = order.PhoneNumber;
                result.ProductFId = order.ProductFId;
                result.ProductPrice = order.ProductPrice;
                result.ProductQuantity = order.ProductQuantity;


                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            bool isExist = IsOrderExist(id);
            if (!isExist)
            {
                return false;
            }
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
