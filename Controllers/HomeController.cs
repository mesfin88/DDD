using Microsoft.AspNetCore.Mvc;

namespace Boven.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string user = this.HttpContext.User.Identity.Name;
            if (user != null)
                return RedirectToAction("ErrandList", "/Admin/Errand");
            else
                return RedirectToAction("ErrandReport", "/Admin/Errand");
        }
        public ViewResult Services()
        {
            return View();
        }

        public ViewResult Faq()
        {
            return View();
        }

        public ViewResult Contact()
        {
            return View();
        }
    }
}