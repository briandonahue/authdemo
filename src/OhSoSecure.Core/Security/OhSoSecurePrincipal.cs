using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Script.Serialization;
using OhSoSecure.Core.Domain;

namespace OhSoSecure.Core.Security
{
    public class OhSoSecurePrincipal : IPrincipal
    {
        public OhSoSecurePrincipal()
        {
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
    }
}