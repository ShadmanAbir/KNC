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
using System.Reflection;

namespace KNC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("Unity.Abstractions"))
            {
                return Assembly.Load("Unity.Abstractions, Version=5.11.7.0, Culture=neutral, PublicKeyToken=489b6accfaf20ef0");
            }
            if (args.Name.StartsWith("Unity.Container"))
            {
                return Assembly.Load("Unity.Container, Version=5.11.11.0, Culture=neutral, PublicKeyToken=489b6accfaf20ef0");
            }
            return null;
        }
    }
}
