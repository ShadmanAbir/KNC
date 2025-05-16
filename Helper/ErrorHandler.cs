using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace KNC.Helper
{
    public static class ErrorHandler
    {
        // Log error to a file (can be extended to use a database or external service)
        public static void Log(Exception ex, string user = null)
        {
            try
            {
                var logPath = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                using (var writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine("----- ERROR -----");
                    writer.WriteLine("Time: " + DateTime.UtcNow.ToString("u"));
                    if (!string.IsNullOrEmpty(user))
                        writer.WriteLine("User: " + user);
                    writer.WriteLine("Message: " + ex.Message);
                    writer.WriteLine("Source: " + ex.Source);
                    writer.WriteLine("StackTrace: " + ex.StackTrace);
                    if (ex.InnerException != null)
                    {
                        writer.WriteLine("InnerException: " + ex.InnerException.Message);
                        writer.WriteLine("Inner StackTrace: " + ex.InnerException.StackTrace);
                    }
                    writer.WriteLine("-----------------\n");
                }
            }
            catch
            {
                // Fallback: do nothing or send to a remote logger
            }
        }

        // Custom error result for MVC
        public static ActionResult Handle(Exception ex, string viewName = "Error", string user = null)
        {
            Log(ex, user);
            var result = new ViewResult { ViewName = viewName };
            result.ViewData["ErrorMessage"] = ex.Message;
            return result;
        }
    }
}