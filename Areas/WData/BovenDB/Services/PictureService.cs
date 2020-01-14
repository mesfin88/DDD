using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.WData.BovenDB.Services
{
    public class PictureService : IPicture
    {
        private BovenContext _context;

        public PictureService(BovenContext context)
        {
            _context = context;
        }



        public Task<Picture> DeletePicture(int id)
        {
            return Task.Run(() =>
            {
                Picture dbEntry = _context.Pictures
                .FirstOrDefault(s => s.PictureId == id);
                if (dbEntry != null)
                {
                    _context.Pictures.Remove(dbEntry);
                    _context.SaveChanges();
                }
                return dbEntry;
            });
        }

        public Task<List<Picture>> GetPictures(int errandId)
        {

            return Task.Run(() =>
            {
                IQueryable<Picture> pics = _context.Pictures
                .Where(p => p.ErrandId.Equals(errandId));
                return pics.ToList();
            });

        }

        public Task<Picture> SavePicture(Picture picture)
        {
            return Task.Run(() =>
            {
                _context.Add(picture);
                int id = _context.SaveChanges();
                return picture;
            });
        }


    }
}
