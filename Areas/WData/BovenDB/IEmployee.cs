using Boven.Areas.WData.BovenDB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boven.Areas.WData.BovenDB
{
    public interface IEmployee
    {
        Task<List<Employee>> getEmployees(string departmentId = null);
        Task<Employee> getEmployee(string id);
        Task<List<Employee>> getEmployees();
    }
}
