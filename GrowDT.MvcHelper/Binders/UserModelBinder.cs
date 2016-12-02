using System.Web.Mvc;
using GrowDT.Application;
using GrowDT.MvcHelper.Authorization;

namespace GrowDT.MvcHelper.Binders
{
    internal class UserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            UserSession user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = (UserSession)controllerContext.HttpContext.Session[AppConfig.UserSessionKey];
            }

            return user;
        }
    }
}
