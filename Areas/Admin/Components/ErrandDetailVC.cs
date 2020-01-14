using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Boven.Areas.WData.BovenDB.Models;
using Boven.Areas.Admin.Models;
using Microsoft.Extensions.DependencyInjection;
using Boven.Areas.WData.BovenDB;

namespace Boven.Components
{
    public class ErrandDetailVC : ViewComponent
    {
        private readonly HttpContext _httpContext;

        public ErrandDetailVC(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            //Get DBModel
            JoinedEntitysModel errandModel = ((List<JoinedEntitysModel>)await this._httpContext.RequestServices.GetRequiredService<IErrand>().getErrandsList(errId: id)).First();
            List<Picture> pics = await this._httpContext.RequestServices.GetRequiredService<IPicture>().GetPictures(errandId: id);
            errandModel.Pictures = pics;
            List<Sample> samples = await this._httpContext.RequestServices.GetRequiredService<ISample>().GetSamples(errandId: id);
            errandModel.Samples = samples;

            //Fill ViewModel
            ErrandDetailVM viewModel = new ErrandDetailVM()
            {
                //AutoMapper could have been used here
                //https://github.com/AutoMapper/AutoMapper
                errandId = errandModel.ErrandId,
                Place = errandModel.Place,
                InformerName = errandModel.InformerName,
                InformerPhone = errandModel.InformerPhone,
                Observation = errandModel.Observation,
                InvestigatorInfo = errandModel.InvestigatorInfo,
                InvestigatorAction = errandModel.InvestigatorAction,
                Pictures = errandModel.Pictures,
                Samples = errandModel.Samples,
                TypeOfCrime = errandModel.TypeOfCrime,
                RefNumber = errandModel.RefNumber,
                DateOfObservation = errandModel.DateOfObservation,
                StatusName = errandModel.StatusName,
                DepartmentName = errandModel.DepartmentName,
                EmployeeName = errandModel.EmployeeName
            };
            return View(viewModel);
        }

    }
}
