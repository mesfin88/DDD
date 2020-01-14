using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

using Boven.Areas.WData.BovenDB;
using Boven.Areas.WData.BovenDB.Models;
using System.Collections.Generic;

namespace Boven.Areas.WData.IdentityDB.Services
{
    public class SeedIdentityService : ISeedIdentity
    {

        public async Task EnsurePopulated(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await CreateRoles(roleManager);
            await CreateUsers(services);
        }

        public async Task CreateRoles(RoleManager<IdentityRole> rManager)
        {
            if (!await rManager.RoleExistsAsync(Employee.EMP_ROLE_COORDINATOR))
                await rManager.CreateAsync(new IdentityRole(Employee.EMP_ROLE_COORDINATOR));
            if (!await rManager.RoleExistsAsync(Employee.EMP_ROLE_INVESTIGATOR))
                await rManager.CreateAsync(new IdentityRole(Employee.EMP_ROLE_INVESTIGATOR));
            if (!await rManager.RoleExistsAsync(Employee.EMP_ROLE_MANAGER))
                await rManager.CreateAsync(new IdentityRole(Employee.EMP_ROLE_MANAGER));
        }

        public async Task CreateUsers(IServiceProvider services)
        {
            UserManager<IdentityUser> uManager = services.GetRequiredService<UserManager<IdentityUser>>();
            IEmployee employeeAsset = services.GetRequiredService<IEmployee>();

            if (!uManager.Users.Any())
            {
                List<Employee> employees = await employeeAsset.getEmployees();
                foreach (Employee e in employees)
                {
                    IdentityUser u = new IdentityUser(e.EmployeeId);
                    var result = await uManager.CreateAsync(u, "ooo");
                    if (result.Succeeded)
                        await uManager.AddToRoleAsync(u, e.RoleTitle);
                }
            }
        }

    }
}
