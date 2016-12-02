using System;
using System.Web;
using System.Web.Mvc;
using GrowDT.Application;
using GrowDT.MvcHelper.Authorization;

namespace GrowDT.MvcHelper.Filters
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private UserRole _requiredRole { get; set; }

        public RoleAuthorizeAttribute(UserRole requiredRole)
        {
            _requiredRole = requiredRole;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var userInfo = httpContext.Session[AppConfig.UserSessionKey] as UserSession;
            var isUnAuthorized = userInfo == null
                                 || userInfo.Role == UserRole.None
                                 || (userInfo.Role & _requiredRole) == 0;
            if (isUnAuthorized)
            {
                httpContext.Response.StatusCode = 403;
                return false;
            }
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult { Data = new { Success = false, Message = "Permission denied" } };
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Home");
                }
            } 
        }
    }
}
