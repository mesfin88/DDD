using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.WData.BovenDB.Services
{
    public class SampleService : ISample
    {
        private BovenContext _context;

        public SampleService(BovenContext context)
        {
            _context = context;
        }


        public Task<Sample> DeleteSample(int id)
        {
            return Task.Run(() =>
            {
                Sample dbEntry = _context.Samples
                .FirstOrDefault(s => s.SampleId == id);
                if (dbEntry != null)
                {
                    _context.Samples.Remove(dbEntry);
                    _context.SaveChanges();
                }
                return dbEntry;
            });
        }

        public Task<List<Sample>> GetSamples(int errandId)
        {
            return Task.Run(() =>
            {
                IQueryable<Sample> samples = _context.Samples
                .Where(p => p.ErrandId.Equals(errandId));
                return samples.ToList();
            });
        }

        public Task<Sample> SaveSample(Sample sample)
        {
            return Task.Run(() =>
            {
                _context.Add(sample);
                int id = _context.SaveChanges();
                //sample.SampleId = id;
                return sample;
            });
        }
    }
}
