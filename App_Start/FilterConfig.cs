using System.Web;
using System.Web.Mvc;
using KNC.Helper;

namespace KNC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()); // default
            filters.Add(new GlobalExceptionFilter()); // your custom filter
        }
    }
}
