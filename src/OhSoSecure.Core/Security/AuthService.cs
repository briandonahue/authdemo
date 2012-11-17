using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
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
                FormsAuthentication.SetAuthCookie(userName, false);
                return true;
            }
            return false;
        }

        public User CreateUser(string userName, string password, string firstName, string lastName)
        {
            var user = new User(userName, password, firstName, lastName);
            userRepo.Save(user);
            return user;
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