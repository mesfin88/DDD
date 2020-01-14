using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Boven.Areas.Identity.Models;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Boven.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {



        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(IServiceProvider provider)
        {
            this._userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            this._roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            this._signInManager = provider.GetRequiredService<SignInManager<IdentityUser>>();
        }

        [HttpGet]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await this._userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await this._signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Admin/Errand/ErrandList");
                    }
                }
            }
            ModelState.AddModelError("", "Ogiltigt namn eller lösenord");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await this._signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [Authorize(Policy = "RequireEmployeeRole")]
        public async Task<ActionResult> UsersList()
        {
            System.Linq.IQueryable<IdentityUser> users = _userManager.Users;
            UsersListModel vm = new UsersListModel();
            foreach (IdentityUser u in users)
            {
                List<SelectListItem> sl = new List<SelectListItem>();
                IList<string> roles = await _userManager.GetRolesAsync(u);
                foreach (string r in roles)
                    sl.Add(new SelectListItem { Text = r });
                vm.Users.Add(new User { ID = u.Id, Name = u.UserName, Roles = sl });
            }
            return View(vm);
        }

        [Authorize(Policy = "RequireEmployeeRole")]
        [HttpGet]
        public async Task<ActionResult> UserDelete(string id)
        {
            IdentityUser user = await _userManager.FindByNameAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("UsersList");
        }


    }
}