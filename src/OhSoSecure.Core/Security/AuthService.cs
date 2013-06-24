using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using OhSoSecure.Core.DataAccess;
using OhSoSecure.Core.Domain;

namespace OhSoSecure.Core.Security
{
    public class AuthService : IAuthService
    {
        readonly IUserRepository userRepo;

        public AuthService(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public bool Authenticate(string userName, string password)
        {
            var user = userRepo.FindByUserName(userName);
            if (user != null && user.Password.Matches(password))
            {
                var jsonSerializer = new JavaScriptSerializer();
                var principal = user.ToOhSoSecurePrincipal();

                var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddHours(4),
                                                           false,
                                                           jsonSerializer.Serialize(
                                                           // Don't forget attribute to ignore IIdentity in custom principal
                                                           principal));

                HttpContext.Current.Response.Cookies.Set(new HttpCookie(FormsAuthentication.FormsCookieName,
                                                                        FormsAuthentication.Encrypt(ticket)));
                return true;
            }
            return false;
        }

        public User CreateUser(string userName, string password, string firstName, string lastName)
        {
            return userRepo.CreateUser(userName, password, firstName, lastName);
        }

        public string GetLoginUrlFor(string userName)
        {
            return FormsAuthentication.GetRedirectUrl(userName, false);
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}