using IdentityFramwork.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiShop.DataAccess.Data;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.DataAccess.Infrastructure.Repository;
using MultiShop.DataAccess.Services;
using MultiShop.Models.Models;

namespace MultiShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICartDetailsRepository, CartDetailsRepository>();
            services.AddScoped<ICartHeaderRepository, CartHeaderRepository>();
            services.AddScoped<IUserClaimsPrincipalFactory<RegisterNewUser>, ApplicationUserClaimsPrincipleFactory>();
            services.AddScoped<IUserService, UserService>();


            //identity User
            services.AddIdentity<RegisterNewUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>();


            //register IuserAccountRepository and UserAccountRepository
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();



            services.AddSwaggerGen();
            

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });


            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Could Not Found");
            //});
        }
    }
}
