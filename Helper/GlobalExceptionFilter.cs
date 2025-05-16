using System;
using System.Web.Mvc;
using KNC.Helper;

namespace KNC.Helper
{
    public class GlobalExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            // Log the error
            var user = filterContext.HttpContext.User?.Identity?.Name;
            Logger.LogError(filterContext.Exception, user);

            // Set the result to a custom error view
            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary { { "ErrorMessage", filterContext.Exception.Message } }
            };

            filterContext.ExceptionHandled = true;
        }
    }
}