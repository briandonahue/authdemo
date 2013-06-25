using System.Web.Mvc;
using OhSoSecure.Core.Security;

namespace OhSoSecure.Web.Controllers
{
    public class OhSoSecureController: Controller
    {
    public new IOhSoSecurePrincipal User
    {
      get
      {
          if (HttpContext != null)
                    return HttpContext.User as OhSoSecurePrincipal ??
                           new OhSoSecurePrincipal(HttpContext.User.Identity);
          return null;
      }
    }
    }
}