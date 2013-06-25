using System.Web.Mvc;
using OhSoSecure.Core.Security;

namespace OhSoSecure.Web.Views.Shared
{
    // Don't forget this class is abstract, because your Razor view is implementing the "Execute()" method
    public abstract class OhSoSecureBaseView : WebViewPage
    {
        IOhSoSecurePrincipal ourUser;

        public new IOhSoSecurePrincipal User
        {
            get
            {
                if (ourUser == null)
                    return base.Context.User as OhSoSecurePrincipal ??
                           new OhSoSecurePrincipal(base.Context.User.Identity);
                return ourUser;
            }
            set { ourUser = value; }
        }
    }

    public abstract class OhSoSecureBaseView<T> : WebViewPage<T>
    {
        IOhSoSecurePrincipal ourUser;

        public new IOhSoSecurePrincipal User
        {
            get
            {
                if (ourUser == null)
                    return base.Context.User as OhSoSecurePrincipal ??
                           new OhSoSecurePrincipal(base.Context.User.Identity);
                return ourUser;
            }
            set { ourUser = value; }
        }
    }
}