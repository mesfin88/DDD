using Boven.Areas.WData.BovenDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boven.Areas.WData.BovenDB
{
    public interface IDepartment
    {
        Task<List<Department>> getDepartments();
    }
}
