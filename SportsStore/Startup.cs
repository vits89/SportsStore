using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(
                routes => {
                    routes.MapRoute(
                        name: "Error",
                        template: "Error",
                        defaults: new { controller = "Error", action = "Error" }
                    );
                    routes.MapRoute(
                        name: null,
                        template: "{category}/Page{page:int}",
                        defaults: new { controller = "Product", action = "List" }
                    );
                    routes.MapRoute(
                        name: null,
                        template: "Page{page:int}",
                        defaults: new { controller = "Product", action = "List", page = 1 }
                    );
                    routes.MapRoute(
                        name: null,
                        template: "{category}",
                        defaults: new { controller = "Product", action = "List", page = 1 }
                    );
                    routes.MapRoute(
                        name: null,
                        template: "",
                        defaults: new { controller = "Product", action = "List", page = 1 }
                    );
                    routes.MapRoute(
                        name: null,
                        template: "{controller}/{action}/{id?}"
                    );
                }
            );

            // SeedData.EnsurePopulated(app);
            // IdentitySeedData.EnsurePopulated(app);
        }
    }
}
