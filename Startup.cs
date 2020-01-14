using Boven.Areas.WData.BovenDB;
using Boven.Areas.WData.BovenDB.Models;
using Boven.Areas.WData.BovenDB.Services;
using Boven.Areas.WData.IdentityDB;
using Boven.Areas.WData.IdentityDB.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Boven
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration conf) => Configuration = conf;


        public void ConfigureServices(IServiceCollection services)
        {

            //BOVEN_DB
            services.AddDbContext<BovenContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BovenConnection")));
            //BOVEN_DB_SERVICES
            services.AddScoped<ISeedBoven, SeedBovenService>();
            services.AddScoped<IErrand, ErrandService>();
            services.AddScoped<IEmployee, EmployeeService>();
            services.AddScoped<IErrandStatus, ErrandStatusService>();
            services.AddScoped<IDepartment, DepartmentService>();
            services.AddScoped<IPicture, PictureService>();
            services.AddScoped<ISample, SampleService>();

            //IDENTITY_DB
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            //IDENTITY_DB_SERVICES
            services.AddIdentity<Microsoft.AspNetCore.Identity.IdentityUser, Microsoft.AspNetCore.Identity.IdentityRole>(
                config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequiredLength = 3;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                }
                ).AddEntityFrameworkStores<IdentityContext>();
            services.AddScoped<ISeedIdentity, SeedIdentityService>();


            //Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireEmployeeRole", policy => policy.RequireRole(Employee.EMP_ROLE_COORDINATOR, Employee.EMP_ROLE_INVESTIGATOR, Employee.EMP_ROLE_MANAGER));
                options.AddPolicy("RequireCoordinatorRole", policy => policy.RequireRole(Employee.EMP_ROLE_COORDINATOR));
                options.AddPolicy("RequireManagerRole", policy => policy.RequireRole(Employee.EMP_ROLE_MANAGER));
                options.AddPolicy("RequireInvestigatorRole", policy => policy.RequireRole(Employee.EMP_ROLE_INVESTIGATOR));
            });



            //MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Context accessor in Controllers and Components
            services.AddHttpContextAccessor();

            //Session
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(15);   
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseCookiePolicy();

            //For use of SignInManger etc in Views and Controllers.
            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
