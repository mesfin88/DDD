using Boven.Areas.WData.BovenDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boven.Areas.WData.BovenDB
{
    public interface IErrand
    {
        Task<Errand> SaveErrand(Errand errand);
        Task<List<JoinedEntitysModel>> getErrandsList(
                int errId = 0,
                string departmentId = null,
                string statusId = null,
                string refNr = null,
                string employeeId = null);
        Errand DeleteErrand(int id);

    }
}
