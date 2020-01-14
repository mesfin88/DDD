using System;
using System.Collections.Generic;


namespace Boven.Areas.WData.BovenDB.Models
{
    public class JoinedEntitysModel
    {
        public JoinedEntitysModel() { }
        public JoinedEntitysModel(int id)
        {
            ErrandId = id;
        }
        //ErrandList
        public int ErrandId { get; set; }
        public DateTime? DateOfObservation { get; set; }
        public string TypeOfCrime { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string RefNumber { get; set; }

        //ErrandDetails edit
        public string Place { get; set; }
        public string InformerName { get; set; }
        public string InformerPhone { get; set; }
        public string Observation { get; set; }
        public string InvestigatorInfo { get; set; }
        public string InvestigatorAction { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<Sample> Samples { get; set; }
    }
}
