using System;
using System.IO;
using System.Web;

namespace KNC.Helper
{
    public static class Logger
    {
        public static void Log(string message, string level = "INFO", string user = null)
        {
            try
            {
                var logPath = HttpContext.Current.Server.MapPath("~/App_Data/AppLog.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                using (var writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine("----- LOG ENTRY -----");
                    writer.WriteLine("Time: " + DateTime.UtcNow.ToString("u"));
                    writer.WriteLine("Level: " + level);
                    if (!string.IsNullOrEmpty(user))
                        writer.WriteLine("User: " + user);
                    writer.WriteLine("Message: " + message);
                    writer.WriteLine("---------------------\n");
                }
            }
            catch
            {
                // Optionally handle logging failures (e.g., send to remote logger)
            }
        }

        public static void LogError(Exception ex, string user = null)
        {
            Log($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}", "ERROR", user);
        }
    }
}