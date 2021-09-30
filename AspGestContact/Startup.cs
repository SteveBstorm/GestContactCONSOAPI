using DataAccessLayer.Interface;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Interfaces;

namespace AspGestContact
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
            services.AddControllersWithViews();
            //j'ajoute la possibilité d'utiliser l'état de session
            services.AddDistributedMemoryCache();
            services.AddSession();
            //si je le souhaite je peux le configurer pour luidonner des options
            //services.AddSession(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.IdleTimeout = TimeSpan.FromMinutes(25);
            //    options.Cookie.IsEssential = true;
            //});
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            //});


            //définition de la connectionstring
            //services.AddSingleton(sp => new Connection(Configuration.GetConnectionString("Tour")));
            //Pour ceux qui travailleraient sur 2 machines différentes (tour et portable par exemple)
            //services.AddSingleton(sp => new Connection(Configuration.GetConnectionString(Environment.MachineName.Equals("DESKTOP-P56A26Q") ? "Portable" : "Tour")));
            
            //Injection des services
            services.AddScoped<IUserGlobalRepo, UserGlobalService>();
            services.AddScoped<IUserClientRepo, UserClientService>();

            services.AddScoped<IContactGlobalRepo, ContactGlobalService>();
            services.AddScoped<IContactClientRepo, ContactClientService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //je lui dit qu'on va utiliser les sessions
            app.UseSession();
            //on va utiliser des polices (des règles)
            app.UseCookiePolicy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
