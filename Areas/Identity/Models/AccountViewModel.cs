using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.Identity.Models
{
    public class AccountViewModel
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string RoleTitle { get; set; }

        public static implicit operator AccountViewModel(Employee em)
        {
            var vm = new AccountViewModel
            {
                EmployeeID = em.EmployeeId,
                EmployeeName = em.EmployeeName,
                RoleTitle = em.RoleTitle
            };
            return vm;
        }
        public static implicit operator Employee(AccountViewModel vm)
        {
            var em = new Employee
            {
                EmployeeId = vm.EmployeeID,
                EmployeeName = vm.EmployeeName,
                RoleTitle = vm.RoleTitle
            };
            return em;
        }
    }
}
