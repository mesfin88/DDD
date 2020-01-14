using System.Collections.Generic;
using Boven.Areas.WData.BovenDB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Boven.Areas.Admin.Models
{
    public abstract class SelectListModel
    {
        //Post-property
        public string departmentId { get; set; }
        public string statusId { get; set; }
        public string employeeId { get; set; }

        //View-property
        public List<SelectListItem> ErrandStatuses { get; set; }
        public List<SelectListItem> Departments { get; set; }
        public List<SelectListItem> Employees { get; set; }

        public void removeEmployee(string emp)
        {
            List<SelectListItem> r = new List<SelectListItem>();
            foreach (SelectListItem item in this.Employees)
            {
                if (item.Value != emp)
                    r.Add(item);
            }
            this.Employees = r;
        }
        public void removeStatus(string status)
        {
            List<SelectListItem> r = new List<SelectListItem>();
            foreach (SelectListItem item in this.ErrandStatuses)
            {
                if (item.Value != status)
                    r.Add(item);
            }
            this.ErrandStatuses = r;
        }
        public void setDepartments(List<Department> departments, string selectedId = null)
        {
            {
                List<SelectListItem> r = new List<SelectListItem>();
                for (int i = 0; i < departments.Count; i++)
                {
                    Department d = (Department)departments[i];
                    if (d.DepartmentId != "D00")
                        r.Add(new SelectListItem { Text = d.DepartmentName, Value = d.DepartmentId, Selected = selectedId == d.DepartmentId ? true : false });
                }
                this.Departments = r;
            }
        }
        public void setStatuses(List<ErrandStatus> list, string selectedId = null)
        {
            {
                List<SelectListItem> r = new List<SelectListItem>();
                for (int i = 0; i < list.Count; i++)
                {
                    ErrandStatus d = (ErrandStatus)list[i];
                    r.Add(new SelectListItem { Text = d.StatusName, Value = d.StatusId, Selected = selectedId == d.StatusId ? true : false });
                }
                this.ErrandStatuses = r;
            }
        }

        public void setEmployees(List<Employee> list, string selectedId = null)
        {
            {
                List<SelectListItem> r = new List<SelectListItem>();
                for (int i = 0; i < list.Count; i++)
                {
                    Employee d = (Employee)list[i];
                    r.Add(new SelectListItem { Text = d.EmployeeName, Value = d.EmployeeId, Selected = selectedId == d.EmployeeId ? true : false });
                }
                this.Employees = r;
            }
        }

    }
}
