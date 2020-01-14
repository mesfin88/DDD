using Boven.Areas.WData.BovenDB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boven.Areas.WData.BovenDB
{
    public interface IPicture
    {
        Task<Picture> DeletePicture(int id);
        Task<Picture> SavePicture(Picture picture);
        Task<List<Picture>> GetPictures(int errandId);
    }
}
