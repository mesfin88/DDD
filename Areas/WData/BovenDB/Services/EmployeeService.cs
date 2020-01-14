using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.WData.BovenDB.Services
{
    public class EmployeeService : IEmployee
    {
        private BovenContext _context;

        public EmployeeService(BovenContext context)
        {
            _context = context;
        }

        public Task<List<Employee>> getEmployees(string departmentId = null)
        {
            IQueryable<Employee> e = _context.Employees;
            if (departmentId != null && departmentId != "null")
                e = e.Where(p => p.DepartmentId.Equals(departmentId));
            return Task.Run(() =>
            {
                return e.ToList();
            });
        }



        public Task<Employee> getEmployee(string id)
        {
            return Task.Run(() =>
            {
                var obj = _context.Employees.Where(a => a.EmployeeId == id).First();
                return obj;

            });
        }


        public Task<List<Employee>> getEmployees()
        {
            return Task.Run(() =>
            {
                return _context.Employees.ToList();
            });
        }

    }
}
