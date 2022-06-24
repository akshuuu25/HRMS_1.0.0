
using AspNetCoreHero.ToastNotification;
using HRMS.Email;
using HRMS.Models;
using HRMS.Repository;
using IdentityFramework.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace UserIdentity
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
            services.AddAntiforgery();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>();
            services.AddIdentityCore<ApplicationUser>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
           
          
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.SignIn.RequireConfirmedEmail = true;
            //});
           
            services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));
            services.Configure<EmailHelper>(Configuration.GetSection("EmailConfiguration"));
            services.AddDistributedMemoryCache();
           
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(3000);
               

            });
            // services.AddMvc(options =>
            // {
            //    var policy = new AuthorizationPolicyBuilder()
            //                  .RequireAuthenticatedUser()
            //                  .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //}).AddXmlSerializerFormatters();
            services.AddNotyf(config => { config.DurationInSeconds = 5; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

            services.AddScoped<IAccountRepository, AccountRepository>();
          

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , IServiceProvider service)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
         // app.UseUnobtrusiveAjax();

            app.UseRouting();
     
            app.UseAuthentication();
            app.UseStatusCodePages(async context=>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == 404)
                {
                    response.Redirect("/Account/AdminAccess");
                }
            });
            app.UseSession();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=LogIn}/{id?}");
               
            });
           //CreateUserRoles(service).Wait();
        }
    }
}
