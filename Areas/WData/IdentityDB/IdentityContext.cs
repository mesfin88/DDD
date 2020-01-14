using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Boven.Areas.WData.IdentityDB
{
    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        public IdentityContext(Microsoft.EntityFrameworkCore.DbContextOptions<IdentityContext> ops)
            : base(ops) { }

    }
}
