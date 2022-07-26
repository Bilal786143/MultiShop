using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<RegisterNewUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
    
    }
}
