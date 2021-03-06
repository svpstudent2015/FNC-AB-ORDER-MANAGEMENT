﻿using System.Web;
using System.Web.Optimization;

namespace FNC_AB_ORDER_MANAGEMENT
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.dataTables.min.1.10.15.js",
                        "~/Scripts/chosen.jquery.js",
                        "~/Scripts/jquery-1.12.4.min.js",
                        "~/Scripts/jquery.dataTables.select.min.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js"));
            


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        //"~/Scripts/jquery.validate*"));
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate-vsdoc.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery.dataTables.min.css",
                      "~/Content/select.dataTables.min.1.2.2.css",
                      "~/Content/chosen.css",
                      "~/Content/themes/base/minified/jquery-ui.min.css"));
            
        }
    }
}
