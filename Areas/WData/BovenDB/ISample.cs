using Boven.Areas.WData.BovenDB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boven.Areas.WData.BovenDB
{
    public interface ISample
    {
        Task<Sample> DeleteSample(int id);
        Task<Sample> SaveSample(Sample sample);
        Task<List<Sample>> GetSamples(int errandId);
    }
}
