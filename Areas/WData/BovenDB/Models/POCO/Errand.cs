
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boven.Areas.WData.BovenDB.Models
{
    public class Errand
    {


        [Key]
        public int ErrandID { get; set; } = 0;


        public string Place { get; set; }


        public string TypeOfCrime { get; set; }


        public System.DateTime? DateOfObservation { get; set; }


        public string Observation { get; set; }



        public string InformerName { get; set; }


        public string InformerPhone { get; set; }


        public string InvestigatorInfo { get; set; }
        public string InvestigatorAction { get; set; }

        
        public string RefNumber { get; set; }

        // Foreign keys
        // We dont want all the foreign keys to be loaded and therefor use virtual
        [ForeignKey("ErrandStatus")]
        public virtual string StatusId { get; set; }
        [ForeignKey("Department")]
        public virtual string DepartmentId { get; set; }
        [ForeignKey("Employee")]
        public virtual string EmployeeId { get; set; }
    }
}


