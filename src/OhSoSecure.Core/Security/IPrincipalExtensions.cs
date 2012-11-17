using System.Linq;
using System.Security.Principal;
using OhSoSecure.Core.Domain;

namespace OhSoSecure.Core.Security
{
    public static class IPrincipalExtensions
    {
        
        public static bool IsInRole(this IPrincipal user, AuthRole role)
        {
            return user.IsInRole(role.ToString());
        }

        public static bool IsInAnyRole(this IPrincipal user, params AuthRole[] roles)
        {
            return roles.Any(user.IsInRole);
        }

        public static OhSoSecurePrincipal ToOhSoSecurePrincipal(this User user) {
            return new OhSoSecurePrincipal(user);
        }
    }
}