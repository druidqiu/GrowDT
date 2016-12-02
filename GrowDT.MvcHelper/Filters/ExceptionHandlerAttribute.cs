using System;
using System.Web.Mvc;
using GrowDT.Logging;
using System.Net;
using GrowDT.MvcHelper.Customization;

namespace GrowDT.MvcHelper.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            string controllerName = (string) filterContext.RouteData.Values["controller"];
            string actionName = (string) filterContext.RouteData.Values["action"];
            const string msgTemplate = "执行 controller[{0}] 的 action[{1}] 时产生异常";
            LoggingFactory.GetLogger().Log(LogLevel.Error, string.Format(msgTemplate, controllerName, actionName), filterContext.Exception);


            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                string message = "Something went wrong while processing your request. Please refresh the page and try again.";
                if (filterContext.Exception is ICustomMvcException)
                {
                     message = filterContext.Exception.Message;
                }

                filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true; //for iis7
                filterContext.ExceptionHandled = true;

                filterContext.Result = new JsonResult
                {
                    Data = new {Success = false, Message = message}
                };
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }
}
