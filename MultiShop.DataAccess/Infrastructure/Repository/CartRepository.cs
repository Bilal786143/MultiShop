using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiShop.DataAccess.Data;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Models.Models;
using MultiShop.Models.Models.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        public CartRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderfromDb = await _context.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderfromDb != null)
            {
                _context.CartDetails.RemoveRange(_context.CartDetails.Where(u => u.CartHeaderFId == cartHeaderfromDb.Id));
                _context.CartHeader.Remove(cartHeaderfromDb);
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);
            //check if product exist in db 
            //if not exist  create it 
            var productInDb = await _context.Product.FirstOrDefaultAsync(x => x.Id == cartDto.CartDetails.FirstOrDefault().ProductFId);
            if (productInDb == null)
            {
                _context.Product.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }
            //check if cartHeader is null Create CartHeader and cartdetails 
            //first we find out user id 
            var carHeaderFromDb = await _context.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
            if (carHeaderFromDb == null)
            {
                //create cartHeader And CartDetails
                _context.CartHeader.Add(cart.CartHeader);
                await _context.SaveChangesAsync();

                //passing CartHeaderId from CartHeader To CartHeaderFid in CartDetails 

                cart.CartDetails.FirstOrDefault().CartHeaderFId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;

                //Add Cart Details 

                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }

            //if Cart Header Is Not null
            //check if details have same product 
            else
            {
                //To Get CartDetails From Database
                var cartDetailsFromDb = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(u => u.ProductFId == cart.CartDetails.FirstOrDefault().ProductFId && u.CartHeaderFId == carHeaderFromDb.Id);

                if (cartDetailsFromDb == null)
                {
                    //create product Details 
                    cart.CartDetails.FirstOrDefault().CartHeaderFId = carHeaderFromDb.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //update the count or cart details 
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }

            }
            return _mapper.Map<CartDto>(cart);


        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _context.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId)
            };
            cart.CartDetails = _context.CartDetails.Where(x => x.CartHeaderFId == cart.CartHeader.Id).Include(u => u.Product);
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _context.CartDetails.FirstOrDefaultAsync(x => x.CartDetailsId == cartDetailsId);
                int totalCountOfCartitems = _context.CartDetails.Where(x => x.CartHeaderFId == cartDetails.CartHeaderFId).Count();
                _context.Remove(cartDetails);
                if (totalCountOfCartitems == 1)
                {
                    var cartHeaderToremove = await _context.CartHeader.FirstOrDefaultAsync(x => x.Id == cartDetails.CartHeaderFId);
                    _context.CartHeader.Remove(cartHeaderToremove);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }


}