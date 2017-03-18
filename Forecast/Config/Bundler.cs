using System.Web.Optimization;

namespace Forecast.Config
{
    public class Bundler
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // General JS
#if DEBUG
            bundles.Add(new ScriptBundle("~/js/bundled")
                .Include("~/scripts/angular.js")
                .Include("~/scripts/angular-sanitize.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/jquery-1.9.1.js")
                );
#else
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/js/bundled")
                .Include("~/scripts/angular.min.js")
                .Include("~/scripts/angular-sanitize.min.js")
                .Include("~/scripts/bootstrap.min.js")
                .Include("~/scripts/jquery-1.9.1.min.js")
                //.Include("~/scripts/lodash.min.js")
            );
#endif

            // Angular
            bundles.Add(new ScriptBundle("~/app/bundled")
                .Include("~/app/ngAutocomplete.js")
                .Include("~/app/app.js")
                .Include("~/app/resources.js")
                .Include("~/app/services/openWeather.js"));

            // CSS
            bundles.Add(new StyleBundle("~/css/bundled")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/css/main.css"));
        }
    }
}