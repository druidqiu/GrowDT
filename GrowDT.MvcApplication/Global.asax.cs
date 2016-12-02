using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GrowDT.Application;
using StackExchange.Profiling;

namespace GrowDT.MvcApplication
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            if (AppConfig.MiniProfilerEnable)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            if (AppConfig.MiniProfilerEnable)
            {
                MiniProfiler.Stop();
            }
        }
    }
}
