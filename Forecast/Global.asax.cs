using System.Web.Optimization;
using System.Web.Http;

namespace Forecast
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Config.Bundler.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(Config.WebApiConfig.Register);
        }
    }
}
