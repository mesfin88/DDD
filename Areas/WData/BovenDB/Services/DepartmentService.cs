using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.WData.BovenDB.Services
{
    public class DepartmentService : IDepartment
    {
        BovenContext _context;

        public DepartmentService(BovenContext context)
        {
            _context = context;
        }

        public Task<List<Department>> getDepartments()
        {
            return Task.Run(() =>
            {
                List<Department> l = null;
                l = _context.Departments.ToList();
                return l;
            });
        }
    }
}
