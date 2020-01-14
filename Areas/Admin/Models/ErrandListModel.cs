using Boven.Areas.WData.BovenDB.Models;
using System.Collections.Generic;



namespace Boven.Areas.Admin.Models
{
    public class ErrandListModel : SelectListModel
    {
        //Post-property
        public string refNr { get; set; }

        //View-property
        public List<JoinedEntitysModel> Errands { get; set; }

    }



}
