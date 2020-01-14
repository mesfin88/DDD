using System.ComponentModel.DataAnnotations;

namespace Boven.Areas.WData.BovenDB.Models
{
    public class Sample
    {
        [Key]
        public int SampleId { get; set; }
        public string SampleName { get; set; }
        public int ErrandId { get; set; }

    }

}



