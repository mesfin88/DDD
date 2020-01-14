using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.WData.BovenDB.Services
{
    public class ErrandStatusService : IErrandStatus
    {
        BovenContext _context;

        public ErrandStatusService(BovenContext context)
        {
            _context = context;
        }

        public Task<List<ErrandStatus>> getStatuses()
        {
            return Task.Run(() =>
            {
                List<ErrandStatus> l = null;
                l = _context.ErrandStatuses.ToList();
                return l;
            });
        }

    }
}
