using System;
using System.Web.Mvc;
using System.Web.Routing;
using GrowDT.Logging;

namespace GrowDT.MvcHelper.Filters
{
    public class LogVisitAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
            base.OnResultExecuted(filterContext);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = string.Format("{0} controller: {1} action: {2}", methodName, controllerName, actionName);
            if (methodName == "OnResultExecuted")
            {
                message += Environment.NewLine + "------------------------------------------------";
            }
            LoggingFactory.GetLogger().Log(LogLevel.Debug, message);
        }
    }
}
