using Boven.Areas.WData.BovenDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boven.Areas.Admin.Models
{
    public class PictureModel
    {
        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public int ErrandId { get; set; }

        public static implicit operator PictureModel(Picture em)
        {
            var vm = new PictureModel
            {
                PictureId = em.PictureId,
                PictureName = em.PictureName,
                ErrandId = em.ErrandId
            };
            return vm;
        }
    }
}
