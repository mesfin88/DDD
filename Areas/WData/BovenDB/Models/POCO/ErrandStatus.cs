using System.ComponentModel.DataAnnotations;

namespace Boven.Areas.WData.BovenDB.Models
{
    public class ErrandStatus
    {
        public static string ST_REPORTED = "S_A";
        public static string ST_NO_ACTION = "S_B";
        public static string ST_STARTED = "S_C";
        public static string ST_READY = "S_D";


        [Key]
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }
}


