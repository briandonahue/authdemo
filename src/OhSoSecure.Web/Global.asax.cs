using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using OhSoSecure.Core.Security;
using OhSoSecure.Web.App_Start;
using OhSoSecure.Web.Helpers;

namespace OhSoSecure.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            StructureMapConfig.Configure();
            ModelMetadataProviders.Current = new ConventionModelMetaDataProvider();
        }

        protected void Application_OnPostAuthenticateRequest()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var principal = jsonSerializer.Deserialize<OhSoSecurePrincipal>(ticket.UserData);
                Context.User = principal;
            }
        }
        
    }
}