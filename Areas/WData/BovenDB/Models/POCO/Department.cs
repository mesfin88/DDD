﻿using System.ComponentModel.DataAnnotations;

namespace Boven.Areas.WData.BovenDB.Models
{
    public class Department
    {
        [Key]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}


