using System.Web;
using System.Web.Mvc;
using GrowDT.MvcHelper.Filters;
using GrowDT.MvcHelper.Utility;
using GrowDT.Application;

namespace GrowDT.MvcHelper.Customization
{
    [ExceptionHandler]
    [LogVisit]
    [Authorize]
    public class MvcBaseController : Controller
    {
        protected ActionResult DownloadFile(byte[] contents, string fileDownloadName)
        {
            if (Request.Browser.Browser == "IE")
                fileDownloadName = HttpUtility.UrlEncode(fileDownloadName);
            var contentType = FileHelper.GetFileType(fileDownloadName);
            return File(contents, contentType, fileDownloadName);
        }

        protected ActionResult DownloadFile(string path, string fileDownloadName)
        {
            var contentType = FileHelper.GetFileType(path);
            return File(path, contentType, fileDownloadName);
        }

        protected ActionResult DownloadFile(string path)
        {
            string fileDownloadName = System.IO.Path.GetFileName(path);
            var contentType = FileHelper.GetFileType(path);
            return File(path, contentType, fileDownloadName);
        }


        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Session[AppConfig.UserSessionKey] == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");//TODO:需要加上returnUrl
            }
        }
    }
}
