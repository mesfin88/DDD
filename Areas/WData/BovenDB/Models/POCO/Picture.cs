using System.ComponentModel.DataAnnotations;

namespace Boven.Areas.WData.BovenDB.Models
{
    public class Picture
    {
        [Key]
        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public int ErrandId { get; set; }

    }

}



