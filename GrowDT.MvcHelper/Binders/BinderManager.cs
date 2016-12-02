using System.Web.Mvc;
using GrowDT.MvcHelper.Authorization;

namespace GrowDT.MvcHelper.Binders
{
    internal static class BinderManager
    {
        public static void Bind()
        {
            ModelBinders.Binders.Add(typeof(UserSession), new UserModelBinder());
        }
    }
}
