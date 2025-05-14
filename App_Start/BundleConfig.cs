using System.Web;
using System.Web.Optimization;

namespace KNC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/material").Include(
          "~/Content/material-dashboard/css/material-dashboard.css"));

            bundles.Add(new ScriptBundle("~/bundles/material").Include(
                      "~/Content/material-dashboard/js/core/jquery.min.js",
                      "~/Content/material-dashboard/js/core/popper.min.js",
                      "~/Content/material-dashboard/js/core/bootstrap-material-design.min.js",
                      "~/Content/material-dashboard/js/material-dashboard.js"));

        }
    }
}
