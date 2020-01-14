using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;


namespace Boven.Areas.WData.IdentityDB
{
    public interface ISeedIdentity
    {
        Task EnsurePopulated(IServiceProvider services);
        Task CreateRoles(RoleManager<IdentityRole> rManager);
        Task CreateUsers(IServiceProvider services);
    }
}