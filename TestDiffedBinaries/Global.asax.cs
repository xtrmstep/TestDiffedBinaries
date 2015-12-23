using System.Web;
using System.Web.Http;

namespace TestDiffedBinaries
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Configure();
        }

        public static void Configure(HttpConfiguration config = null)
        {
            if (config == null)
            {
                GlobalConfiguration.Configure(WebApiConfig.Register);
            }
            else
            {
                // used in the integration tests
                WebApiConfig.Register(config);
            }
        }
    }
}