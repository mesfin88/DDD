using Boven.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Boven.Areas.WData.BovenDB;
using System.Collections.Generic;
using Boven.Areas.WData.BovenDB.Models;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Boven.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireEmployeeRole")]
    public class ErrandController : Controller
    {
        private readonly HttpContext _httpContext;
        private IHostingEnvironment _environment;

        public ErrandController(IHttpContextAccessor httpContextAccessor, IHostingEnvironment env)
        {
            this._httpContext = httpContextAccessor.HttpContext;
            this._environment = env;
        }


        //ERRAND REPORT
        [AllowAnonymous]
        public IActionResult ErrandReport(ErrandReportFormViewModel model, int wizpagenr)
        {
            model.WizardPageNr = wizpagenr;
            return ViewComponent("ErrandReportWizard", model);
        }


        //ERRAND LIST
        public async Task<ViewResult> ErrandList(ErrandListModel errandListVM)
        {
            string user = this._httpContext.User.Identity.Name;
            var employeeEntity = await this._httpContext.RequestServices.GetRequiredService<IEmployee>().getEmployee(user);
            EmployeeModel employeeModel = employeeEntity;

            //Manager can only see errands for own departments
            if (employeeModel.RoleTitle == EmployeeModel.EMP_ROLE_MANAGER)
                errandListVM.departmentId = employeeModel.DepartmentId;
            //Investigator can only see own errands
            if (employeeModel.RoleTitle == EmployeeModel.EMP_ROLE_INVESTIGATOR)
                errandListVM.employeeId = employeeModel.EmployeeId;

            errandListVM.Errands = await this._httpContext.RequestServices.GetRequiredService<IErrand>().getErrandsList(
                departmentId: errandListVM.departmentId,
                statusId: errandListVM.statusId,
                refNr: errandListVM.refNr,
                employeeId: errandListVM.employeeId);

            //Droplists
            errandListVM.setStatuses(await this._httpContext.RequestServices.GetRequiredService<IErrandStatus>().getStatuses());
            errandListVM.setDepartments(await this._httpContext.RequestServices.GetRequiredService<IDepartment>().getDepartments());
            if (employeeModel.RoleTitle == EmployeeModel.EMP_ROLE_MANAGER)
                errandListVM.setEmployees(await this._httpContext.RequestServices.GetRequiredService<IEmployee>().getEmployees(employeeModel.DepartmentId));
            else
                errandListVM.setEmployees(await this._httpContext.RequestServices.GetRequiredService<IEmployee>().getEmployees());

            return View(errandListVM);
        }



        // Getmethod for presenting View ErrandDetails
        [HttpGet]
        public async Task<ViewResult> ErrandDetails(int id)
        {
            string user = this._httpContext.User.Identity.Name;
            var employeeEntity = await this._httpContext.RequestServices.GetRequiredService<IEmployee>().getEmployee(user);
            EmployeeModel employeeModel = employeeEntity;

            //if (user == null)
            //    return View("Login");
            //ViewBag.role = user.RoleTitle;
            //ViewBag.rolename = u.getRoleDisplayName();
            //ViewBag.userfullname = u.EmployeeName;

            JoinedEntitysModel errandModel = (await this._httpContext.RequestServices.GetRequiredService<IErrand>().getErrandsList(errId: id)).First();
            ErrandDetailVM errandViewModel = new ErrandDetailVM
            {
                errandId = id,

            };
            if (employeeModel.RoleTitle == Employee.EMP_ROLE_COORDINATOR)
                errandViewModel.setDepartments(await this._httpContext.RequestServices.GetRequiredService<IDepartment>().getDepartments(), selectedId: errandModel.DepartmentId);
            if (employeeModel.RoleTitle == Employee.EMP_ROLE_MANAGER)
            {
                errandViewModel.setEmployees(await this._httpContext.RequestServices.GetRequiredService<IEmployee>().getEmployees(employeeModel.DepartmentId), selectedId: errandModel.EmployeeId);
                errandViewModel.removeEmployee(user);
                errandViewModel.noAction = errandModel.StatusId == ErrandStatus.ST_NO_ACTION ? true : false;
                errandViewModel.information = errandModel.InvestigatorInfo;
            }
            if (employeeModel.RoleTitle == Employee.EMP_ROLE_INVESTIGATOR)
            {
                errandViewModel.events = errandModel.InvestigatorAction;
                errandViewModel.information = errandModel.InvestigatorInfo;
                errandViewModel.setStatuses(await this._httpContext.RequestServices.GetRequiredService<IErrandStatus>().getStatuses(), selectedId: errandModel.StatusId);
                errandViewModel.removeStatus(ErrandStatus.ST_NO_ACTION);
            }
            return View($"ErrandDetails{employeeModel.RoleTitle}", errandViewModel);
        }

        // Handling all the incoming data from ErrandDetail View posts for changing data.
        // After changes are saved Redirects to actionmethod ErrandDetails.
        [HttpPost]
        public async Task<IActionResult> UpdateErrandDetails(ErrandDetailVM errandDetailVM)
        {
            string user = this._httpContext.User.Identity.Name;
            var employeeEntity = await this._httpContext.RequestServices.GetRequiredService<IEmployee>().getEmployee(user);
            EmployeeModel employeeModel = employeeEntity;

            //if (user == null)
            //    return View("Login");
            //Create and fill Errand for saving
            Errand errand = new Errand
            {
                ErrandID = errandDetailVM.errandId,
                DepartmentId = errandDetailVM.departmentId,
                StatusId = errandDetailVM.statusId,
                EmployeeId = errandDetailVM.employeeId,
            };
            if (employeeModel.RoleTitle == Employee.EMP_ROLE_COORDINATOR)
            {
                if (errandDetailVM.statusId == ErrandStatus.ST_REPORTED)
                    errand.StatusId = ErrandStatus.ST_STARTED;
            }
            if (employeeModel.RoleTitle == Employee.EMP_ROLE_MANAGER)
            {
                errand.StatusId = errandDetailVM.noAction == true ? ErrandStatus.ST_NO_ACTION : ErrandStatus.ST_STARTED;
                errand.InvestigatorInfo = errandDetailVM.information + "\n\n" + errandDetailVM.noActionReason;
            }
            if (employeeModel.RoleTitle == Employee.EMP_ROLE_INVESTIGATOR)
            {
                errand.InvestigatorAction = errandDetailVM.events;
                errand.InvestigatorInfo = errandDetailVM.information;
                errand.StatusId = errandDetailVM.statusId;
            }
            //Save Errand
            await this._httpContext.RequestServices.GetRequiredService<IErrand>().SaveErrand(errand);
            //Save images
            if (errandDetailVM.loadImage != null)
                await ErrandHelper.saveStream(environment: this._environment, httpContext: this._httpContext, stream_list: errandDetailVM.loadImage, stream_type: "picture", errand_id: errandDetailVM.errandId);
            if (errandDetailVM.loadSample != null)
                await ErrandHelper.saveStream(environment: this._environment, httpContext: this._httpContext, stream_list: errandDetailVM.loadSample, stream_type: "sample", errand_id: errandDetailVM.errandId);
            //Redirect back again
            return RedirectToAction("ErrandDetails", new { id = errandDetailVM.errandId });
        }

        [HttpGet]
        public async Task<ActionResult> DeletePicture(int errId, int objId, string objName)
        {
            var path = System.IO.Path.Combine(this._environment.WebRootPath, "uploads", objName);
            System.IO.File.Delete(path);
            await this._httpContext.RequestServices.GetRequiredService<IPicture>().DeletePicture(objId);
            return RedirectToAction("ErrandDetails", new { id = errId });
        }
        [HttpGet]
        public async Task<ActionResult> DeleteSample(int errId, int objId, string objName)
        {
            var path = System.IO.Path.Combine(this._environment.WebRootPath, "uploads", objName);
            System.IO.File.Delete(path);
            await this._httpContext.RequestServices.GetRequiredService<ISample>().DeleteSample(objId);
            return RedirectToAction("ErrandDetails", new { id = errId });
        }

    }




    public static class ErrandHelper
    {
        public static async Task saveStream(IHostingEnvironment environment,
            HttpContext httpContext,
            List<IFormFile> stream_list,
            string stream_type,
            int errand_id)
        {
            foreach (IFormFile s in stream_list)
            {
                if (s.Length > 0)
                {
                    var tmpPath = System.IO.Path.GetTempFileName();
                    using (var stream = new FileStream(tmpPath, FileMode.Create))
                    {
                        await s.CopyToAsync(stream);
                    }
                    var toPath = System.IO.Path.Combine(environment.WebRootPath, "uploads", s.FileName);
                    toPath = ErrandHelper.idPath(toPath, errand_id, ftype: stream_type);
                    toPath = ErrandHelper.GetUniqueFilename(toPath);
                    System.IO.File.Move(tmpPath, toPath);
                    if (stream_type == "picture")
                        await httpContext.RequestServices.GetRequiredService<IPicture>().SavePicture(new Picture { PictureName = Path.GetFileName(toPath), ErrandId = errand_id });
                    else
                        await httpContext.RequestServices.GetRequiredService<ISample>().SaveSample(new Sample { SampleName = Path.GetFileName(toPath), ErrandId = errand_id });
                }
            }
        }
        private static string idPath(string path, int errandId, string ftype)
        {
            return Path.Combine(Path.GetDirectoryName(path), "EID" + errandId.ToString() + "_" + ftype + "_" + Path.GetFileName(path));
        }
        //Get a unique filename. If the file exists the name will be incremented by one.
        private static string GetUniqueFilename(string fullPath)
        {
            if (!Path.IsPathRooted(fullPath))
                fullPath = Path.GetFullPath(fullPath);
            if (System.IO.File.Exists(fullPath))
            {
                System.String filename = Path.GetFileName(fullPath);
                System.String path = fullPath.Substring(0, fullPath.Length - filename.Length);
                System.String filenameWOExt = Path.GetFileNameWithoutExtension(fullPath);
                System.String ext = Path.GetExtension(fullPath);
                int n = 1;
                do
                {
                    fullPath = Path.Combine(path, System.String.Format("{0} ({1}){2}", filenameWOExt, (n++), ext));
                }
                while (System.IO.File.Exists(fullPath));
            }
            return fullPath;
        }


    }





}
