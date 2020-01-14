using Boven.Areas.WData.BovenDB.Models;
using System.ComponentModel.DataAnnotations;

namespace Boven.Areas.Admin.Models
{
    public class ErrandReportFormViewModel
    {

        [Required(ErrorMessage = "Du måste fylla i en plats")]
        [Display(Name = "Var har brottet skett någonstans?")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Du måste fylla i brott")]
        [Display(Name = "Vilken typ av brott?")]
        public string TypeOfCrime { get; set; }

        [Required(ErrorMessage = "Du måste fylla i datum")]
        [Display(Name = "När skedde brottet?")]
        [DataType(DataType.Date, ErrorMessage = "Du måste ange ett datum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime? DateOfObservation { get; set; }

        [Required(ErrorMessage = "Du måste fylla i namn")]
        [Display(Name = "Ditt namn (för- och efternamn):")]
        public string InformerName { get; set; }

        [Required(ErrorMessage = "Du måste ange ett telefonnummer")]
        [Display(Name = "Din telefon :")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Ogiltigt telefonnummer")]
        [RegularExpression(@"^(?:0|\+?46)(?:\d\s?){6,12}$", ErrorMessage = "Ogiltigt telefonnummer.")]
        public string InformerPhone { get; set; }

        [Display(Name = "Beskriv din observation (ex.namn på misstänkt person):")]
        public string Observation { get; set; }

        public int WizardPageNr { get; set; }
        public string RefNumber { get; set; }




        public static implicit operator ErrandReportFormViewModel(Errand em)
        {
            var vm = new ErrandReportFormViewModel
            {
                RefNumber = em.RefNumber,
                Place = em.Place,
                TypeOfCrime = em.TypeOfCrime,
                DateOfObservation = em.DateOfObservation,
                InformerName = em.InformerName,
                InformerPhone = em.InformerPhone,
                Observation = em.Observation
            };
            return vm;
        }
        public static implicit operator Errand(ErrandReportFormViewModel vm)
        {
            var em = new Errand
            {
                RefNumber = vm.RefNumber,
                Place = vm.Place,
                TypeOfCrime = vm.TypeOfCrime,
                DateOfObservation = vm.DateOfObservation,
                InformerName = vm.InformerName,
                InformerPhone = vm.InformerPhone,
                Observation = vm.Observation
            };
            return em;
        }
    }

}
