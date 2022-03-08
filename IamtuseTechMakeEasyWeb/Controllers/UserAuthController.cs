using IamtuseTechMakeEasyWeb.Data;
using IamtuseTechMakeEasyWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> RegisterUser(RegistrationModel model)
        {
            model.RegistrationInvalid = "true";

            if (!ModelState.IsValid) { return PartialView("_UserRegistrationPartial", model); }

            ApplicationUser newUser = new()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address1 = model.Address1,
                Address2 = model.Address2,
                PostCode = model.PostCode,
                PhoneNumber = model.PhoneNumber,

            };

            var result = await _userManager.CreateAsync(newUser, model.Password);


            if (result.Succeeded)
            {
                model.RegistrationInvalid = "";

                await _signInManager.SignInAsync(newUser, isPersistent: false);

                return PartialView("_UserRegistrationPartial", model);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return PartialView("_UserRegistrationPartial", model);

        }

        [AllowAnonymous]
        public async Task<bool> CheckIfEmailExists(string userName)
        {
            bool exists = await _context.Users.AnyAsync(x => x.UserName.ToUpper() == userName.ToUpper());
            if (exists)
                return true;

            return false;
        }

    }
}
