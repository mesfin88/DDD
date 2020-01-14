using Microsoft.EntityFrameworkCore;

using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.WData.BovenDB
{
    public class BovenContext : DbContext
    {
        public BovenContext(DbContextOptions<BovenContext> ops)
            : base(ops) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Errand> Errands { get; set; }
        public DbSet<ErrandStatus> ErrandStatuses { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Sequence> Sequences { get; set; }
    }
} 

