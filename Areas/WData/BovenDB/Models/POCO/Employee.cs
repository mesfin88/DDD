using System.ComponentModel.DataAnnotations;

namespace Boven.Areas.WData.BovenDB.Models
{
    public class Employee
    {
        public const string EMP_ROLE_COORDINATOR = "Coordinator";
        public const string EMP_ROLE_INVESTIGATOR = "Investigator";
        public const string EMP_ROLE_MANAGER = "Manager";

        [Key]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string RoleTitle { get; set; }
        public string DepartmentId { get; set; }


        //// Helper method
        //public string getRoleDisplayName()
        //{
        //    try
        //    {
        //        if (RoleTitle.Equals(Employee.EMP_ROLE_COORDINATOR))
        //            return "samordnare";
        //        if (RoleTitle.Equals(Employee.EMP_ROLE_MANAGER))
        //            return "avdelningschef";
        //        // if coordinator
        //        return "handläggare";
        //    } catch (System.Exception)
        //    {
        //        throw new System.Exception("Error in Employee");
        //    }
        //}
    }

}



