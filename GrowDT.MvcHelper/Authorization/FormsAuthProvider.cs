using System.Web;
using System.Web.Security;
using GrowDT.Application;

namespace GrowDT.MvcHelper.Authorization
{
    public class FormsAuthProvider: IAuthProvider
    {
        public void Authenticate(string username)
        {
            FormsAuthentication.SetAuthCookie(username, false);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session[AppConfig.UserSessionKey] = null;
        }

        public bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.Session[AppConfig.UserSessionKey] != null;
        }

        public string LoginUrl
        {
            get { return FormsAuthentication.LoginUrl; }
        }
    }
}
