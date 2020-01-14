using Boven.Areas.WData.BovenDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boven.Areas.Admin.Models
{
    public class EmployeeModel
    {
        public const string EMP_ROLE_COORDINATOR = "Coordinator";
        public const string EMP_ROLE_INVESTIGATOR = "Investigator";
        public const string EMP_ROLE_MANAGER = "Manager";

        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string RoleTitle { get; set; }
        public string DepartmentId { get; set; }

        public static implicit operator EmployeeModel(Employee em)
        {
            var vm = new EmployeeModel
            {
                EmployeeId = em.EmployeeId,
                EmployeeName = em.EmployeeName,
                RoleTitle = em.RoleTitle,
                DepartmentId = em.DepartmentId
            };
            return vm;
        }
        // Helper method
        public string getRoleDisplayName()
        {
            try
            {
                if (RoleTitle.Equals(EmployeeModel.EMP_ROLE_COORDINATOR))
                    return "samordnare";
                if (RoleTitle.Equals(EmployeeModel.EMP_ROLE_MANAGER))
                    return "avdelningschef";
                // if coordinator
                return "handläggare";
            }
            catch (System.Exception)
            {
                throw new System.Exception("Error in Employee");
            }
        }
    }
}
