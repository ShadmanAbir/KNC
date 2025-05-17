using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using KNC.Helper;

namespace KNC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            ErrorHandler.Log(ex, HttpContext.Current?.User?.Identity?.Name);
        }
    }
}
