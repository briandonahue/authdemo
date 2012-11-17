using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace OhSoSecure.Core.Security
{
    public class OhSoSecurePrincipal: IPrincipal
    {
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public OhSoSecurePrincipal() {}

        public OhSoSecurePrincipal(string name, IEnumerable<string> roles)
        {
            Name = name;
            Roles = roles;
        }

        public bool IsInRole(string role)
        {
            return Roles.Any(r => r == role);
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(Name); }
        }
    }
}