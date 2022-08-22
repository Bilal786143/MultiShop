using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccess.Data;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models;
using MultiShop.Models.Request;
using MultiShop.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.Repository
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<OrderdetailsResponse> CreateOrderDetails(OrderDetailsRequest request)
        {
            var orderDetails = new OrderDetails()
            {
               OrderFId= request.OrderFId,
               ProductFId = request.ProductFId,
               SalePrice = request.SalePrice,
               TotalPrice = request.TotalPrice,
               ProductQuantity = request.ProductQuantity
            };
            var result = await  _context.OrderDetails.AddAsync(orderDetails);
            await _context.SaveChangesAsync();
            return new OrderdetailsResponse
            {
                OrderDetails = result.Entity
            };
        }

        public async Task<IEnumerable<OrderDetails>> GetAllOrdersDetails()
        {
          return await _context.OrderDetails.ToListAsync();
        }
        public async  Task<OrderDetails> GetOrderDetailsById(int id)
        {
            var result =  await _context.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
            if(result != null)
            {
                return result;
            }
            return null;
        }
    }
}
