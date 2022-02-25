using IamtuseTechMakeEasyWeb.Data;
using IamtuseTechMakeEasyWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IamtuseTechMakeEasyWeb.Controllers
{
    public class UserAuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAuthController(ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous, ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            model.LoginInvalid = "true";
            if (!ModelState.IsValid)
            {
                return PartialView("_UserLoginPartial", model);
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                    isPersistent: model.RememberMe, lockoutOnFailure: false);


                if (result.Succeeded)
                {
                    model.LoginInvalid = "";

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invaild login attempt.");
                }

                return PartialView("_UserLoginPartial", model);
            }

        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();


            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }


            return RedirectToAction("index", "home");

        }
    }
}
