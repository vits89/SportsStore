using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:SportsStoreProducts:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportsStoreIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            /* app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            }); */

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Error",
                    pattern: "Error",
                    defaults: new { controller = "Error", action = "Error" }
                );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}/Page{page:int}",
                    defaults: new { controller = "Product", action = "List" }
                );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Page{page:int}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{controller}/{action}/{id?}"
                );
            });

            // SeedData.EnsurePopulated(app);
            // IdentitySeedData.EnsurePopulated(app);
        }
    }
}
