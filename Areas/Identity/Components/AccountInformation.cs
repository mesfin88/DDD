using Boven.Areas.Identity.Models;
using Boven.Areas.WData.BovenDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Boven.Areas.Identity.Components
{
    public class AccountInformation : ViewComponent
    {
        private HttpContext _httpContext;

        public AccountInformation(IHttpContextAccessor service)
        {
            _httpContext = service.HttpContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string user = this._httpContext.User.Identity.Name;
            IEmployee employeeAsset = this._httpContext.RequestServices.GetRequiredService<IEmployee>();
            var employee = await employeeAsset.getEmployee(id: user);
            AccountViewModel viewModel = employee;
            return View(viewModel);
        }
    }
}
