using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boven.Areas.WData.BovenDB.Models;

namespace Boven.Areas.WData.BovenDB.Services
{
    public class ErrandService : IErrand
    {
        BovenContext _context;

        public ErrandService(BovenContext context)
        {
            _context = context;
        }

        public Errand DeleteErrand(int id)
        {
            Errand dbEntry = _context.Errands
                .FirstOrDefault(s => s.ErrandID == id);
            if (dbEntry != null)
            {
                _context.Errands.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public Task<List<JoinedEntitysModel>> getErrandsList(
            int errId,
            string depId,
            string statusId,
            string refNr,
            string empId)
        {
            IQueryable<Errand> eq = this._context.Errands;
            if (errId != 0)
                eq = eq.Where(p => p.ErrandID.Equals(errId));
            if (depId != null && depId != "null")
                eq = eq.Where(p => p.DepartmentId == depId);
            if (statusId != null && statusId != "null")
                eq = eq.Where(p => p.StatusId == statusId);
            if (refNr != null && refNr != "null")
                eq = eq.Where(p => p.RefNumber.Contains(refNr));
            if (empId != null && empId != "null")
                eq = eq.Where(p => p.EmployeeId.Equals(empId));


            return Task.Run(() =>
            {
                var result = (from ERR in eq
                              from EMP in _context.Employees
                              .Where(e => ERR.EmployeeId == e.EmployeeId).DefaultIfEmpty()
                              from DEP in _context.Departments
                              .Where(d => ERR.DepartmentId == d.DepartmentId).DefaultIfEmpty()

                              from ST in _context.ErrandStatuses
                              .Where(s => ERR.StatusId == s.StatusId).DefaultIfEmpty()

                              select new JoinedEntitysModel
                              {
                                  ErrandId = ERR.ErrandID,
                                  RefNumber = ERR.RefNumber,
                                  DateOfObservation = ERR.DateOfObservation,
                                  TypeOfCrime = ERR.TypeOfCrime,
                                  StatusId = ERR.StatusId,
                                  StatusName = ST.StatusName,
                                  DepartmentId = ERR.DepartmentId,
                                  DepartmentName = DEP.DepartmentName,
                                  EmployeeId = ERR.EmployeeId,
                                  EmployeeName = EMP.EmployeeName,
                                  //ErrandEdit
                                  Place = ERR.Place,
                                  InformerName = ERR.InformerName,
                                  InformerPhone = ERR.InformerPhone,
                                  Observation = ERR.Observation,
                                  InvestigatorInfo = ERR.InvestigatorInfo,
                                  InvestigatorAction = ERR.InvestigatorAction
                              }).ToList();
                return result;
            });
        }


        public Task<Errand> SaveErrand(Errand errand)
        {
            return Task.Run(() =>
            {
                Errand result = null;
                if (errand.ErrandID == 0)
                {
                    errand.StatusId = ErrandStatus.ST_REPORTED;
                    errand.RefNumber = System.DateTime.Now.Year.ToString() + "-45-" + this.GetSequence(id: 1).CurrentValue.ToString();
                    _context.Errands.Add(errand);
                    this.IncrementSequence(id: 1);
                    result = errand;
                }
                else //UPDATE
                {
                    Errand dbEntry = _context.Errands
                        .FirstOrDefault(s => s.ErrandID == errand.ErrandID);

                    if (dbEntry != null)
                    {
                        dbEntry.Place = errand.Place == null ? dbEntry.Place : errand.Place;
                        dbEntry.TypeOfCrime = errand.TypeOfCrime == null ? dbEntry.TypeOfCrime : errand.TypeOfCrime;
                        dbEntry.DateOfObservation = errand.DateOfObservation == null ? dbEntry.DateOfObservation : errand.DateOfObservation;
                        dbEntry.Observation = errand.Observation == null ? dbEntry.Observation : errand.Observation;
                        dbEntry.InformerName = errand.InformerName == null ? dbEntry.InformerName : errand.InformerName;
                        dbEntry.InformerPhone = errand.InformerPhone == null ? dbEntry.InformerPhone : errand.InformerPhone;
                        dbEntry.InvestigatorInfo = errand.InvestigatorInfo == null ? dbEntry.InvestigatorInfo : errand.InvestigatorInfo;
                        dbEntry.InvestigatorAction = errand.InvestigatorAction == null ? dbEntry.InvestigatorAction : errand.InvestigatorAction;
                        dbEntry.RefNumber = errand.RefNumber == null ? dbEntry.RefNumber : errand.RefNumber;

                        if (dbEntry.StatusId == ErrandStatus.ST_REPORTED)
                            dbEntry.StatusId = ErrandStatus.ST_STARTED;
                        else
                            dbEntry.StatusId = errand.StatusId == null || errand.StatusId == "null" ? dbEntry.StatusId : errand.StatusId;

                        dbEntry.EmployeeId = errand.EmployeeId == null || errand.EmployeeId == "null" ? dbEntry.EmployeeId : errand.EmployeeId;


                        dbEntry.DepartmentId = errand.DepartmentId == null || errand.DepartmentId == "null" ? dbEntry.DepartmentId : errand.DepartmentId;

                        //if (userRole == Employee.EMP_ROLE_MANAGER && errand.StatusId == ErrandStatus.ST_NO_ACTION)
                        //    dbEntry.StatusId = errand.StatusId;


                    }
                    result = dbEntry;
                }
                _context.SaveChanges();
                return result;
            });
        }

        //SEQUENCE************************
        public Sequence GetSequence(int id)
        {
            var obj = _context.Sequences.Where(a => a.Id == id).First();
            return obj;
        }
        public void IncrementSequence(int id)
        {
            Sequence dbEntry = _context.Sequences
                    .FirstOrDefault(s => s.Id == id);
            if (dbEntry != null)
            {
                dbEntry.CurrentValue++;
            }
            _context.SaveChanges();
        }


    }
}
