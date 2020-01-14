using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Boven.Infrastructure;

using Boven.Areas.WData.BovenDB;
using Boven.Areas.Admin.Models;

namespace Boven.Areas.Admin.Components
{
    public class ErrandReportWizard : ViewComponent
    {
        private readonly HttpContext _httpContext;

        public ErrandReportWizard(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(ErrandReportFormViewModel model)
        {

            if (model == null || model.WizardPageNr == 0)
            {
                model = this._httpContext.Session.GetJson<ErrandReportFormViewModel>("ErrandReportFormViewModel");
                return View("ReportForm", model);
            }
            if ((this._httpContext.Request.Method == "POST") && (model.WizardPageNr == 1))
            {
                if (!ModelState.IsValid)
                {
                    model.WizardPageNr--;
                    return View("ReportForm", model);
                }
                this._httpContext.Session.SetJson("ErrandReportFormViewModel", model);
                return View("ReportValidate", model);
            }
            if (model.WizardPageNr == 2)
            {
                IErrand errandAsset = this._httpContext.RequestServices.GetRequiredService<IErrand>();
                model = this._httpContext.Session.GetJson<ErrandReportFormViewModel>("ErrandReportFormViewModel");
                model = await errandAsset.SaveErrand(model);
                this._httpContext.Session.SetJson("ErrandReportFormViewModel", null);
                return View("ReportThanks", model);
            }
            throw new System.Exception("Error in ErrandREportWizard.");
        }
    }
}
