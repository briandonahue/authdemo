using System.Web.Mvc;

namespace OhSoSecure.Core.Web
{
    public static class MvcExtensions
    {
        public static string GetCurrentAction(this UrlHelper helper)
        {
            return helper.RequestContext.RouteData.Values.ContainsKey("Action") 
                ? helper.RequestContext.RouteData.Values["Action"].ToString()
                : string.Empty;
        }

        public static string GetCurrentController(this UrlHelper helper)
        {
            return helper.RequestContext.RouteData.Values.ContainsKey("Controller") 
                ? helper.RequestContext.RouteData.Values["Controller"].ToString()
                : string.Empty;
        }

         
    }
}