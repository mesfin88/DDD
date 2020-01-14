using System;
using System.Collections.Generic;
using System.Linq;
using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.Admin.Models
{
    public class ErrandModel
    {
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
        public List<PictureModel> Pictures { get; set; }
        public List<SampleModel> Samples { get; set; }

        public static implicit operator ErrandModel(JoinedEntitysModel em)
        {
            var vm = new ErrandModel
            {
                ErrandId = em.ErrandId,
                DateOfObservation = em.DateOfObservation,
                TypeOfCrime = em.TypeOfCrime,
                StatusId = em.StatusId,
                StatusName = em.StatusName,
                DepartmentId = em.DepartmentId,
                DepartmentName = em.DepartmentName,
                EmployeeId = em.EmployeeId,
                EmployeeName = em.EmployeeName,
                RefNumber = em.RefNumber,
                Place = em.Place,
                InformerName = em.InformerName,
                InformerPhone = em.InformerPhone,
                Observation = em.Observation,
                InvestigatorInfo = em.InvestigatorInfo,
                InvestigatorAction = em.InvestigatorAction,
                Pictures = em.Pictures.Select(pic => new PictureModel
                {
                    PictureId = pic.PictureId,
                    PictureName = pic.PictureName,
                    ErrandId = pic.ErrandId
                }).ToList(),
                Samples = em.Samples.Select(samp => new SampleModel
                {
                    SampleId = samp.SampleId,
                    SampleName = samp.SampleName,
                    ErrandId = samp.ErrandId
                }).ToList()
            };
            return vm;
        }
    }
}
