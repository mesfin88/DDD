using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.Admin.Models
{
    public class ErrandDetailVM : SelectListModel
    {
        //ErrandDetails Edit
        public string events { get; set; }
        public string information { get; set; }
        public string noActionReason { get; set; }
        public bool noAction { get; set; }
        public List<IFormFile> loadSample { get; set; }
        public List<IFormFile> loadImage { get; set; }

        //ErrandDetailVC View
        public int errandId { get; set; }
        public string Place { get; set; }
        public string InformerName { get; set; }
        public string InformerPhone { get; set; }
        public string Observation { get; set; }
        public string InvestigatorInfo { get; set; }
        public string InvestigatorAction { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<Sample> Samples { get; set; }
        public string TypeOfCrime { get; set; }
        public string RefNumber { get; set; }
        public DateTime? DateOfObservation { get; set; }
        public string StatusName { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }


    }
}