using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.WData.BovenDB.Boven.Areas.WData.Services
{
    public class BovenRepositoryService : IBovenRepository
    {
        private BovenContext _context;

        public BovenRepositoryService(BovenContext ctx)
        {
            this._context = ctx;
        }












    }
}
