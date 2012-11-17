using System;
using System.Web.Mvc;
using OhSoSecure.Core.Security;
using OhSoSecure.Web.Models;

namespace OhSoSecure.Web.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && authService.Authenticate(model.UserName, model.Password))
            {

                return Redirect(authService.GetLoginUrlFor(model.UserName));
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword) {
                ModelState.AddModelError("", "Password and Password Confirmation do not match.");
            }
            if (ModelState.IsValid) {
                var user = authService.CreateUser(model.UserName, model.Password, model.FirstName, model.LastName);
                authService.Authenticate(model.UserName, model.Password);
                return Redirect(authService.GetLoginUrlFor(model.UserName));
            }
            return View(model);
        }
    }
}