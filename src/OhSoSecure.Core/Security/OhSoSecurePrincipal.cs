using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Script.Serialization;
using OhSoSecure.Core.Domain;

namespace OhSoSecure.Core.Security
{
    public interface IOhSoSecurePrincipal : IPrincipal
    {
        string FirstName { get; set; }
        IEnumerable<string> Roles { get; set; }
        string UserName { get; set; }
        bool IsAuthenticated { get; }
    }

    public class OhSoSecurePrincipal : IOhSoSecurePrincipal
    {
        public OhSoSecurePrincipal()
        {
            UserName = string.Empty;
        }

        public OhSoSecurePrincipal(IIdentity identity)
        {
            UserName = identity.Name;
        }

        public OhSoSecurePrincipal(User user)
        {
            UserName = user.UserName;
            FirstName = user.FirstName;
            Roles = user.Roles.Select(r => r.Name);
        }

        public string FirstName { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string UserName { get; set; }

        public bool IsInRole(string role)
        {
            return Roles.Any(r => r == role);
        }

        [ScriptIgnore] // Tell serializer to ignore this or you get a circular serialization error
        public IIdentity Identity
        {
            get { return new GenericIdentity(UserName); }
        }

        public bool IsAuthenticated
        {
            get
            {
                return Identity.IsAuthenticated;
            }
        }
    }

}