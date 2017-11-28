using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SklepMuzyczny.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/Site.css",
                "~/Content/bootstrap.css",
                "~/Content/ErrorStyle.css",
                "~/Content/FontStyles.css"
                ));
           
            bundles.Add(new ScriptBundle("~/Scripts/js")
                .Include("~/Scripts/jquery-{version}.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/modernizr-{version}js"
                ));
        }
    }
}