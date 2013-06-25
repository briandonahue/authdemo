using System;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using OhSoSecure.Core.Domain;
using OhSoSecure.Core.Security;
using OhSoSecure.Web.Models;

namespace OhSoSecure.Web.Controllers
{
    public class AccountController : OhSoSecureController
    {
        readonly IAuthService authService;
        readonly ISession session;

        public AccountController(IAuthService authService, ISession session)
        {
            this.authService = authService;
            this.session = session;
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
            authService.Logout();
            return RedirectToAction("Index", "Home");
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


        public ActionResult UserProfile()
        {
            var user = session.Query<User>().First(u => u.UserName == User.UserName);
            var profileModel = new ProfileModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName
                };



            return View(profileModel);
        }
    }
}